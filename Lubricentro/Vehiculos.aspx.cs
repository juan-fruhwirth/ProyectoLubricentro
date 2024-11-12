using biz;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Lubricentro
{
    public partial class Vehiculos : System.Web.UI.Page
    {
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
                    if (usuarioActual.confirmado == false)
                    {
                        Response.Redirect("ConfirmacionEmail.aspx");
                    }

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
                CargarTiposDeCombustible();
                CargarMisVehiculos();
            }
        }
        private void CargarMisVehiculos()
        {
            int usuarioID = usuarioActual.id_usuario;
            var vehiculos = ObtenerVehiculosPorUsuarioID(usuarioID);

            gridVehiculos.DataSource = vehiculos;
            gridVehiculos.DataBind();
        }
        private List <Vehiculo> ObtenerVehiculosPorUsuarioID (int usuarioID)
        {

        }
        private void CargarTiposDeCombustible()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["JOACO-PC"].ToString();
            string query = "SELECT TipoCombustibleID, nombre FROM Combustibles";

            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, cn);
                cn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                inputTipoDeCombustible.DataSource = reader;
                inputTipoDeCombustible.DataTextField = "Nombre"; // Campo a mostrar en el dropdown
                inputTipoDeCombustible.DataValueField = "TipoCombustibleID"; // Valor interno (id)
                inputTipoDeCombustible.DataBind();
                reader.Close();
            }
        }
        protected void AñadirVehiculo(object sender, EventArgs e)
        {
            // Limpiar errores anteriores
            lblErrorMarca.Text = "";
            lblErrorModelo.Text = "";
            lblErrorAño.Text = "";
            lblErrorPatente.Text = "";
            lblErrorTipoDeCombustible.Text = "";
            lblErrorObservaciones.Text = "";
            inputMarca.Style["border"] = "";
            inputModelo.Style["border"] = "";
            inputAño.Style["border"] = "";
            inputPatente.Style["border"] = "";
            inputTipoDeCombustible.Style["border"] = "";
            inputObservaciones.Style["border"] = "";
            // Expresiones regulares para validaciones
            string marcaPattern = @"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$"; // Solo letras (con tildes y ñ) y espacios
            string modeloPattern = @"^[a-zA-Z0-9\s\-]+$"; // Letras, números y guiones (para modelos como "CX-5")
            string añoPattern = @"^(19|20)\d{2}$"; // Años entre 1900 y 2099
            string patentePattern = @"^[A-Z]{2}\d{3}[A-Z]{2}$|^[A-Z]{3}\d{3}$"; // Formatos de patente argentina (AAA123 o AA123AA)
            string observacionesPattern = @"^[\w\s\.,;:áéíóúÁÉÍÓÚñÑ\-]+$"; // Letras, números, espacios y ciertos signos de puntuación (punto, coma, punto y coma, guion, etc.)
            bool hayErrores = false;
            // Validar la Marca
            if (!System.Text.RegularExpressions.Regex.IsMatch(inputMarca.Text, marcaPattern))
            {
                lblErrorMarca.Text = "La marca solo puede contener letras (con tildes y ñ) y espacios";
                inputMarca.Style["border"] = "2px solid red";
                hayErrores = true;
            }
            // Validar el Modelo
            if (!System.Text.RegularExpressions.Regex.IsMatch(inputModelo.Text, modeloPattern))
            {
                lblErrorModelo.Text = "El Modelo solo puede contener letras, números y guiones";
                inputModelo.Style["border"] = "2px solid red";
                hayErrores = true;
            }
            // Validar el Año
            if (!System.Text.RegularExpressions.Regex.IsMatch(inputAño.Text, añoPattern))
            {
                lblErrorAño.Text = "El año debe estar entre 1900 y el año actual";
                inputAño.Style["border"] = "2px solid red";
                hayErrores = true;
            }
            // Validar la Patente
            if (!System.Text.RegularExpressions.Regex.IsMatch(inputPatente.Text, patentePattern))
            {
                lblErrorPatente.Text = "La patente debe tener formato de patente argentina";
                inputPatente.Style["border"] = "2px solid red";
                hayErrores = true;
            }
            // Validar las Observaciones
            if (!System.Text.RegularExpressions.Regex.IsMatch(inputObservaciones.Text, observacionesPattern))
            {
                lblErrorObservaciones.Text = "Las observaciones pueden tener letras, números, espacios y signos de puntuación";
                inputObservaciones.Style["border"] = "2px solid red";
                hayErrores = true;
            }
            // Si hay errores, detener la ejecución
            if (hayErrores)
            {
                return;
            }
            usuarioActual = (Usuario)Session["Usuario"];
            Vehiculo vehiculo = new Vehiculo(inputMarca.Text, inputModelo.Text, int.Parse(inputAño.Text), inputPatente.Text, new TipoDeCombustible(int.Parse(inputTipoDeCombustible.Text)), inputObservaciones.Text, usuarioActual);
            if (Vehiculo.Alta(vehiculo))
            {
                lblAñadirVehiculoStatus.Attributes["ForeColor"] = "Green";
                lblAñadirVehiculoStatus.Text = "Exito!";
            }
            else
            {
                lblAñadirVehiculoStatus.Attributes["ForeColor"] = "Red";
                lblAñadirVehiculoStatus.Text = "Falla!";
                return;
            }
        }

        protected void gridVehiculos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Eliminar")
            {
                int vehiculoID = Convert.ToInt32(e.CommandArgument);
                EliminarVehiculo(vehiculoID);
                CargarMisVehiculos();
            }
        }
        private void EliminarVehiculo(int vehiculoID)
        {

        }
    }
}