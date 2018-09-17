namespace Spreadtrum.LHD.Mvc.Areas.Users.Controllers
{
    using KaYi.Utilities;
    using Microsoft.CSharp.RuntimeBinder;
    using Newtonsoft.Json;
    using Spreadtrum.LHD.Business;
    using Spreadtrum.LHD.Entity.Users;
    using Spreadtrum.LHD.Mvc.Areas.Shared;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Web.Mvc;

    public class UserManagerController : BaseController
    {
        public ActionResult DisableUsers(string[] chkUserID)
        {
            foreach (string str in chkUserID)
            {
                if (str != BaseController.CurrentUserInfo.UserID)
                {
                    UserService.DisableUser(str);
                }
            }
            return null;
        }

        public ActionResult DisplayUsersByRole(int pageSize, int pageIndex, string hidCurrentRole,string selJobType)
        {
            int recordCount = 0;
            string email = base.Request.QueryString["txtEmail"];
            string role = base.Request.QueryString["cmbRoleText"];
            string accountState = base.Request.QueryString["cmbAccountState"];
            switch (accountState)
            {
                case "":
                case null:
                    accountState = "Active";
                    break;
            }
            IList<User> list = UserService.GetUsersBy(base.Request.QueryString["txtFullName"], email, role, selJobType, accountState, pageIndex, pageSize, out recordCount);
            string s = JsonConvert.SerializeObject(new { currentPage = pageIndex, totalPages = PagerUtility.GetPageCount(recordCount, pageSize), rows = list });
            base.Response.Write(s);
            return null;
        }

        public ActionResult Index()
        {
            return base.View("Index");
        }

        public ActionResult OSAT(int pageIndex = 0, int pageSize = 10)
        {
            if (o__5.p__0 == null)
            {
                CSharpArgumentInfo[] argumentInfo = new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null), CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant | CSharpArgumentInfoFlags.UseCompileTimeType, null) };
                o__5.p__0 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Title", typeof(UserManagerController), argumentInfo));
            }
            o__5.p__0.Target(o__5.p__0, base.ViewBag, "OSAT User");
            return base.View("Index");
        }

        public ActionResult PC(int pageIndex = 0, int pageSize = 10)
        {
            if (o__2.p__0 == null)
            {
                CSharpArgumentInfo[] argumentInfo = new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null), CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant | CSharpArgumentInfoFlags.UseCompileTimeType, null) };
                o__2.p__0 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Title", typeof(UserManagerController), argumentInfo));
            }
            o__2.p__0.Target(o__2.p__0, base.ViewBag, "PC User");
            return base.View("Index");
        }

        public ActionResult PE(int pageIndex = 0, int pageSize = 10)
        {
            if (o__3.p__0 == null)
            {
                CSharpArgumentInfo[] argumentInfo = new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null), CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant | CSharpArgumentInfoFlags.UseCompileTimeType, null) };
                o__3.p__0 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Title", typeof(UserManagerController), argumentInfo));
            }
            o__3.p__0.Target(o__3.p__0, base.ViewBag, "PE User");
            return base.View("Index");
        }

        public ActionResult QA(int pageIndex = 0, int pageSize = 10)
        {
            if (o__4.p__0 == null)
            {
                CSharpArgumentInfo[] argumentInfo = new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null), CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant | CSharpArgumentInfoFlags.UseCompileTimeType, null) };
                o__4.p__0 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Title", typeof(UserManagerController), argumentInfo));
            }
            o__4.p__0.Target(o__4.p__0, base.ViewBag, "QA User");
            return base.View("Index");
        }

        [CompilerGenerated]
        private static class o__2
        {
            public static CallSite<Func<CallSite, object, string, object>> p__0;
        }

        [CompilerGenerated]
        private static class o__3
        {
            public static CallSite<Func<CallSite, object, string, object>> p__0;
        }

        [CompilerGenerated]
        private static class o__4
        {
            public static CallSite<Func<CallSite, object, string, object>> p__0;
        }

        [CompilerGenerated]
        private static class o__5
        {
            public static CallSite<Func<CallSite, object, string, object>> p__0;
        }
    }
}

