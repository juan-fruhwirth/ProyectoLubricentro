using biz;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Lubricentro
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {

                if (Session["usuario"] != null){
                    Usuario usuario_actual = (Usuario)Session["Usuario"];

                    if (usuario_actual.confirmado == false)
                    {
                        Response.Redirect("ConfirmacionEmail.aspx");
                    }
                    else
                    {
                        Response.Redirect("Default.aspx");
                    }
                    
                }
            }

        }
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection cn = new System.Data.SqlClient.SqlConnection();
                cn.ConnectionString = ConfigurationManager.ConnectionStrings["JUAN-LAPTOP"].ToString();
                cn.Open();
                string ls_sql = "SELECT UsuarioID, Correo, Telefono, Nombre, Apellido, NivelUsuario, CorreoConfirmado  FROM Usuarios WHERE Correo = @correo";
                SqlCommand cmd = new SqlCommand(ls_sql, cn);
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@correo", txtCorreo.Text);

                //cuidado que no pase igual con valores 0 si falla el Command
                int id_usuario = -1;
                string correo = txtCorreo.Text;
                string telefono = "";
                string nombre = "";
                string apellido = "";
                string contrasenia_ingresada = txtPassword.Text;
                int id_nivel = -1;
                bool confirmado = false;


               
                

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        id_usuario = Convert.ToInt32(reader["UsuarioID"]);
                        id_nivel = Convert.ToInt32(reader["NivelUsuario"]);
                        telefono = reader["Telefono"].ToString();
                        nombre = reader["Nombre"].ToString();
                        apellido = reader["Apellido"].ToString();
                        confirmado = Convert.ToBoolean(reader["CorreoConfirmado"]);
                    }
                }

                Usuario usuario = new Usuario(correo, telefono, nombre, apellido, contrasenia_ingresada, id_usuario, id_nivel, confirmado);


                string hash_guardado = null;
                string salt_guardado = null;

                string ls_sql2 = "SELECT ContraseñaHash, ContraseñaSalt FROM Contraseñas WHERE UsuarioID = @usuarioID";
                SqlCommand cmd2 = new SqlCommand(ls_sql2, cn);
                cmd2.CommandType = CommandType.Text;
                cmd2.Parameters.AddWithValue("@usuarioID", id_usuario);  // Usa parámetros para evitar inyecciones SQL

                using (SqlDataReader reader = cmd2.ExecuteReader())
                {
                    if (reader.Read())  // Lee la primera fila (si existe)
                    {
                         hash_guardado = reader["ContraseñaHash"].ToString();
                         salt_guardado = reader["ContraseñaSalt"].ToString();


                        // Ahora puedes usar hash_guardado y salt_guardado
                    }
                    else
                    {
                        // Manejar el caso donde no se encuentra la fila
                        throw new Exception("Usuario no encontrado");
                    }
                }

                if (HasherContrasenia.VerificarContrasenia(txtPassword.Text, hash_guardado, salt_guardado) == false) {
                    throw new Exception("Contraseña incorrecta");
                }

                
          
                Session["Usuario"] = usuario;

                if (usuario.confirmado == false)
                {
                    int codigo_actual = (Registro.SendConfirmationEmail(usuario.correo));
                    if (codigo_actual != -1)
                    {
                        Registro.GuardarCodigoEnBaseDeDatos(usuario.id_usuario, codigo_actual);
                    }
                    lbl1.Text = "Falta confirmar su correo, se envio un nuevo codigo a su mail";

                    Response.AddHeader("Refresh", "2;url=ConfirmacionEmail.aspx");

                }

                else
                {
                    if (Session["Usuario"] != null)
                    {

                        lbl1.Text = "Se inicio sesion";
                        Response.AddHeader("Refresh", "0.5;url=Default.aspx");

                    }

                }



            }

            catch(Exception error)
            {
                lbl1.Text = "No se inicio sesion " +error;
                Console.WriteLine(error);
            }
        }
    }
}