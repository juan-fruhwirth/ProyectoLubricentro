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
            mailMessage.To.Add("flores.joaquin@usal.edu.ar");  // El destinatario (email del usuario que se registra)
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

