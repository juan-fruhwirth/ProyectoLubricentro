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
using System.Web;

namespace biz
    {
        public class Registro
        {
        public static bool GuardarCodigoEnBaseDeDatos(int id_usuario, int codigo)
        {

            string connectionString = ConfigurationManager.ConnectionStrings["JOACO-PC"].ConnectionString;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Consulta de inserción 
                    connection.Open();
                    string query = "INSERT INTO Codigos (Codigo, UsuarioID) VALUES (@Codigo, @UsuarioID)";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Codigo", codigo);
                    command.Parameters.AddWithValue("@UsuarioID", id_usuario);
                    command.ExecuteNonQuery();

                }
                return true;

            }

            catch
            {
                return false;
            }
            
        }

        public static int SendConfirmationEmail(string toEmail)
        {

            Random random = new Random();
            int codigo = random.Next(100000, 1000000);

            // Asunto del email
            string subject = "Confirma tu cuenta";

            // Cuerpo del mensaje de confirmación, incluyendo el enlace
            string body = $"Por favor, confirma tu cuenta ingresando estos seis digitos en la pagina de confirmacion: {codigo}";

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
                return codigo;
            }
            catch (SmtpException ex)
            {
                Console.WriteLine("Error al enviar email: " + ex.Message);
                return -1;
            }
        }

        public static bool Registro_existente(Usuario usuario)
        {
            try
            {
                SqlConnection cn = new System.Data.SqlClient.SqlConnection();
                cn.ConnectionString = ConfigurationManager.ConnectionStrings["JOACO-PC"].ToString();
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

