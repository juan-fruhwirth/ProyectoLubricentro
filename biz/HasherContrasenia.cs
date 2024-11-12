using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace biz
{

    public static class HasherContrasenia
    {
        public static (string Hash, string Salt) Hashear_contrasenia(string contrasenia)
        {
            // Generar una sal aleatoria
            var salt = GenerarSalt();

            // Usar PBKDF2 para hacer el hash
            using (var pbkdf2 = new Rfc2898DeriveBytes(contrasenia, Convert.FromBase64String(salt), 10000))
            {
                var hashBytes = pbkdf2.GetBytes(20); // Derivar una clave de 20 bytes
                var hash = Convert.ToBase64String(hashBytes);
                return (hash, salt);
            }
        }

        private static string GenerarSalt()
        {
            var saltBytes = new byte[16];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(saltBytes);
            }
            return Convert.ToBase64String(saltBytes);
        }



        public static bool VerificarContrasenia(string contrasenia_ingresada, string Hash_guardado, string Salt_guardado)
        {
            using (var pbkdf2 = new Rfc2898DeriveBytes(contrasenia_ingresada, Convert.FromBase64String(Salt_guardado), 10000))
            {
                var hashBytes = pbkdf2.GetBytes(20); // Derivar la clave de 20 bytes
                var hash = Convert.ToBase64String(hashBytes);
                return hash == Hash_guardado;
            }
        }


        public static bool Subir_contraseña_SQL(Contrasenia contrasenia)
        {

            try
            {
                SqlConnection cn = new System.Data.SqlClient.SqlConnection();
                cn.ConnectionString = ConfigurationManager.ConnectionStrings["JUAN-LAPTOP"].ToString();
                cn.Open();

                string ls_sql = "INSERT INTO Contraseñas (ContraseñaHash, ContraseñaSalt, UsuarioID) VALUES('" + contrasenia.hash + "','" + contrasenia.salt+ "'," + contrasenia.usuario_id + ")";


                SqlCommand cmd = new SqlCommand(ls_sql, cn);
                cmd.CommandType = CommandType.Text;
                int ls_validar = cmd.ExecuteNonQuery();
                cn.Close();
                return ls_validar > 0;

            }

            catch (Exception e)
            {
                return false;
            }


        }

    }


}