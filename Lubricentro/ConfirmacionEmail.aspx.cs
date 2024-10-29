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
            if (!IsPostBack)
            {
                // Obtiene el token de la URL
                string token = Request.QueryString["token"];

                if (!string.IsNullOrEmpty(token))
                {
                    // Verifica el token en la base de datos
                    if (ConfirmarEmail(token))
                    {
                        Response.Write("<h2>¡Tu email ha sido confirmado exitosamente!</h2>");
                    }
                    else
                    {
                        Response.Write("<h2>Error: El token es inválido o ya ha sido utilizado.</h2>");
                    }
                }
                else
                {
                    Response.Write("<h2>Error: No se proporcionó un token de confirmación.</h2>");
                }
            }
        }

        private bool ConfirmarEmail(string token)
        {
            bool confirmacionExitosa = false;
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["JOACO_PC"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT COUNT(*) FROM Usuario WHERE Token = @Token AND IsEmailConfirmed = 0";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Token", token);

                try
                {
                    connection.Open();
                    int count = (int)command.ExecuteScalar();

                    if (count > 0)
                    {
                        // Marca el email como confirmado
                        query = "UPDATE Usuario SET IsEmailConfirmed = 1 WHERE Token = @Token";
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
            Response.Redirect("Default.aspx");
        }
    }
}