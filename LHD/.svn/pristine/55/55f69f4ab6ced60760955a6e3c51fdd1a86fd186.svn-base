namespace Spreadtrum.LHD.Mvc.Controllers
{
    using KaYi.Utilities;
    using Spreadtrum.LHD.Business;
    using Spreadtrum.LHD.Entity.Users;
    using System;
    using System.Web.Mvc;

    public class b2bLoginController : Controller
    {
        public ActionResult Index()
        {
            string hashedCID = base.Request.QueryString["cid"];
            User oSATUserByHashedCID = UserService.GetOSATUserByHashedCID(hashedCID);
            if (oSATUserByHashedCID != null)
            {
                //base.Session.Contents["User"] = oSATUserByHashedCID;
                System.Web.HttpContext.Current.Session["User"] = oSATUserByHashedCID;
                WebClientOperator.WriteCookies(base.Response, "OSAT_CID", hashedCID, DateTime.Now.AddHours(4.0));
                base.Response.Redirect("/Lots/Query/WaitConfirm");
            }
            else
            {
                base.Response.Redirect("/Accounts/Login");
            }
            return null;
        }
    }
}

