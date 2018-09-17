namespace Spreadtrum.LHD.Mvc.Areas.Lots.Controllers
{
    using Microsoft.CSharp.RuntimeBinder;
    using Spreadtrum.LHD.Business;
    using Spreadtrum.LHD.Entity.Lots;
    using Spreadtrum.LHD.Mvc.Areas.Shared;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Web.Mvc;

    public class DetailsController : BaseController
    {
        public ActionResult Index()
        {
            string lotID = base.Request.QueryString["LotID"];
            if (o__0.p__0 == null)
            {
                CSharpArgumentInfo[] argumentInfo = new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null), CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant | CSharpArgumentInfoFlags.UseCompileTimeType, null) };
                o__0.p__0 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Title", typeof(DetailsController), argumentInfo));
            }
            o__0.p__0.Target(o__0.p__0, base.ViewBag, "Details");
            LotView lotViewByLotID = LotService.GetLotViewByLotID(lotID);
            NotificationService.ClearCommentNotificationByUserIDAndLotID(BaseController.CurrentUserInfo.UserID, lotID);
            int recordCount = 0;
            IList<Comment> list = LotService.GetCommentsByLotID(lotID, "OtherBinDispose", false, 0, 0x270f, out recordCount);
            if ((list != null) && (list.Count > 0))
            {
                if (o__0.p__1 == null)
                {
                    CSharpArgumentInfo[] infoArray2 = new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null), CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant | CSharpArgumentInfoFlags.UseCompileTimeType, null) };
                    o__0.p__1 = CallSite<Func<CallSite, object, bool, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "OtherBinDisposeCommentExist", typeof(DetailsController), infoArray2));
                }
                o__0.p__1.Target(o__0.p__1, base.ViewBag, true);
            }
            else
            {
                if (o__0.p__2 == null)
                {
                    CSharpArgumentInfo[] infoArray3 = new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null), CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant | CSharpArgumentInfoFlags.UseCompileTimeType, null) };
                    o__0.p__2 = CallSite<Func<CallSite, object, bool, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "OtherBinDisposeCommentExist", typeof(DetailsController), infoArray3));
                }
                o__0.p__2.Target(o__0.p__2, base.ViewBag, false);
            }
            return base.View(lotViewByLotID);
        }


        [CompilerGenerated]
        public ActionResult mDetailsIndex()
        {
            string lotID = base.Request.QueryString["LotID"];
            if (o__0.p__0 == null)
            {
                CSharpArgumentInfo[] argumentInfo = new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null), CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant | CSharpArgumentInfoFlags.UseCompileTimeType, null) };
                o__0.p__0 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Title", typeof(DetailsController), argumentInfo));
            }
            o__0.p__0.Target(o__0.p__0, base.ViewBag, "Details");
            LotView lotViewByLotID = LotService.GetLotViewByLotID(lotID);
            NotificationService.ClearCommentNotificationByUserIDAndLotID(BaseController.CurrentUserInfo.UserID, lotID);
            int recordCount = 0;
            IList<Comment> list = LotService.GetCommentsByLotID(lotID, "OtherBinDispose", false, 0, 0x270f, out recordCount);
            if ((list != null) && (list.Count > 0))
            {
                if (o__0.p__1 == null)
                {
                    CSharpArgumentInfo[] infoArray2 = new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null), CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant | CSharpArgumentInfoFlags.UseCompileTimeType, null) };
                    o__0.p__1 = CallSite<Func<CallSite, object, bool, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "OtherBinDisposeCommentExist", typeof(DetailsController), infoArray2));
                }
                o__0.p__1.Target(o__0.p__1, base.ViewBag, true);
            }
            else
            {
                if (o__0.p__2 == null)
                {
                    CSharpArgumentInfo[] infoArray3 = new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null), CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant | CSharpArgumentInfoFlags.UseCompileTimeType, null) };
                    o__0.p__2 = CallSite<Func<CallSite, object, bool, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "OtherBinDisposeCommentExist", typeof(DetailsController), infoArray3));
                }
                o__0.p__2.Target(o__0.p__2, base.ViewBag, false);
            }
            return base.View(lotViewByLotID);
        }

        [CompilerGenerated]
        private static class o__0
        {
            public static CallSite<Func<CallSite, object, string, object>> p__0;
            public static CallSite<Func<CallSite, object, bool, object>> p__1;
            public static CallSite<Func<CallSite, object, bool, object>> p__2;
        }
    }
}

