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

        private void SendConfirmationEmail(string toEmail, string subject, string body)
        {
            // Crea un objeto MailMessage
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("lubricentropelamedina@gmail.com");  // El remitente (email configurado en Web.config)
            mailMessage.To.Add(toEmail);  // El destinatario (email del usuario que se registra)
            mailMessage.Subject = subject;  // Asunto del email
            mailMessage.Body = body;  // Cuerpo del email (puede incluir HTML)
            mailMessage.IsBodyHtml = true;  // Habilita el uso de HTML en el cuerpo

            // Usa SmtpClient para enviar el email, se configurará automáticamente desde Web.config
            SmtpClient smtpClient = new SmtpClient();
            smtpClient.EnableSsl = true;  // Asegúrate de que SSL esté habilitado

            try
            {
                smtpClient.Send(mailMessage);  // Envía el email
            }
            catch (SmtpException ex)
            {
                // Maneja errores al enviar el email
                Console.WriteLine("Error al enviar email: " + ex.Message);
            }
        }
        /*

            public static bool Confirmar_registro(Usuario usuario)
            {
                if (usuario == null)
                {
                    return false;
                }

                try

                {
                    if (Registro_no_existente(usuario) == false)
                    {
                        return false;
                    }

                    SqlConnection cn = new System.Data.SqlClient.SqlConnection();
                    cn.ConnectionString = ConfigurationManager.ConnectionStrings["SQL"].ToString();
                    cn.Open();

                    string ls_sql = "INSERT INTO Usuarios (UsuarioID, id_nivel, email, contrasenia, nombre, apellido, telefono ) VALUES('" + usuario.nombre_usuario + "','" + usuario.nivel + "','" + usuario.email + "','" + usuario.contrasenia + "','" + usuario.nombre + "','" + usuario.apellido + "','" + usuario.telefono + "')";
                    SqlCommand cmd = new SqlCommand(ls_sql, cn);
                    cmd.CommandType = CommandType.Text;
                    int ls_validar = cmd.ExecuteNonQuery();
                    cn.Close();

                    if (ls_validar > 0)
                    {
                        return true;
                    }
                    else return false;
                    string userToken = Guid.NewGuid().ToString();  // Genera un token único para la confirmación del email
                    GuardarTokenEnBaseDeDatos(email, userToken);
                    // Envía el email de confirmación
                    string subject = "Confirmación de cuenta";
                    string confirmationLink = "http://tuwebsite.com/ConfirmacionEmail.aspx?token=" + userToken;
                    string body = $"Gracias por registrarte. Por favor confirma tu email haciendo clic en el siguiente enlace: <a href='{confirmationLink}'>Confirmar Email</a>";

    
                }

                catch (Exception e)
                {
                    return false;
                }


            }

            public static bool Registro_no_existente(Usuario usuario)
            {
                try
                {
                    SqlConnection cn = new System.Data.SqlClient.SqlConnection();
                    cn.ConnectionString = ConfigurationManager.ConnectionStrings["SQL"].ToString();
                    cn.Open();

                    string ls_sql = "SELECT 1 FROM Usuario WHERE nombre_usuario = '" + usuario.nombre_usuario + "' OR email = '" + usuario.email + "'";
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
*/


    }
}

