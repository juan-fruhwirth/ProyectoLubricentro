using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace biz
{
    public class Vehiculo
    {
        public Vehiculo(string marca, string modelo, int año, string patente, TipoDeCombustible tipoDeCombustible, string observaciones, Usuario usuario)
        {
            this.marca = marca;
            this.modelo = modelo;
            this.año = año;
            this.patente = patente;
            this.tipoDeCombustible = tipoDeCombustible;
            this.observaciones = observaciones;
            this.usuario = usuario;
        }

        public string marca { get; set; }
        public string modelo { get; set; }
        public TipoDeCombustible tipoDeCombustible { get; set; }
        public int año { get; set; }
        public string patente { get; set; }
        public string observaciones { get; set; }
        public Usuario usuario { get; set; }

        public static bool Alta(Vehiculo Vehiculo)
        {
            SqlConnection cn = new System.Data.SqlClient.SqlConnection();
            cn.ConnectionString = ConfigurationManager.ConnectionStrings["JUAN-LAPTOP"].ToString();
            cn.Open();
            int usuarioID = Usuario.TraerID(Vehiculo.usuario);
            string query = $"INSERT INTO Vehiculos (Marca, Modelo, Año, Patente, TipoCombustibleID, Observaciones, UsuarioID) VALUES ('{Vehiculo.marca}', '{Vehiculo.modelo}', {Vehiculo.año}, '{Vehiculo.patente}', {Vehiculo.tipoDeCombustible.id_tipoDeCombustible}, '{Vehiculo.observaciones}', '{usuarioID}');";
            string exito = "";
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
                Console.WriteLine($"Error al insertar el vehiculo: {ex.Message}");
                return false;
            }
        }
        public static bool Baja(Vehiculo vehiculo)
        {
            SqlConnection cn = new System.Data.SqlClient.SqlConnection();
            cn.ConnectionString = ConfigurationManager.ConnectionStrings["JUAN-LAPTOP"].ToString();
            cn.Open();
            string query = $"DELETE FROM Vehiculos WHERE Patente = '{vehiculo.patente}';";
            string exito = "";
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
                Console.WriteLine($"Error al eliminar el vehículo: {ex.Message}");
                return false;
            }
        }
        public static bool Modificacion(Vehiculo Vehiculo)
        {
            SqlConnection cn = new System.Data.SqlClient.SqlConnection();
            cn.ConnectionString = ConfigurationManager.ConnectionStrings["JUAN-LAPTOP"].ToString();
            cn.Open();
            string query = $"UPDATE Vehiculos SET Marca = '{Vehiculo.marca}', Modelo = '{Vehiculo.modelo}', Año = {Vehiculo.año}, TipoDeCombustible = {Vehiculo.tipoDeCombustible}, Observaciones = '{Vehiculo.observaciones}' WHERE Patente = '{Vehiculo.patente}';";
            string exito = "";
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
                Console.WriteLine($"Error al modificar el vehículo: {ex.Message}");
                return false;
            }
        }
    }
}