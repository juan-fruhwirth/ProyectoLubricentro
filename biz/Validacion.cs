using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace biz
{
    public class Validacion
    {
        public static string validar_nivel_sitio(string url, string nivel)
        {
            SqlConnection cn = new System.Data.SqlClient.SqlConnection();
            cn.ConnectionString = ConfigurationManager.ConnectionStrings["JOACO-LAPTOP"].ToString();


            try
            {
                cn.Open();
                string ls_sql = "SELECT EstadoID FROM PermisosNivel WHERE NivelUsuario = @nivel_usuario AND Pagina = @url";

                SqlCommand cmd = new SqlCommand(ls_sql,cn);
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@nivel_usuario", nivel);
                cmd.Parameters.AddWithValue("@url", url);
                object result = cmd.ExecuteScalar();
                string validacion = result != null ? result.ToString() : "No se encontró coincidencia";
                //string validacion = cmd.ExecuteScalar().ToString();//despues borrar
                //int validacion = Convert.ToInt32(cmd.ExecuteScalar());
                cn.Close();
                return validacion;
            }

            catch (Exception e)
            {
                return e.ToString();
            }
        }
    }
}
