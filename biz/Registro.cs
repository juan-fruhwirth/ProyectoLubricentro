using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Net.Mail;

namespace biz
    {
        public class Registro
        {
        private void GuardarTokenEnBaseDeDatos(string email, string token)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["JOACO_PC"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "UPDATE Usuario SET Token = @Token WHERE correo = @Email";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Token", token);
                    command.Parameters.AddWithValue("@Email", email);

                    try
                    {
                        connection.Open(); // Abre la conexión
                        command.ExecuteNonQuery(); // Ejecuta la consulta
                        Console.WriteLine("Token guardado exitosamente en la base de datos.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error al guardar el token en la base de datos: " + ex.Message);
                    }
                }
            }
        }
        private void SendConfirmationEmail(string toEmail, string token)
        {
            // Genera un token único para el usuario
            string userToken = Guid.NewGuid().ToString();

            // Guarda el token en la base de datos asociado al usuario (asegúrate de que este método exista y funcione)
            GuardarTokenEnBaseDeDatos(toEmail, userToken);

            // Crea el enlace de confirmación con el token
            string confirmationLink = $"http://tuwebsite.com/ConfirmacionEmail.aspx?token={userToken}";

            // Asunto del email
            string subject = "Confirma tu cuenta";

            // Cuerpo del mensaje de confirmación, incluyendo el enlace
            string body = $"Por favor, confirma tu cuenta haciendo clic en el siguiente enlace: <a href='{confirmationLink}'>Confirmar Email</a>";

            // Crear y configurar el mensaje de correo
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("lubricentropelamedina@gmail.com");  // Correo configurado en Web.config
            mailMessage.To.Add(toEmail);
            mailMessage.Subject = subject;
            mailMessage.Body = body;
            mailMessage.IsBodyHtml = true;  // Permitir HTML en el cuerpo

            // Configurar SMTP y enviar el correo
            SmtpClient smtpClient = new SmtpClient();
            smtpClient.EnableSsl = true;

            try
            {
                smtpClient.Send(mailMessage);  // Enviar el email
                Console.WriteLine("Correo enviado correctamente");
            }
            catch (SmtpException ex)
            {
                Console.WriteLine("Error al enviar email: " + ex.Message);
            }
        }

        public static bool Registro_existente(Usuario usuario)
        {
            try
            {
                SqlConnection cn = new System.Data.SqlClient.SqlConnection();
                cn.ConnectionString = ConfigurationManager.ConnectionStrings["JUAN_LAPTOP"].ToString();
                cn.Open();

                string ls_sql = "SELECT isnull(count(UsuarioID), 0) FROM Usuarios WHERE Correo = '" + usuario.correo + "'";
                SqlCommand cmd = new SqlCommand(ls_sql, cn);
                cmd.CommandType = CommandType.Text;
                string ls_validar = cmd.ExecuteScalar().ToString();
                cn.Close();
                if (ls_validar == "1")
                {
                    return true;
                }

                else return false;

            }
            catch (Exception e)
            {
                return false;
            }
        }


    }
}

