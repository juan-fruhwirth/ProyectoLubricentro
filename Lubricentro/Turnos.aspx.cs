using biz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Lubricentro
{
    public partial class Turnos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["usuario"] == null)
                {
                    Response.Redirect("Login.aspx");
                }
                else
                {
                    
                    string path = HttpContext.Current.Request.Url.AbsolutePath;
                    path = path.Substring(1) + ".aspx";
                    lbl2.Text = path;
                    string li_valida = biz.Validacion.validar_nivel_sitio(path, Session["nivel"].ToString());
                    lbl1.Text = li_valida.ToString();
                    if (li_valida != "1")
                    {
                       // Response.Redirect("NoTienePermiso.aspx")
                        Response.AddHeader("Refresh", "2;url=NoTienePermiso.aspx");

                    }
                }

            }
        }

    }
}