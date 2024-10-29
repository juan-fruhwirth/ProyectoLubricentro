using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Net.Mail;
using System.Net;

namespace biz
    {
        public class Registro
        {
        public static int GuardarTokenEnBaseDeDatos(string token)
        {
            int idToken = -1; // Variable para almacenar el id_token generado
            string connectionString = ConfigurationManager.ConnectionStrings["JUAN-LAPTOP"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Consulta de inserción para agregar el nuevo token y configurar IsEmailConfirmed en 0
                string query = "INSERT INTO Tokens (Token, IsEmailConfirmed) OUTPUT INSERTED.TokenID VALUES (@Token, 0)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Token", token);

                    try
                    {
                        connection.Open(); // Abre la conexión
                        idToken = (int)command.ExecuteScalar(); // Ejecuta la consulta e inserta el registro

                        Console.WriteLine("Token guardado exitosamente en la base de datos. ID generado: " + idToken);


                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error al guardar el token en la base de datos: " + ex.Message);
                    }
                }

                // mal, esto tiene que primero asignarle el token_id a Usuarios 

                

                string query2 = "UPDATE Usuarios SET CorreoConfirmado = 1 WHERE TokenID = @id_token";

                using (SqlCommand command2 = new SqlCommand(query2, connection))
                {
                    command2.Parameters.AddWithValue("@id_token", idToken);

                    try
                    {
                        connection.Open(); // Abre la conexión
                        command2.ExecuteNonQuery(); // Ejecuta la consulta e inserta el registro
                        connection.Close();

                        Console.WriteLine("Confirmacion de mail cambiado en Usuarios exitosamente");


                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error al cambiar el estado de confirmaciond de mail en Usuarios: " + ex.Message);
                    }
                }
            }

            return idToken; // Devuelve el ID del token generado
        }

        public static string SendConfirmationEmail(string toEmail)
        {
            // Genera un token único para el usuario
            string userToken = Guid.NewGuid().ToString();

            // Guarda el token en la base de datos asociado al usuario (asegúrate de que este método exista y funcione)
            //GuardarTokenEnBaseDeDatos(userToken);

            // Crea el enlace de confirmación con el token
            string confirmationLink = $"http://localhost:44326/ConfirmacionEmail.aspx?token={userToken}";

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
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587); // Cambia el servidor SMTP y puerto según el proveedor de correo
            smtpClient.EnableSsl = true; // Activar SSL
            smtpClient.Credentials = new NetworkCredential("lubricentroelpelamedina@gmail.com", "ates yldn djav cage"); // Asegúrate de que la contraseña sea la correcta
            try
            {
                smtpClient.Send(mailMessage);  // Enviar el email
                Console.WriteLine("Correo enviado correctamente");
                return userToken;
            }
            catch (SmtpException ex)
            {
                Console.WriteLine("Error al enviar email: " + ex.Message);
                return "";
            }
        }

        public static bool Registro_existente(Usuario usuario)
        {
            try
            {
                SqlConnection cn = new System.Data.SqlClient.SqlConnection();
                cn.ConnectionString = ConfigurationManager.ConnectionStrings["JOACO-LAPTOP"].ToString();
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

