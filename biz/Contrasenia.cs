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


        public static bool cambiarContrasenia(string correo, string nueva_contrasenia)
        {
            if (Usuario.confirmarExisteUsuario(correo) == false)
            {
                return false;
            }
            else return true;
        }

        public static bool Baja(Usuario usuario)
        {
            SqlConnection cn = new System.Data.SqlClient.SqlConnection();
            cn.ConnectionString = ConfigurationManager.ConnectionStrings["JOACO-PC"].ToString();

            cn.Open();
            try
            {
                SqlCommand cmd = new SqlCommand($"DELETE FROM Usuarios WHERE Correo={usuario.correo};", cn);
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
