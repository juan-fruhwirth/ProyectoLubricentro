using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;
using System.Data.SqlClient;
using biz;

namespace Lubricentro
{
    public partial class ConfirmacionEmail : System.Web.UI.Page
    {
      
        protected void Page_Load(object sender, EventArgs e)
        {
         if (!IsPostBack)
             {
                if (Session["Usuario"] == null)
                {
                    Response.Redirect("Signup.aspx");
         
                }
                
             }
         }

        protected void ConfirmarCodigoClick(object sender, EventArgs e)
        {
            Usuario usuario_actual = (Usuario)Session["Usuario"];

            if (usuario_actual != null)
            {
                try
                {
                    if (ConfirmarEmail(int.Parse(txtCodigo.Text), usuario_actual.id_usuario))
                    {
                        txtResultadoConfirmacion.Text = "Se confirmo el mail exitosamente, ya puede iniciar sesion con su cuenta";
                        usuario_actual.confirmado = true;
                        Response.AddHeader("Refresh", "1;url=Logout.aspx");

                    }
                    else txtResultadoConfirmacion.Text = "Hubo un error en la confirmacion de mail, intentelo nuevamente";

                }

                catch
                {
                    txtResultadoConfirmacion.Text = "Hubo un error en la confirmacion de mail, intentelo nuevamente";
                }
            }




        }

        protected void ReenviarCodigoClick(object sender, EventArgs e)
        {
            Usuario usuario_actual = (Usuario)Session["Usuario"];
            int codigo_actual = (Registro.SendConfirmationEmail(usuario_actual.correo));
            if (codigo_actual != -1)
            {
                if (Registro.GuardarCodigoEnBaseDeDatos(usuario_actual.id_usuario, codigo_actual))
                {
                    Label3.Text = "Se reenvio un nuevo codigo a tu mail";
                }
                else Label3.Text = "No se pudo reenviar el nuevo codigo";

            }
        }


         private bool ConfirmarEmail(int codigo, int id_usuario )
         {
             bool confirmacionExitosa = false;
             string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["BDD-ONLINE"].ConnectionString;

             using (SqlConnection connection = new SqlConnection(connectionString))
             {
                 string query = "SELECT ISNULL(COUNT(*), 0) FROM Codigos WHERE Codigo = @Codigo AND UsuarioID = @UsuarioID AND Expiracion > GETDATE()";
                 SqlCommand command = new SqlCommand(query, connection);
                 command.Parameters.AddWithValue("@Codigo", codigo);
                 command.Parameters.AddWithValue("@UsuarioID", id_usuario);

                try
                 {
                     connection.Open();
                     int count = (int)command.ExecuteScalar();

                     if (count > 0)
                     {
                         // Marca el email como confirmado
                         query = "UPDATE Usuarios SET CorreoConfirmado = 1 WHERE UsuarioID = @UsuarioID";
                         command.CommandText = query;
                         command.ExecuteNonQuery();
                         confirmacionExitosa = true;
                     }
                 }
                 catch (Exception ex)
                 {
                     Console.WriteLine("Error al confirmar el email: " + ex.Message);
                 }
             }

             return confirmacionExitosa;
             
            
        }
        


    }
}