using biz;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq.Expressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Lubricentro
{
    public partial class Admin : System.Web.UI.Page
    {
        private string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["JOACO-LAPTOP"].ConnectionString;
        private Usuario usuarioActual;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["Usuario"] == null)
                {
                    Response.Redirect("Login.aspx");
                }
                else
                {

                    usuarioActual = (Usuario)Session["Usuario"];
                    int nivel_actual = usuarioActual.nivel;
                    string path = HttpContext.Current.Request.Url.AbsolutePath;
                    path = path.Substring(1) + ".aspx";
                    string li_valida = biz.Validacion.validar_nivel_sitio(path, nivel_actual.ToString());


                    if (usuarioActual.confirmado == false)
                    {
                        Response.Redirect("ConfirmacionEmail.aspx");
                    }

                    if (li_valida != "True")
                    {
                        // Response.Redirect("NoTienePermiso.aspx")
                        Response.Redirect("NoTienePermiso.aspx");

                    }
                }

                CargarPermisos();
                CargarPermisos2();
            }
        }

        private void CargarPermisos()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string selectQuery = @"
                    SELECT UsuarioID, NivelUsuario, Nombre, Correo 
                    FROM Usuarios";

                    SqlCommand command = new SqlCommand(selectQuery, connection);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    UsuariosGridView.DataSource = dataTable;
                    UsuariosGridView.DataBind();
                }
            }
            catch (Exception e)
            {
                Console.Write(e);
                return;
            }
           
        }



        protected void UsuariosGridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            UsuariosGridView.EditIndex = e.NewEditIndex;
            CargarPermisos();
        }

        protected void UsuariosGridView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            UsuariosGridView.EditIndex = -1;
            CargarPermisos();
        }

        protected void UsuariosGridView_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                // Asegurarse de que el índice esté dentro del rango de filas.
                if (e.RowIndex >= 0 && e.RowIndex < UsuariosGridView.Rows.Count)
                {
                    // Asigno el valor del Correo, que ahora es el data key
                    string correoUsuario = (UsuariosGridView.DataKeys[e.RowIndex].Values["Correo"]).ToString();

                    // Asigno el valor del nuevo nivel seleccionado en el dropdown
                    DropDownList nivelDropDownList = (DropDownList)UsuariosGridView.Rows[e.RowIndex].FindControl("NivelDropDownList");
                    int nuevoNivelAutorizacion = int.Parse(nivelDropDownList.SelectedValue);

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        string updateQuery = "UPDATE Usuarios SET NivelUsuario = @NuevoNivelUsuario WHERE Correo = @Correo";
                        SqlCommand command = new SqlCommand(updateQuery, connection);
                        command.Parameters.AddWithValue("@NuevoNivelUsuario", nuevoNivelAutorizacion); // Cambia a @NuevoNivelAutorizacion
                        command.Parameters.AddWithValue("@Correo", correoUsuario);

                        command.ExecuteNonQuery();

                    }

                    UsuariosGridView.EditIndex = -1;
                    CargarPermisos(); // Asegúrate de que este método recargue el GridView correctamente
                }
            }

            catch (Exception error)
            {
                Console.WriteLine(error);
                UsuariosGridView.EditIndex = -1;
                CargarPermisos();

            }
           
        }


        private void CargarPermisos2()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string selectQuery = @"
                    SELECT NivelUsuario, Pagina, EstadoID
                    FROM PermisosNivel";

                    SqlCommand command = new SqlCommand(selectQuery, connection);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    NivelesGridView.DataSource = dataTable;
                    NivelesGridView.DataBind();
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                return;
            }

        }



        protected void NivelesGridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            NivelesGridView.EditIndex = e.NewEditIndex;
            CargarPermisos2();
        }

        protected void NivelesGridView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            NivelesGridView.EditIndex = -1;
            CargarPermisos2();
        }

        protected void NivelesGridView_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                // Asegurarse de que el índice esté dentro del rango de filas.
                if (e.RowIndex >= 0 && e.RowIndex < NivelesGridView.Rows.Count)
                {
                    // Asigno el valor del Correo, que ahora es el data key
                    int nivel_usuario = Convert.ToInt32(NivelesGridView.DataKeys[e.RowIndex].Values["NivelUsuario"]);
                    string pagina = (NivelesGridView.DataKeys[e.RowIndex].Values["Pagina"]).ToString();

                    // Asigno el valor del nuevo nivel seleccionado en el dropdown
                    DropDownList estadoIDDropDownList = (DropDownList)NivelesGridView.Rows[e.RowIndex].FindControl("EstadoIDDropdownList");
                    int nuevoEstadoID = int.Parse(estadoIDDropDownList.SelectedValue);

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        string updateQuery = "UPDATE PermisosNivel SET EstadoID = @NuevoEstadoID WHERE NivelUsuario = @NivelUsuario AND Pagina = @Pagina";
                        SqlCommand command = new SqlCommand(updateQuery, connection);
                        command.Parameters.AddWithValue("@NuevoEstadoID", nuevoEstadoID); // Cambia a @NuevoNivelAutorizacion
                        command.Parameters.AddWithValue("@NivelUsuario", nivel_usuario);
                        command.Parameters.AddWithValue("@Pagina", pagina);

                        command.ExecuteNonQuery();

                    }

                   NivelesGridView.EditIndex = -1;
                    CargarPermisos2(); // Asegúrate de que este método recargue el GridView correctamente
                }
            }

            catch(Exception error)
            {
                Console.WriteLine(error);
                NivelesGridView.EditIndex = -1;
            }
           
        }
        

    }
}
