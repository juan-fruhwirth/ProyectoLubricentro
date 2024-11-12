using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace biz
{
    public class Codigo
    {

        public Codigo(int id_usuario)

        {
            
        }

        public int id_codigo { get; set; }
        public int codigo { get; set; }
        public int id_usuario { get; set; }

        public static bool Baja(int id_usuario)
        {
            SqlConnection cn = new System.Data.SqlClient.SqlConnection();
            cn.ConnectionString = ConfigurationManager.ConnectionStrings["JOACO-PC"].ToString();

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
    }
}
