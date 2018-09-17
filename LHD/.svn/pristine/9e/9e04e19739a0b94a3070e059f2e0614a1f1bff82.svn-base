using KaYi.Services.EventServices;
using KaYi.Utilities;
using KaYi.Web.Infrastructure.Model.System.Urls;
using KaYi.Web.Infrastructure.Services;
using Microsoft.CSharp.RuntimeBinder;
using Spreadtrum.LHD.Business;
using Spreadtrum.LHD.Entity.Users;
using System;
using System.Configuration;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Mvc;
namespace Spreadtrum.LHD.Mvc.Areas.Shared
{
    public class BaseController : Controller
    {
        [CompilerGenerated]
        private static class o__7
        {
            public static CallSite<Func<CallSite, object, KaYi.Web.Infrastructure.Model.System.Urls.Url, object>> p__0;
        }
        public bool needLogin
        {
            get
            {
                return Convert.ToBoolean(ConfigurationManager.AppSettings["needLogin"]);
            }

        }
        public static User CurrentUserInfo
        {
            //get;
            //set;
            get
            {
                var context = System.Web.HttpContext.Current;
                if (context.Session["User"] != null)
                {
                    return (User)context.Session["User"];
                }
                else
                {
                    return null;
                }
            }
            set
            {
                var context = System.Web.HttpContext.Current;
                context.Session["User"] = value;
                context.Session.Timeout = 24 * 60;
            }
        }
        private User TryAdLogin(out string nextUrl)
        {
            return AdLoginHelper.TryAdLogin(base.User.Identity.Name, out nextUrl, "OnActionExecuting");
        }
        private User TryOSATLogin(out string nextUrl)
        {
            User user = null;
            if (base.Request.Cookies["OSAT_CID"] != null && base.Request.Cookies["OSAT_CID"].Value != "")
            {
                string text = base.Request.Cookies["OSAT_CID"].Value.ToString();
                user = UserService.GetOSATUserByHashedCID(text);
                if (user != null)
                {
                    //base.Session.Contents["User"] = user;
                    CurrentUserInfo = user;
                    WebClientOperator.WriteCookies(base.Response, "OSAT_CID", text, DateTime.Now.AddHours(4.0));
                    if (CurrentUserInfo.JobType.ToUpper().IndexOf("FT")>-1)
                    {
                        nextUrl = "/Lots/Query/WaitConfirm";
                    }
                    else {
                        nextUrl = "/Lots/Transform";
                    }
                }
                else
                {
                    nextUrl = "/Accounts/Login";
                }
            }
            else
            {
                nextUrl = "/Accounts/Login";
            }
            return user;
        }
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            base.OnActionExecuting(filterContext);
            if (BaseController.o__7.p__0 == null)
            {
                BaseController.o__7.p__0 = CallSite<Func<CallSite, object, KaYi.Web.Infrastructure.Model.System.Urls.Url, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.SetMember(CSharpBinderFlags.None, "Url", typeof(BaseController), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
				}));
            }
            BaseController.o__7.p__0.Target(BaseController.o__7.p__0, base.ViewBag, UrlService.GetUrlByUrlPath(base.Request.Url.AbsolutePath));
            BaseController.ProcessHttpRequest(base.Request);
            string empty = string.Empty;
            User user;
            //if (base.Session.Contents["User"] == null)

            if (CurrentUserInfo == null)
            {
                EventService.AppendToLogFileToAbsFile(@"C:\LHD_APPLICATION\UserInfo.txt", "Windows Account login:base.Session.Contents[\"User\"] == null\r\n");

                if (ConfigurationManager.AppSettings["ADLogin"] == "1")
                {
                    user = this.TryAdLogin(out empty);
                }
                else
                {
                    user = this.TryOSATLogin(out empty);
                }
                BaseController.CurrentUserInfo = user;
            }
            else
            {
                //user = (User)base.Session.Contents["User"];
                user = CurrentUserInfo;
                string guid = Guid.NewGuid().ToString();
                EventService.AppendToLogFileToAbsFile(@"C:\LHD_APPLICATION\UserInfo.txt", "Windows Account login--" + guid + ":" + user.ThirdpartyAccountID + "\r\n");

                if (user.Role == UserRoles.OSAT || user.Role == UserRoles.OSATAdmin)
                {
                    //user = UserService.GetOSATUserByHashedCID(user.UserID);
                    WebClientOperator.WriteCookies(base.Response, "OSAT_CID", user.UserID, DateTime.Now.AddHours(4.0));
                }
                else
                {
                    //user = UserService.GetUserByThirdPartyAccountID("SPRDUser", user.ThirdpartyAccountID);
                    //user.ChineseName = SPRDInterface.GetSPRDUserByEmail(user.Email).ChineseName;
                    EventService.AppendToLogFileToAbsFile(@"C:\LHD_APPLICATION\UserInfo.txt", "Windows Account login SPRDUser--" + guid + ":" + user.ThirdpartyAccountID + "\r\n");

                }
            }

            //base.Session.Contents["User"] = user;
            //BaseController.CurrentUserInfo = user;
            //if (user == null && this.needLogin)
            if (user == null && this.needLogin)
            {
                base.Response.Redirect("http://b2b.spreadtrum.com:2012/", true);
            }
        }
        private static void AppendKeyValueToForm(HttpRequestBase request, string key, string value)
        {
            request.Form.GetType().GetMethod("MakeReadWrite", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(request.Form, null);
            request.Form.Add(key, value);
        }
        private static void ProcessHttpRequest(HttpRequestBase request)
        {
            try
            {
                string text = request.Form["KaYiData"];
                if (text != null && text != "")
                {
                    string[] array = text.Split(new char[]
					{
						'&'
					});
                    if (array.Length != 0)
                    {
                        for (int i = 0; i <= array.Length - 1; i++)
                        {
                            string[] expr_4E = array[i].Split(new char[]
							{
								'='
							});
                            string key = HttpUtility.UrlDecode(expr_4E[0]);
                            string value = HttpUtility.UrlDecode(expr_4E[1]);
                            BaseController.AppendKeyValueToForm(request, key, value);
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
        }
    }
}
