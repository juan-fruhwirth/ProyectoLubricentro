using biz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Lubricentro
{
    public partial class _Default : Page
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
                    

                    if (li_valida != "1")
                    {
                        // Response.Redirect("NoTienePermiso.aspx")
                        Response.AddHeader("Refresh", "0.5;url=NoTienePermiso.aspx");

                    }


                }

            }
        }
    }
}
    