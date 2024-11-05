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

                    /*
                    if (usuarioActual.confirmado == false)
                    {
                        Response.Redirect("ConfirmacionEmail.aspx");
                    }
                    */
                    if (li_valida != "1")
                    {
                        // Response.Redirect("NoTienePermiso.aspx")
                        Response.AddHeader("Refresh", "2;url=NoTienePermiso.aspx");

                    }


                }
                CargarVehiculos();
                CargarServicios();
                CargarTurnos();

            }
        }

        private void CargarTurnos()
        {
            // Código para obtener y enlazar la lista de turnos del usuario actual en gridTurnos

        }

        private void CargarVehiculos()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["JOACO-PC"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Consulta para obtener las patentes según el usuario
                string query = "SELECT VehiculoID, Patente FROM Vehiculos WHERE UsuarioId = @UsuarioId";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UsuarioId", usuarioActual.id_usuario);

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    // Limpia el DropDownList antes de agregar nuevos ítems
                    inputVehiculo.Items.Clear();

                    // Agrega la opción inicial
                    inputVehiculo.Items.Add(new ListItem("Seleccione su vehículo", ""));

                    inputVehiculo.DataSource = reader;
                    inputVehiculo.DataValueField = "VehiculoID"; // Usado para SelectedValue
                    inputVehiculo.DataTextField = "Patente";     // Texto visible en el dropdown
                    inputVehiculo.DataBind();
                    connection.Close();
                }
            }
        }

        private void CargarServicios()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["JOACO-PC"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Consulta para obtener los servicios
                string query = "SELECT ServicioID, Nombre FROM Servicios";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    // Limpia el DropDownList antes de agregar nuevos ítems
                    inputServicio.Items.Clear();

                    // Agrega la opción inicial
                    inputServicio.Items.Add(new ListItem("Seleccione un servicio", ""));

                    // Rellena el DropDownList con los servicios
                    inputServicio.DataSource = reader;
                    inputServicio.DataValueField = "ServicioID"; // Usado para SelectedValue
                    inputServicio.DataTextField = "Nombre";     // Texto visible en el dropdown
                    inputServicio.DataBind();
                    connection.Close();
                }
            }
        }

        protected void RegistrarTurno(object sender, EventArgs e)
        {
            // Validaciones de campos (similar al método de Vehículos)

            int usuarioID = usuarioActual.id_usuario;
            int vehiculoID = int.Parse(inputVehiculo.SelectedValue);
            int servicioID = int.Parse(inputServicio.SelectedValue);
            int estadoTurnoID = 1;
            DateTime fechaHora = DateTime.Parse(inputFechaHora.Text);

            string connectionString = ConfigurationManager.ConnectionStrings["JOACO-PC"].ToString();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Turnos (UsuarioID, VehiculoID, ServicioID, FechaHora, EstadoTurnoID) " +
                               "VALUES (@UsuarioID, @VehiculoID, @ServicioID, @FechaHora, @EstadoTurnoID)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // Asignar parámetros para evitar SQL Injection
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

            // Confirmar el registro y redirigir o mostrar mensaje de éxito
            lblStatusTurno.Text = "Turno registrado exitosamente.";
            lblStatusTurno.CssClass = "alert-success"; // Puedes definir esta clase en CSS para que el mensaje sea verde


            CargarTurnos(); // Recargar lista de turnos
        }

        protected void gridTurnos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int turnoID = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "Modificar")
            {
                // Código para la modificación del turno
            }
            else if (e.CommandName == "Eliminar")
            {
                // Código para la eliminación del turno
            }
        }

        protected void calendarFechaHora_SelectionChanged(object sender, EventArgs e)
        {
            inputFechaHora.Text = calendarFechaHora.SelectedDate.ToString("yyyy-MM-dd");
        }
    }
}