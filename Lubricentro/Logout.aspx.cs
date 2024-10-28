using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Lubricentro
{
    public partial class Logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["usuario"] == null)
                {
                    Response.Redirect("Login.aspx");
                }

                else fn_logout();
            }

        }

        public void fn_logout()
        {
            Session.Clear();
            Session.Abandon();
            Response.AddHeader("Refresh", "5;url=Login.aspx");

        }
    }
}