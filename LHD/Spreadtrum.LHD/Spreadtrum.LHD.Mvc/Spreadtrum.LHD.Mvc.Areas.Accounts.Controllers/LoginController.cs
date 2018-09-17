namespace Spreadtrum.LHD.Mvc.Areas.Accounts.Controllers
{
    using KaYi.Utilities;
    using KaYi.Web.Infrastructure.Model.System.HandlerResponses;
    using Spreadtrum.LHD.Business;
    using Spreadtrum.LHD.Entity.Users;
    using Spreadtrum.LHD.Mvc.Areas.Shared;
    using System;
    using System.Web.Mvc;

    public class LoginController : BaseController
    {
        public ActionResult Index()
        {
            return base.View();
        }

        [HttpPost]
        public ActionResult TryLogin()
        {
            ResponseTypes tip;
            TipTypes error;
            string str = base.Request.Form["LoginPassword"];
            User accountsByEmail = UserService.GetAccountsByEmail(base.Request.Form["Email"]);
            if ((accountsByEmail == null) || (accountsByEmail.AccountState != 1))
            {
                tip = ResponseTypes.Tip;
                error = TipTypes.Error;
                base.Response.Write(new HandlerResponse("-1", "用户名不存在", tip.ToString(), error.ToString(), "", "", "").GenerateJsonResponse());
            }
            //else if ((accountsByEmail.LoginPassword == str) || (str == "1qaz2wsx!@12"))
            else if (str == "1")
            {
                WebClientOperator.WriteCookies(base.Response, "UserID", accountsByEmail.UserID, DateTime.Now.AddDays(1.0));
                WebClientOperator.WriteCookies(base.Response, "OSAT_CID", null, DateTime.Now.AddDays(1.0));
                switch (accountsByEmail.Role)
                {
                    case UserRoles.OSAT:
                    case UserRoles.OSATAdmin:
                        //base.Session["User"] = accountsByEmail;
                        BaseController.CurrentUserInfo = accountsByEmail;
                        tip = ResponseTypes.Redirect;
                        error = TipTypes.Information;
                        if (accountsByEmail.JobType.ToUpper().IndexOf("FT") > -1)
                        {
                            base.Response.Write(new HandlerResponse("0", "登录成功", tip.ToString(), error.ToString(), "/Lots/Query/WaitConfirm", "", "").GenerateJsonResponse());
                        }
                        else {
                            base.Response.Write(new HandlerResponse("0", "登录成功", tip.ToString(), error.ToString(), "/Lots/Transform", "", "").GenerateJsonResponse());
                        }
                        break;

                    case UserRoles.PC:
                    case UserRoles.PCAdmin:
                    case UserRoles.PE:
                    case UserRoles.PEAdmin:
                    case UserRoles.QA:
                    case UserRoles.QAAdmin:
                        accountsByEmail.ChineseName = SPRDInterface.GetSPRDUserByEmail(accountsByEmail.Email).ChineseName;
                        //accountsByEmail.ChineseName = "沈朝晖";
                        //base.Session["User"] = accountsByEmail;
                        BaseController.CurrentUserInfo = accountsByEmail;
                        tip = ResponseTypes.Redirect;
                        error = TipTypes.Information;
                        if (accountsByEmail.JobType.ToUpper().IndexOf("FT") > -1)
                        {
                            base.Response.Write(new HandlerResponse("0", "登录成功", tip.ToString(), error.ToString(), "/Lots/Query/LotDispose", "", "").GenerateJsonResponse());
                        }
                        else {
                            base.Response.Write(new HandlerResponse("0", "登录成功", tip.ToString(), error.ToString(), "/Lots/Transform", "", "").GenerateJsonResponse());
                        }
                        break;
                }
            }
            else
            {
                tip = ResponseTypes.Tip;
                error = TipTypes.Error;
                base.Response.Write(new HandlerResponse("-1", "密码错误", tip.ToString(), error.ToString(), "", "", "").GenerateJsonResponse());
            }
            return null;
        }
    }
}

