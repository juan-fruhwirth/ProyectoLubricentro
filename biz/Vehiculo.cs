using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace biz
{
    //// OJO ESTO NO ESTA TERMINADO! SIMPLEMENTE COPIÉ Y PEGUE DE LA CLASE USUARIO, FALTA CAMBIAR LOS DATOS!  
    ///
    //public class Vehiculo
    //{
    //    public Vehiculo(string Marca, string modelo, string año, string patente, int tipoDeCombustible, string observaciones)
    //    {
    //        this.marca = marca;
    //        this.modelo = modelo;
    //        this.año = año;
    //        this.patente = patente;
    //        this.contrasenia = new TipoDeCombustible (tipoDeCombustible);
    //        this.nivel = nivel;
    //    }

    //    public string modelo { get; set; }
    //    public string marca { get; set; }
    //    public TipoDeCombustible contrasenia { get; set; }
    //    public string año { get; set; }
    //    public string patente { get; set; }
    //    public int nivel { get; set; }

    //    public static bool Alta(Vehiculo Vehiculo)
    //    {
    //        SqlConnection cn = new System.Data.SqlClient.SqlConnection();
    //        cn.ConnectionString =
    //        ConfigurationManager.ConnectionStrings["JOACO-PC"].ToString();

    //        cn.Open();
    //        try
    //        {
    //            if (Registro.Registro_existente(Vehiculo) == true)
    //            {
    //                return false;
    //            }

    //            string ls_sql = "INSERT INTO Vehiculos(marca, modelo, año, patente, NivelVehiculo) VALUES ('" + Vehiculo.marca + "', '" + Vehiculo.modelo + "', '" + Vehiculo.año + "', '" + Vehiculo.patente + "', " + Vehiculo.nivel + ");";
    //            SqlCommand cmd = new SqlCommand(ls_sql, cn);
    //            cmd.CommandType = System.Data.CommandType.Text;
    //            cmd.ExecuteNonQuery();
    //            string ls_sql2 = "; SELECT VehiculoID FROM Vehiculos WHERE marca = '" + Vehiculo.marca + "'";
    //            SqlCommand cmd2 = new SqlCommand(ls_sql2, cn);
    //            cmd2.CommandType = System.Data.CommandType.Text;
    //            int id_Vehiculo = Convert.ToInt32(cmd2.ExecuteScalar());

    //            cn.Close();

    //            Vehiculo.contrasenia.Vehiculo_id = id_Vehiculo;
    //            return HasherTipoDeCombustible.Subir_contraseña_SQL(Vehiculo.contrasenia);
    //        }
    //        catch (Exception e)
    //        {
    //            cn.Close();
    //            throw e;
    //        }
    //    }
    //    public static string Baja(Vehiculo Vehiculo)
    //    {
    //        SqlConnection cn = new System.Data.SqlClient.SqlConnection();
    //        cn.ConnectionString = ConfigurationManager.ConnectionStrings["JOACO-PC"].ToString();
    //        cn.Open();
    //        try
    //        {
    //            SqlCommand cmd = new SqlCommand($"DELETE FROM Vehiculos WHERE marca={Vehiculo.marca};", cn);
    //            cmd.CommandType = System.Data.CommandType.Text;
    //            cmd.ExecuteNonQuery();
    //            cn.Close();

    //            return "Eliminado";
    //        }
    //        catch (Exception e)
    //        {
    //            cn.Close();
    //            throw e;
    //        }
    //    }
    //    public static string Modificacion(Vehiculo Vehiculo)
    //    {
    //        SqlConnection cn = new System.Data.SqlClient.SqlConnection();
    //        cn.ConnectionString = ConfigurationManager.ConnectionStrings["JOACO-PC"].ToString();
    //        cn.Open();
    //        try
    //        {
    //            SqlCommand cmd = new SqlCommand($"UPDATE Vehiculos SET modelo = {Vehiculo.modelo}, patente = {Vehiculo.patente}, año = '{Vehiculo.año}', NivelVehiculo = {Vehiculo.nivel} WHERE marca = {Vehiculo.marca};", cn);
    //            cmd.CommandType = System.Data.CommandType.Text;
    //            cmd.ExecuteNonQuery();
    //            cn.Close();

    //            return "Modificado";
    //        }
    //        catch (Exception e)
    //        {
    //            cn.Close();
    //            throw e;
    //        }
    //    }
    //}

}
