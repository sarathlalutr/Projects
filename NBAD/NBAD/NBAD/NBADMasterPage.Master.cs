using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NBAD
{
    public partial class NBADMasterPage : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Cache.SetExpires(DateTime.Now.AddMinutes(-1));
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();
        }
        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Clear(); //this will clear session
            Session.Abandon(); //this will Abandon session
            FormsAuthentication.SignOut();
            Response.Cache.SetExpires(DateTime.Now);
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();

            //FormsAuthentication.RedirectToLoginPage();
            Response.Redirect("Logon.aspx");
        }
    }
}