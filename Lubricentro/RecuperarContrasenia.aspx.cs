using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Lubricentro
{
    public partial class RecuperarContraseña : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e) { 
 

        }

        protected void btnResetPassword_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text;

            // Si el correo existe en nuestra base de datos, se enviará un enlace para restablecer su contraseña
            lblMessage.Text = "Se le a enviado un correo electronico con las indicaciones para continuar.";
        }
    }
}