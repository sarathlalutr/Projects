using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Encryption;
using Libraries;
using UserDetails;

namespace NBAD
{
    public partial class Logon : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string refererUrl = "";
            if (Request.QueryString["ReturnURL"] != null)
            {
                refererUrl = Request.QueryString["ReturnURL"];
            }

            //Check to see if user was redirected because of Timeout or initial login
            //Where "Default.aspx" is the default page for your application
            if (refererUrl != "" || refererUrl != "%2f")
            {
                //Show HTML etc showing session timeout message 
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert",
                    "showAlert('<h3>Your session expires. Please login to continue</h3>', 'error', 'top');", true);
            }
        }

        protected void Login1_Authenticate(object sender, AuthenticateEventArgs e)
        {
            var user = new UserManager();

            if (user.authenticateUser(Login1.UserName, encryptSHA1.GetSHA1HashData(Login1.Password)))
            {
                var authUser = new UserEntity();
                authUser = user.getUserDetails(Login1.UserName, encryptSHA1.GetSHA1HashData(Login1.Password));
                //Session["userid"] = authUser.userID;
                Session["username"] = Login1.UserName;
                Session["role"] = authUser.role;
                //Session["email"] = authUser.email == "" ? "No Email" : authUser.email;
                //Session["EmployeeName"] = authUser.EmployeeName;
                //Session["ContactNo"] = authUser.ContactNo;

                if (authUser.role == "admin")
                {
                //    Session["BuildingDetailsID"] = null;
                //    Session["BuildingName"] = "Administrator";
                }
                else
                {
                    //Session["BuildingDetailsID"] = authUser.BuildingDetailsID;
                    //Session["BuildingName"] = authUser.BuildingName;
                }
                // Create forms authentication ticket

                var ticket = new FormsAuthenticationTicket
                    (
                    1 // Ticket version
                    ,
                    Login1.UserName // Username to be associated with this ticket
                    ,
                    DateTime.Now // Date/time ticket was issued
                    ,
                    DateTime.Now.AddMinutes(120) // Date and time the cookie will expire
                    ,
                    Login1.RememberMeSet // if user has chcked rememebr me then create persistent cookie
                    ,
                    authUser.role // store the user data, in this case roles of the user
                    ,
                    FormsAuthentication.FormsCookiePath
                    // Cookie path specified in the web.config file in <Forms> tag if any.
                    );

                // To give more security it is suggested to hash it

                string hashCookies = FormsAuthentication.Encrypt(ticket);

                var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, hashCookies); // Hashed ticket

                // Add the cookie to the response, user browser

                Response.Cookies.Add(cookie);

                // Get the requested page from the url

                //string returnUrl = Request.QueryString["ReturnUrl"];

                //// check if it exists, if not then redirect to default page

                //if (returnUrl == null) returnUrl = "~/Handover.aspx";

                //Response.Redirect(returnUrl);

                FormsAuthentication.RedirectFromLoginPage(Login1.UserName, Login1.RememberMeSet);
                //to set persistant cookies
                //Response.Redirect(FormsAuthentication.GetRedirectUrl(Login1.UserName, true));
            }
        }
    }
}