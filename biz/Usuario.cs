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
       

        public Usuario(string correo, string telefono, string nombre, string apellido, string contrasenia_ingresada, int nivel = 1)
        {
            this.correo = correo;
            this.telefono = telefono;
            this.nombre = nombre;
            this.apellido = apellido;
            this.contrasenia = new Contrasenia(contrasenia_ingresada);
            this.nivel = nivel;
        }

        public string telefono { get; set; }
        public string correo { get; set; }
        public Contrasenia contrasenia { get; set; }
        public string nombre { get; set; }
        public string apellido {  get; set; }
        public int nivel {  get; set; }

  

     

        public static bool Alta (Usuario usuario)
        {
            SqlConnection cn = new System.Data.SqlClient.SqlConnection();
            cn.ConnectionString = ConfigurationManager.ConnectionStrings["JUAN_LAPTOP"].ToString();
            cn.Open();
            try
            {

                if (Registro.Registro_existente(usuario) == true)
                {
                    return false;
                    //return "ERROR, No insertado";
                }






                string ls_sql = "INSERT INTO Usuarios (Correo, Telefono, Nombre, Apellido, NivelUsuario) VALUES ('" + usuario.correo + "', '" + usuario.telefono + "' , '" + usuario.nombre + "', '" + usuario.apellido + "', " + usuario.nivel + ");";
                SqlCommand cmd = new SqlCommand(ls_sql, cn);
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.ExecuteNonQuery();

                string ls_sql2 = "SELECT UsuarioID FROM Usuarios WHERE Correo = '" + usuario.correo + "'";
                
                SqlCommand cmd2 = new SqlCommand(ls_sql2, cn);
                cmd2.CommandType = System.Data.CommandType.Text;
                int id_usuario = Convert.ToInt32(cmd2.ExecuteScalar());
                

                cn.Close();


                usuario.contrasenia.usuario_id = id_usuario;
                return HasherContrasenia.Subir_contraseña_SQL(usuario.contrasenia); ;
                //return "Insertado";
            }
            catch (Exception e)
            {
                cn.Close();
                throw e;
            }
        }
        public static string Baja(Usuario usuario)
        {
            SqlConnection cn = new System.Data.SqlClient.SqlConnection();
            cn.ConnectionString = ConfigurationManager.ConnectionStrings["JUAN_LAPTOP"].ToString();
            cn.Open();
            try
            {
                SqlCommand cmd = new SqlCommand($"DELETE FROM Usuarios WHERE Correo={usuario.correo};", cn);
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
        public static string Modificacion(Usuario usuario)
        {
            SqlConnection cn = new System.Data.SqlClient.SqlConnection();
            cn.ConnectionString = ConfigurationManager.ConnectionStrings["JUAN_LAPTOP"].ToString();
            cn.Open();
            try
            {
                SqlCommand cmd = new SqlCommand($"UPDATE Usuarios SET Telefono = {usuario.telefono}, Apellido = {usuario.apellido}, Nombre = '{usuario.nombre}', NivelUsuario = {usuario.nivel} WHERE Correo = {usuario.correo};", cn);
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
