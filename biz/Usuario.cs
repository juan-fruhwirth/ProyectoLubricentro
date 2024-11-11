using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Security.AccessControl;
using System.Data;

namespace biz
{
    public class Usuario
    {
       
        public Usuario(string correo, string telefono, string nombre, string apellido, string contrasenia_ingresada, int id_usuario = -1, int nivel = 1, bool confirmado= false )
        {
            this.id_usuario = id_usuario;
            this.correo = correo;
            this.telefono = telefono;
            this.nombre = nombre;
            this.apellido = apellido;
            this.contrasenia = new Contrasenia(contrasenia_ingresada);
            this.nivel = nivel;
            this.confirmado = confirmado;
        }
        public Usuario() {
            this.id_usuario = 1;
            this.correo = "JohnDoe@gmail.com";
            this.telefono = "1188883333";
            this.nombre = "John";
            this.apellido = "Doe";
            this.nivel = 2;
            this.confirmado = true;
        }

        public int id_usuario { get; set; }
        public string telefono { get; set; }
        public string correo { get; set; }
        public Contrasenia contrasenia { get; set; }
        public string nombre { get; set; }
        public string apellido {  get; set; }
        public int nivel {  get; set; }
        public bool confirmado { get; set; }


        public static bool Alta (Usuario usuario)
        {
            SqlConnection cn = new System.Data.SqlClient.SqlConnection();
            cn.ConnectionString = ConfigurationManager.ConnectionStrings["JOACO-PC"].ToString();

            cn.Open();
            try
            {

                if (Registro.Registro_existente(usuario) == true)
                {
                    return false;
                    //return "ERROR, No insertado";
                }


                string ls_sql = "INSERT INTO Usuarios (Correo, Telefono, Nombre, Apellido, NivelUsuario, CorreoConfirmado) " +
                         "VALUES (@correo, @telefono, @nombre, @apellido, @nivel, @correoConfirmado)";

                using (SqlCommand cmd = new SqlCommand(ls_sql, cn))
                {
                    // Define the command type
                    cmd.CommandType = System.Data.CommandType.Text;

                    // Add parameters with appropriate data types
                    cmd.Parameters.AddWithValue("@correo", usuario.correo);
                    cmd.Parameters.AddWithValue("@telefono", usuario.telefono);
                    cmd.Parameters.AddWithValue("@nombre", usuario.nombre);
                    cmd.Parameters.AddWithValue("@apellido", usuario.apellido);
                    cmd.Parameters.AddWithValue("@nivel", usuario.nivel);
                    cmd.Parameters.AddWithValue("@correoConfirmado", usuario.confirmado);

                    // Execute the command
                    cmd.ExecuteNonQuery();
                }



                string ls_sql2 = "SELECT UsuarioID FROM Usuarios WHERE Correo = '" + usuario.correo + "'";
                
                SqlCommand cmd2 = new SqlCommand(ls_sql2, cn);
                cmd2.CommandType = System.Data.CommandType.Text;
                int id_usuario = Convert.ToInt32(cmd2.ExecuteScalar());
                

                cn.Close();


                usuario.contrasenia.usuario_id = id_usuario;
                return HasherContrasenia.Subir_contraseña_SQL(usuario.contrasenia);

            }
            catch (Exception e)
            {
                cn.Close();
                throw e;
            }
        }

        public static bool confirmarExisteUsuario(string correo)
        {
            SqlConnection cn = new System.Data.SqlClient.SqlConnection();
            cn.ConnectionString = ConfigurationManager.ConnectionStrings["JOACO-PC"].ToString();
            try
            {
                cn.Open();
                string ls_sql = "SELECT UsuarioID FROM Usuarios WHERE Correo = @Correo";
                SqlCommand cmd = new SqlCommand(ls_sql, cn);
                cmd.CommandType = System.Data.CommandType.Text;
                object result = cmd.ExecuteScalar();
                cn.Close();

                // Retorna verdadero si el usuario existe (result no es null)
                return result != null;
            }
            catch (Exception e)
            {
                cn.Close();
                return false;
            }

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
        public static bool Modificacion(Usuario usuario)
        {
            SqlConnection cn = new System.Data.SqlClient.SqlConnection();
            cn.ConnectionString = ConfigurationManager.ConnectionStrings["JOACO-PC"].ToString();
            cn.Open();

            try
            {
                SqlCommand cmd = new SqlCommand($"UPDATE Usuarios SET Telefono = {usuario.telefono}, Apellido = {usuario.apellido}, Nombre = '{usuario.nombre}', NivelUsuario = {usuario.nivel} WHERE Correo = {usuario.correo};", cn);
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
        public static int TraerID(Usuario usuario)
        {
            SqlConnection cn = new System.Data.SqlClient.SqlConnection();
            cn.ConnectionString = ConfigurationManager.ConnectionStrings["JOACO-PC"].ToString();
            cn.Open();
            string query = $"SELECT UsuarioID FROM Usuarios WHERE Correo = '{usuario.correo}'";
            int idUsuario = 0;
            try
            {
                SqlCommand cmd = new SqlCommand(query, cn);
                cmd.CommandType = System.Data.CommandType.Text;
                idUsuario = int.Parse(cmd.ExecuteScalar().ToString());
                cn.Close();
            }
            catch (Exception e)
            {
                cn.Close();
                throw e;
            }
            return idUsuario;
        }
    }
}
