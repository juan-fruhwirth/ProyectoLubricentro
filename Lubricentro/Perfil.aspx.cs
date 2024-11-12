using biz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Lubricentro
{
    public partial class Perfil : System.Web.UI.Page
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
                    int nivel_actual = usuarioActual.nivel;
                    string path = HttpContext.Current.Request.Url.AbsolutePath;
                    path = path.Substring(1) + ".aspx";
                    string li_valida = biz.Validacion.validar_nivel_sitio(path, nivel_actual.ToString());


                    if (usuarioActual.confirmado == false)
                    {
                        Response.Redirect("ConfirmacionEmail.aspx");
                    }

                    /*if (li_valida != "1")
                    {
                        // Response.Redirect("NoTienePermiso.aspx")
                        Response.Redirect("NoTienePermiso.aspx");

                    }
                    */
                }
                CargarDatosUsuario();
            }
        }

            protected void btnGuardar_Click(object sender, EventArgs e)
        {

        }

        private void CargarDatosUsuario()
        {
            // Supongamos que tienes una clase de Usuario con un método para obtener los datos del usuario logueado
            string emailUsuario = User.Identity.Name; // O usa el email del usuario actual
            Usuario usuario = (Usuario)Session["Usuario"];

            if (usuario != null)
            {
                txtNombre.Text = usuario.nombre;
                txtApellido.Text = usuario.apellido;
                txtTelefono.Text = usuario.telefono;
                
            }
        }

        // Método simulado para obtener los datos del usuario desde la base de datos
        /*private Usuario ObtenerDatosUsuario(string email)
        {
            // Aquí va la lógica para obtener los datos del usuario desde la base de datos
            // Retornarías un objeto Usuario con los datos del usuario actual
            return new Usuario
            {
                Nombre = "Juan",
                Apellido = "Perez",
                Telefono = "123456789",
                Email = email
            };
        }
        */

    }
}