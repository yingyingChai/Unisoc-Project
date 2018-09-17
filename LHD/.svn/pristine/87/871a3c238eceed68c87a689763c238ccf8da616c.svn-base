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

    public class QueryController : BaseController
    {
        public ActionResult EQALots(int pageIndex = 0, int pageSize = 10)
        {
            return base.View("Index");
        }

        private static int GetRandomSeed()
        {
            byte[] data = new byte[4];
            new RNGCryptoServiceProvider().GetBytes(data);
            return BitConverter.ToInt32(data, 0);
        }

        public ActionResult Index()
        {
            return base.View();
        }

        public ActionResult LotDispose(int pageIndex = 0, int pageSize = 10)
        {
            return base.View("Index");
        }

        public ActionResult NewComment(int pageIndex = 0, int pageSize = 10)
        {
            return base.View("Index");
        }

        public ActionResult OtherBinDispose(int pageIndex = 0, int pageSize = 10)
        {
            return base.View("Index");
        }

        public ActionResult Search(int pageSize, int pageIndex = 0, string orderBy = "", bool desc = false)
        {
            //if (DateTime.Now <= Convert.ToDateTime("2017-12-31"))
            //{
            //    if (StringHelper.isNullOrEmpty(orderBy))
            //    {
            //        orderBy = "UpdateTime";
            //        desc = true;
            //    }
            //    int recordCount = 0;
            //    LotQuery queryFromRequest = QueryUtility.GetQueryFromRequest(base.Request, BaseController.CurrentUserInfo.UserID);
            //    if ((BaseController.CurrentUserInfo.Role == UserRoles.OSAT) || (BaseController.CurrentUserInfo.Role == UserRoles.OSATAdmin))
            //    {
            //        queryFromRequest.Osat = BaseController.CurrentUserInfo.BUName;
            //    }
            //    IList<LotView> list = LotService.GetLotViews(queryFromRequest, orderBy, desc, pageIndex, pageSize, out recordCount);
            //    string s = JsonConvert.SerializeObject(new { currentPage = pageIndex, totalPages = PagerUtility.GetPageCount(recordCount, pageSize), rows = list });
            //    base.Response.Write(s);
            //}
            //return null;
            if (StringHelper.isNullOrEmpty(orderBy))
            {
                orderBy = "UpdateTime";
                desc = true;
            }
            int recordCount = 0;
            LotQuery queryFromRequest = QueryUtility.GetQueryFromRequest(base.Request, BaseController.CurrentUserInfo.UserID);
            if (BaseController.CurrentUserInfo.Role == UserRoles.OSAT || BaseController.CurrentUserInfo.Role == UserRoles.OSATAdmin)
            {
                queryFromRequest.Osat = BaseController.CurrentUserInfo.BUName;
            }
            IList<LotView> lotViews = LotService.GetLotViews(queryFromRequest, orderBy, desc, pageIndex, pageSize, out recordCount);
            foreach (LotView lv in lotViews)
            {
                lv.HoldReason = lv.HoldReason.Replace("failed sublot:", "");
            }
            string s = JsonConvert.SerializeObject(new
            {
                currentPage = pageIndex,
                totalPages = PagerUtility.GetPageCount(recordCount, pageSize),
                rows = lotViews
            });
            base.Response.Write(s);
            return null;
        }

        public ActionResult SetAllReaded(string notificationType)
        {
            NotificationService.SetAllReaded(BaseController.CurrentUserInfo.UserID);
            BaseController.CurrentUserInfo.WaitForConfirm = 0;
            BaseController.CurrentUserInfo.WaitForDispose = 0;
            BaseController.CurrentUserInfo.WaitForOtherBinDispose = 0;
            BaseController.CurrentUserInfo.NewComments = 0;
            BaseController.CurrentUserInfo.EQALots = 0;
            base.Session.Contents["User"] = BaseController.CurrentUserInfo;
            return null;
        }

        public ActionResult WaitConfirm(int pageIndex = 0, int pageSize = 10)
        {
            return base.View("Index");
        }
    }
}

