namespace Spreadtrum.LHD.Mvc.Controllers
{
    using KaYi.Utilities;
    using Spreadtrum.LHD.Mvc;
    using System;
    using System.Configuration;
    using System.Web.Mvc;

    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            string name = base.User.Identity.Name;
            if (ConfigurationManager.AppSettings["ADLogin"] == "1")
            {
                string nextUrl = string.Empty;
                //base.Session.Contents["User"] = AdLoginHelper.TryAdLogin(name, out nextUrl, "Session Start");
                System.Web.HttpContext.Current.Session["User"] = AdLoginHelper.TryAdLogin(name, out nextUrl, "Session Start");
                if (!StringHelper.isNullOrEmpty(nextUrl))
                {
                    base.Response.Redirect(nextUrl);
                }
            }
            else
            {
                //base.Response.Redirect("/Accounts/Login");
                base.Response.Redirect("http://b2b.spreadtrum.com:2012/", true);
            }
            return null;
        }
    }
}

