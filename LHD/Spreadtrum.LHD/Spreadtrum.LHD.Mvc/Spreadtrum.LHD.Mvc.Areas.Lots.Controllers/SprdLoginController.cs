namespace Spreadtrum.LHD.Mvc.Areas.Lots.Controllers
{
    using KaYi.Utilities;
    using Newtonsoft.Json;
    using Spreadtrum.LHD.Business;
    using Spreadtrum.LHD.Entity.Lots;
    using Spreadtrum.LHD.Entity.Users;
    using Spreadtrum.LHD.Mvc.Areas.Shared;
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using System.Security.Cryptography;
    using System.Web.Mvc;

    public class SprdLoginController : BaseController
    {
        public ActionResult Index()
        {
            string SN = base.Request.QueryString["SN"];
            User oSATUserByHashedCID = UserService.GetSPRDUserSN(SN);
            if (oSATUserByHashedCID != null)
            {
                System.Web.HttpContext.Current.Session["User"] = oSATUserByHashedCID;
                if (oSATUserByHashedCID.JobType.ToUpper().IndexOf("FT") > -1)
                {
                    ViewBag.Message = "isFT";
                }
                else
                {
                    ViewBag.Message = "isNoFT";
                }
            }
            else
            {
                ViewBag.Message = "ToLogin";
            }
            return View();
        }
    }
}

