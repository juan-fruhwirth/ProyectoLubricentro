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

        }
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection cn = new System.Data.SqlClient.SqlConnection();
                cn.ConnectionString = ConfigurationManager.ConnectionStrings["JUAN_LAPTOP"].ToString();
                cn.Open();
                string ls_sql = "SELECT UsuarioID FROM Usuarios WHERE Correo = '" + txtCorreo.Text+ "'";

                SqlCommand cmd = new SqlCommand(ls_sql, cn);
                cmd.CommandType = System.Data.CommandType.Text;
                int id_usuario = Convert.ToInt32(cmd.ExecuteScalar());


                //Obtener el hash y el salt asociado a ese usuarioID

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

                if (HasherContrasenia.VerificarContrasenia(txtPassword.Text, hash_guardado, salt_guardado))
                {
                    lbl1.Text = "Se inicio sesion";
                    //Response.Redirect("Turnos.aspx");
                }

                else
                {
                    lbl1.Text = "No se inicio sesion";
                    //Response.Redirect("Login.aspx");
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