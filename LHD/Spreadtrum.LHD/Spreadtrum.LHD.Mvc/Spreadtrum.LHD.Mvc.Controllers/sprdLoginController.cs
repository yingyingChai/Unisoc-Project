namespace Spreadtrum.LHD.Mvc.Controllers
{
    using KaYi.Utilities;
    using Spreadtrum.LHD.Business;
    using Spreadtrum.LHD.Entity.Users;
    using System;
    using System.Web.Mvc;

    public class sprdLoginController : Controller
    {
        public ActionResult Index()
        {
            string SN = base.Request.QueryString["SN"];
            User oSATUserByHashedCID = UserService.GetSPRDUserSN(SN);
            if (oSATUserByHashedCID != null)
            {
                //base.Session.Contents["User"] = oSATUserByHashedCID;
                System.Web.HttpContext.Current.Session["User"] = oSATUserByHashedCID;
                //WebClientOperator.WriteCookies(base.Response, "OSAT_CID", hashedCID, DateTime.Now.AddHours(4.0));
                string nextUrl;
                if (oSATUserByHashedCID.JobType.ToUpper().IndexOf("FT") > -1)
                {
                    ViewBag.Message = "isFT";
                    //nextUrl = "/Lots/Query/LotDispose";
                }
                else
                {
                    ViewBag.Message = "isNoFT";
                    //nextUrl = "/Lots/Transform";
                }
                //base.Response.Redirect(nextUrl);
            }
            else
            {
                ViewBag.Message = "ToLogin";
                //base.Response.Redirect("/Accounts/Login");
            }
            return View();
        }
    }
}

