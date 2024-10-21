using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace biz
{
    public class Usuario
    {
        //public Usuario(string correo, int telefono, string contraseña)
        //{
        //    this.correo = correo;
        //    this.telefono = telefono;
        //    this.contraseña = contraseña;
        //    this.nivel = 1;
        //}

        //private int telefono { get; set; }
        //private string correo { get; set; }
        //private string contraseña { get; set; }
        //private string nombre { get; set; }
        //private string apellido {  get; set; }
        //private int nivel {  get; set; }

        public static string Alta (string correo, int telefono, string apellido, string nombre, int nivel)
        {
            SqlConnection cn = new System.Data.SqlClient.SqlConnection();
            cn.ConnectionString = ConfigurationManager.ConnectionStrings["JOACO-PC"].ToString();
            cn.Open();
            try
            {
                SqlCommand cmd = new SqlCommand($"insert into Usuarios (Correo,Telefono,Apellido,Nombre,NivelUsuario) values('{correo}', {telefono}, '{apellido}', '{nombre}', {nivel});", cn);
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.ExecuteNonQuery();
                cn.Close();

                return "Insertado";
            }
            catch (Exception e)
            {
                cn.Close();
                throw e;
            }
        }
        public static string Baja(int correo)
        {
            SqlConnection cn = new System.Data.SqlClient.SqlConnection();
            cn.ConnectionString = ConfigurationManager.ConnectionStrings["JOACO-PC"].ToString();
            cn.Open();
            try
            {
                SqlCommand cmd = new SqlCommand($"DELETE FROM Usuarios WHERE Correo={correo};", cn);
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.ExecuteNonQuery();
                cn.Close();

                return "Eliminado";
            }
            catch (Exception e)
            {
                cn.Close();
                throw e;
            }
        }
        public static string Modificacion(string correo, int telefono, string apellido, string nombre, int nivel)
        {
            SqlConnection cn = new System.Data.SqlClient.SqlConnection();
            cn.ConnectionString = ConfigurationManager.ConnectionStrings["JOACO-PC"].ToString();
            cn.Open();
            try
            {
                SqlCommand cmd = new SqlCommand($"UPDATE Usuarios SET Telefono = {telefono}, Apellido = {apellido}, Nombre = '{nombre}', NivelUsuario = {nivel} WHERE Correo = {correo};", cn);
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.ExecuteNonQuery();
                cn.Close();

                return "Modificado";
            }
            catch (Exception e)
            {
                cn.Close();
                throw e;
            }
        }
    }

    
}
