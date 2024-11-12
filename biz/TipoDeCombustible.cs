using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace biz
{
    public class TipoDeCombustible
    {
        public TipoDeCombustible(int id_tipoDeCombustible)
        {
            this.id_tipoDeCombustible = id_tipoDeCombustible;
            this.nombre = consultarNombre(id_tipoDeCombustible);
        }
        public int id_tipoDeCombustible { get; set; }
        public string nombre { get; set; }
        public static string consultarNombre(int id_tipoDeCombustible)
        {
            SqlConnection cn = new System.Data.SqlClient.SqlConnection();
            cn.ConnectionString = ConfigurationManager.ConnectionStrings["JOACO-PC"].ToString();
            string query = $"SELECT Nombre FROM Combustibles WHERE TipoCombustibleID = {id_tipoDeCombustible}";
            string nombre = "";
            cn.Open();
            try
            {
                SqlCommand cmd = new SqlCommand(query, cn);
                cmd.CommandType = System.Data.CommandType.Text;
                nombre = cmd.ExecuteScalar().ToString();
                cn.Close();
            }
            catch (Exception ex)
            {
                cn.Close();
                Console.WriteLine($"Error al consultar el nombre: {ex.Message}");
            }
            return nombre;
        }
        public static bool Alta(String nombre)
        {
            SqlConnection cn = new System.Data.SqlClient.SqlConnection();
            cn.ConnectionString = ConfigurationManager.ConnectionStrings["JOACO-PC"].ToString();
            string query = $"INSERT INTO Combustibles (Nombre) VALUES ('{nombre}');";
            string exito = "";
            cn.Open();
            try
            {
                SqlCommand cmd = new SqlCommand(query, cn);
                cmd.CommandType = System.Data.CommandType.Text;
                exito = cmd.ExecuteNonQuery().ToString();
                cn.Close();
                if (exito != "0") return true;
                else return false;
            }
            catch (Exception ex)
            {
                cn.Close();
                Console.WriteLine($"Error al consultar el nombre: {ex.Message}");
                return false;
            }
        }
    }
}
