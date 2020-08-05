using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;
using System;
using System.Web;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.User
{
    public partial class Logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HttpCookie cookie = Request.Cookies["loginName"];
            cookie.Expires = DateTime.Now.AddDays(-1);
            Response.Cookies.Add(cookie);
            SessionManager.Logout(Context);

            Response.Redirect("~/Pages/MainPage.aspx");
        }
    }
}