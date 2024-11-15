using biz;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Lubricentro
{
    public partial class Turnos : System.Web.UI.Page
    {
        // agregar = new Usuario() para ingresar como John Doe
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

                    if (li_valida != "1" & li_valida != "True")
                    {
                        Response.Redirect("NoTienePermiso.aspx");

                    }


                }
                CargarVehiculos();
                CargarServicios();
                CargarTurnos();
                usuarioActual = (Usuario)Session["Usuario"];
            }
        }
        
        private void CargarTurnos()
        {
            usuarioActual = (Usuario)Session["Usuario"];
            int usuarioID = usuarioActual.id_usuario;
            string connectionString = ConfigurationManager.ConnectionStrings["BDD-ONLINE"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
            SELECT t.TurnoID, t.FechaHora AS FechaHora, et.Nombre AS Estado, s.Nombre AS Servicio, s.Precio, v.Patente AS Vehiculo
            FROM Turnos t
            JOIN EstadoTurnos et ON t.EstadoTurnoID = et.EstadoTurnoID
            JOIN Servicios s ON t.ServicioID = s.ServicioID
            JOIN Vehiculos v ON t.VehiculoID = v.VehiculoID
            WHERE t.UsuarioID = @UsuarioID";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UsuarioID", usuarioID);
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    gridTurnos.DataSource = reader;
                    gridTurnos.DataBind();
                    conn.Close();
                }
            }
        }


        private void CargarVehiculos()
        {
            usuarioActual = (Usuario)Session["Usuario"];
            string connectionString = ConfigurationManager.ConnectionStrings["BDD-ONLINE"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT VehiculoID, Patente FROM Vehiculos WHERE UsuarioId = @UsuarioId";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UsuarioId", usuarioActual.id_usuario);

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    inputVehiculo.Items.Clear();

                    inputVehiculo.Items.Add(new ListItem("Seleccione su vehículo", ""));

                    inputVehiculo.DataSource = reader;
                    inputVehiculo.DataValueField = "VehiculoID"; 
                    inputVehiculo.DataTextField = "Patente"; 
                    inputVehiculo.DataBind();
                    connection.Close();
                }
            }
        }

        private void CargarServicios()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["BDD-ONLINE"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT ServicioID, Nombre FROM Servicios";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    inputServicio.Items.Clear();

                    inputServicio.Items.Add(new ListItem("Seleccione un servicio", ""));

                    inputServicio.DataSource = reader;
                    inputServicio.DataValueField = "ServicioID";
                    inputServicio.DataTextField = "Nombre";
                    inputServicio.DataBind();
                    connection.Close();
                }
            }
        }

        protected void RegistrarTurno(object sender, EventArgs e)
        {
            usuarioActual = (Usuario)Session["Usuario"];
            int usuarioID = usuarioActual.id_usuario;
            int vehiculoID = int.Parse(inputVehiculo.SelectedValue);
            int servicioID = int.Parse(inputServicio.SelectedValue);
            int estadoTurnoID = 1;
            DateTime fechaHora = DateTime.Parse(inputFechaHora.Text);

            string connectionString = ConfigurationManager.ConnectionStrings["BDD-ONLINE"].ToString();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Turnos (UsuarioID, VehiculoID, ServicioID, FechaHora, EstadoTurnoID) " +
                               "VALUES (@UsuarioID, @VehiculoID, @ServicioID, @FechaHora, @EstadoTurnoID)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UsuarioID", usuarioID);
                    cmd.Parameters.AddWithValue("@VehiculoID", vehiculoID);
                    cmd.Parameters.AddWithValue("@ServicioID", servicioID);
                    cmd.Parameters.AddWithValue("@FechaHora", fechaHora);
                    cmd.Parameters.AddWithValue("@EstadoTurnoID", estadoTurnoID);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            lblStatusTurno.Text = "Turno registrado exitosamente.";
            lblStatusTurno.CssClass = "alert-success";
            CargarTurnos();
        }

        protected void gridTurnos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int turnoID = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "Eliminar")
            {
                EliminarTurno(turnoID);
                CargarTurnos();
            }
        }
        protected void EliminarTurno(int turnoID) 
        {    
            string connectionString = ConfigurationManager.ConnectionStrings["BDD-ONLINE"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Turnos WHERE TurnoID = @TurnoID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@TurnoID", turnoID);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        protected void calendarFechaHora_SelectionChanged(object sender, EventArgs e)
        {
            inputFechaHora.Text = calendarFechaHora.SelectedDate.ToString("yyyy-MM-dd");
        }
    }
}