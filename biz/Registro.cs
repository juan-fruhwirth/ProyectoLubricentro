using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace biz
    {
        public class Registro
        {

            public static bool Confirmar_registro(Usuario usuario)
            {
                if (usuario == null)
                {
                    return false;
                }

                try

                {
                    if (Registro_no_existente(usuario) == false)
                    {
                        return false;
                    }

                    SqlConnection cn = new System.Data.SqlClient.SqlConnection();
                    cn.ConnectionString = ConfigurationManager.ConnectionStrings["SQL"].ToString();
                    cn.Open();

                    string ls_sql = "INSERT INTO Usuarios (UsuarioID, id_nivel, email, contrasenia, nombre, apellido, telefono ) VALUES('" + usuario.nombre_usuario + "','" + usuario.nivel + "','" + usuario.email + "','" + usuario.contrasenia + "','" + usuario.nombre + "','" + usuario.apellido + "','" + usuario.telefono + "')";
                    SqlCommand cmd = new SqlCommand(ls_sql, cn);
                    cmd.CommandType = CommandType.Text;
                    int ls_validar = cmd.ExecuteNonQuery();
                    cn.Close();

                    if (ls_validar > 0)
                    {
                        return true;
                    }
                    else return false;

                }

                catch (Exception e)
                {
                    return false;
                }


            }

            public static bool Registro_no_existente(Usuario usuario)
            {
                try
                {
                    SqlConnection cn = new System.Data.SqlClient.SqlConnection();
                    cn.ConnectionString = ConfigurationManager.ConnectionStrings["SQL"].ToString();
                    cn.Open();

                    string ls_sql = "SELECT 1 FROM Usuario WHERE nombre_usuario = '" + usuario.nombre_usuario + "' OR email = '" + usuario.email + "'";
                    SqlCommand cmd = new SqlCommand(ls_sql, cn);
                    cmd.CommandType = CommandType.Text;
                    string ls_validar = cmd.ExecuteScalar().ToString();
                    cn.Close();
                    if (ls_validar == "1")
                    {
                        return true;
                    }
                    else return false;

                }
                catch (Exception e)
                {
                    return false;
                }
            }



        }
    }
}
}
