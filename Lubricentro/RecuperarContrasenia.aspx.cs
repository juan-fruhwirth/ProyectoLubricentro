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
            if (Session["usuario"] == null) {
                Response.Redirect("Login.aspx");
             }

            else
            {
                string path = HttpContext.Current.Request.Url.AbsolutePath;
                path = path.Substring(1) + ".aspx";
                string li_valida = biz.Validacion.validar_nivel_sitio(path, Session["nivel"].ToString());// despues cambiar a int
                if (li_valida != "1")
                {
                    Response.Redirect("NoTienePermiso.aspx");
                }
             }

        }

        protected void btnResetPassword_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text;

            // Si el correo existe en nuestra base de datos, se enviará un enlace para restablecer su contraseña
            lblMessage.Text = "Se le a enviado un correo electronico con las indicaciones para continuar.";
        }
    }
}