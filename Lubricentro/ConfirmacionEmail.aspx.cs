using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;

namespace Lubricentro
{
    public partial class ConfirmacionEmail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)  // Verifica que la página no se esté recargando por un postback
            {
                // Obtén el token de la URL
                string token = Request.QueryString["token"];

                if (!string.IsNullOrEmpty(token))
                {
                    // Llama a un método para verificar el token y confirmar el email
                    if (ConfirmarEmail(token))
                    {
                        // Si el token es válido y el email fue confirmado
                        Response.Write("<h2>¡Tu email ha sido confirmado exitosamente!</h2>");
                    }
                    else
                    {
                        // Si el token no es válido o ya expiró
                        Response.Write("<h2>Error: El token es inválido o ya ha sido utilizado.</h2>");
                    }
                }
                else
                {
                    Response.Write("<h2>Error: No se ha proporcionado un token de confirmación.</h2>");
                }
            }
        }
    }
    }
}