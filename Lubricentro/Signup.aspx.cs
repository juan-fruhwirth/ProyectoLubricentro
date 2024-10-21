using biz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Lubricentro
{
    public partial class Signup : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Registrarse(object sender, EventArgs e)
        {
            if (inputContraseña.Text == inputConfirmarContraseña.Text)
            {
                Usuario.Alta(inputCorreo.Text, int.Parse(inputTelefono.Text), inputApellido.Text, inputNombre.Text, 0);
                Response.Redirect("ConfirmacionEmail.aspx");
            }
        }
    }
}