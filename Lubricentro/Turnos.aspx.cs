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
        private Usuario usuarioActual;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }
            usuarioActual = (Usuario)Session["Usuario"];
            if (!IsPostBack)
            {
                CargarVehiculos();
                CargarServicios();
                CargarEstadosTurno();
                CargarTurnos();
            }
        }
        private void CargarTurnos()
        {
            // Código para obtener y enlazar la lista de turnos del usuario actual en gridTurnos
        }

        private void CargarVehiculos()
        {
            // Código para cargar los vehículos en el Dropdown de Vehículos
        }

        private void CargarServicios()
        {
            // Código para cargar los servicios en el Dropdown de Servicios
        }

        private void CargarEstadosTurno()
        {
            // Código para cargar los estados de turno en el Dropdown de EstadoTurno
        }

        protected void RegistrarTurno(object sender, EventArgs e)
        {
            // Validaciones de campos (similar al método de Vehículos)

            int usuarioID = (int)Session["UsuarioID"];
            int vehiculoID = int.Parse(inputVehiculo.SelectedValue);
            int servicioID = int.Parse(inputServicio.SelectedValue);
            int estadoTurnoID = int.Parse(inputEstadoTurno.SelectedValue);
            DateTime fechaHora = DateTime.Parse(inputFechaHora.Text);

            string connectionString = ConfigurationManager.ConnectionStrings["JOACO-PC"].ToString();
            string query = "INSERT INTO Turnos (UsuarioID, VehiculoID, ServicioID, FechaHora, EstadoTurnoID) " +
                           "VALUES (@UsuarioID, @VehiculoID, @ServicioID, @FechaHora, @EstadoTurnoID)";

            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, cn);
                cmd.Parameters.AddWithValue("@UsuarioID", usuarioID);
                cmd.Parameters.AddWithValue("@VehiculoID", vehiculoID);
                cmd.Parameters.AddWithValue("@ServicioID", servicioID);
                cmd.Parameters.AddWithValue("@FechaHora", fechaHora);
                cmd.Parameters.AddWithValue("@EstadoTurnoID", estadoTurnoID);

                cn.Open();
                int result = cmd.ExecuteNonQuery();

                if (result > 0)
                {
                    lblStatusTurno.Text = "Turno registrado exitosamente.";
                    lblStatusTurno.ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    lblStatusTurno.Text = "Error al registrar el turno.";
                    lblStatusTurno.ForeColor = System.Drawing.Color.Red;
                }
            }

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
    }
}