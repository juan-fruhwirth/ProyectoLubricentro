using biz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Lubricentro
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Usuario"] != null)
            {
                Usuario usuario_actual = (Usuario)Session["Usuario"];
                string ls_correo = usuario_actual.correo;
                lt_login_mp.Text = $"<div>{ls_correo} | <a href='Logout.aspx'>Logout</a></div>";
            }
            else
            {
                lt_login_mp.Text = ""; // Asignar cadena vacía si la sesión es nula
            }
        }
    }
}