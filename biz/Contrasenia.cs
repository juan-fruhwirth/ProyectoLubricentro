using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace biz
{
    public class Contrasenia
    {

        public Contrasenia(string contrasenia_ingresada)

        {
            (this.hash, this.salt) = HasherContrasenia.Hashear_contrasenia(contrasenia_ingresada);
        }

        public int id_contrasenia { get; set; }
        public string hash { get; set; }
        public string salt { get; set; }
        public int usuario_id { get; set; }


        public static bool cambiarContrasenia(string correo, string nueva_contrasenia, int id_usuario)
        {

            try
            {
                if (Usuario.confirmarExisteUsuario(correo) == false)
                {
                    return false;
                }

                Contrasenia contrasenia = new Contrasenia(nueva_contrasenia);
                contrasenia.usuario_id = id_usuario;

                if (HasherContrasenia.Subir_contraseña_SQL(contrasenia) == false)
                {
                    return false;
                }

                if (Baja_especifica(id_usuario, contrasenia.hash) == false)
                {
                    return false;
                }


                return true;
            }

            catch
            {
                return false;
            }
           
        }

        public static bool Baja(int id_usuario)
        {
            SqlConnection cn = new System.Data.SqlClient.SqlConnection();
            cn.ConnectionString = ConfigurationManager.ConnectionStrings["JUAN-LAPTOP"].ToString();

            cn.Open();
            try
            {
                SqlCommand cmd = new SqlCommand($"DELETE FROM Contraseñas WHERE UsuarioID={id_usuario};", cn);
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.ExecuteNonQuery();
                cn.Close();

                return true;
            }
            catch (Exception e)
            {
                cn.Close();
                return false;
            }
        }

        public static bool Baja_especifica(int id_usuario, string hash)
        {

            SqlConnection cn = new System.Data.SqlClient.SqlConnection();
            cn.ConnectionString = ConfigurationManager.ConnectionStrings["JOACO-PC"].ToString();

            cn.Open();
            try
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM Contraseñas WHERE UsuarioID = @UsuarioID AND ContraseñaHash != @Hash", cn);
                cmd.Parameters.AddWithValue("@UsuarioID", id_usuario);
                cmd.Parameters.AddWithValue("@Hash", hash);
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.ExecuteNonQuery();
                cn.Close();

                return true;
            }
            catch (Exception e)
            {
                cn.Close();
                return false;
            }
        }


    }


}
