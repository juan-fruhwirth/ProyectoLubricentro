using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;
using System.Data.SqlClient;

namespace Lubricentro
{
    public partial class ConfirmacionEmail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)  // Verifica que la página no se esté recargando por un postback
            {
                // Obtén el token de la URL
                string token = Request.QueryString["token"];

                if (!string.IsNullOrEmpty(token))
                {
                    // Llama a un método para verificar el token y confirmar el email
                    if (ConfirmarEmail(token))
                    {
                        // Si el token es válido y el email fue confirmado
                        Response.Write("<h2>¡Tu email ha sido confirmado exitosamente!</h2>");
                    }
                    else
                    {
                        // Si el token no es válido o ya expiró
                        Response.Write("<h2>Error: El token es inválido o ya ha sido utilizado.</h2>");
                    }
                }
                else
                {
                    Response.Write("<h2>Error: No se ha proporcionado un token de confirmación.</h2>");
                }
            }
        }

        // Método que verifica el token y confirma el email
        private bool ConfirmarEmail(string token)
        {
            bool confirmacionExitosa = false;

            // Cadena de conexión (puedes tomarla desde tu Web.config si lo tienes configurado)
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["JOACO_PC"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT COUNT(*) FROM Users WHERE EmailToken = @Token AND IsEmailConfirmed = 0";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Token", token);

                try
                {
                    connection.Open();
                    int count = (int)command.ExecuteScalar();

                    // Si existe un usuario con el token y no ha confirmado el email
                    if (count > 0)
                    {
                        // Actualiza el registro para marcar el email como confirmado
                        query = "UPDATE Users SET IsEmailConfirmed = 1 WHERE EmailToken = @Token";
                        command.CommandText = query;
                        command.ExecuteNonQuery();

                        confirmacionExitosa = true;
                    }
                }
                catch (Exception ex)
                {
                    // Maneja errores (por ejemplo, logging)
                    Console.WriteLine("Error al confirmar el email: " + ex.Message);
                }
            }

            return confirmacionExitosa;
        }
    }
}