namespace Spreadtrum.LHD.Mvc.Areas.Lots.Controllers
{
    using KaYi.Utilities;
using Microsoft.CSharp.RuntimeBinder;
using Spreadtrum.LHD.Business;
using Spreadtrum.LHD.Entity.Lots;
using Spreadtrum.LHD.Mvc.Areas.Shared;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Web.Mvc;

    public class SWBinController : BaseController
    {
        [HttpPost]
        public ActionResult getSWBin(string lotID, string code, string defect, string qty, string failRate, string isPassed, string limited, string orderBy, bool desc = false, int pageIndex = 0, int pageSize = 5)
        {
            int recordCount = 0;
            IList<SWBin> list = LotService.GetSWBinsBy(lotID, code, defect, qty, failRate, isPassed, limited, orderBy, desc, pageIndex, pageSize, out recordCount);
            var data = new {
                currentPage = pageIndex,
                totalPages = PagerUtility.GetPageCount(recordCount, pageSize),
                rows = list
            };
            return base.Json(data);
        }

        public ActionResult Index(string lotID)
        {
            if (o__0.p__0 == null)
            {
                CSharpArgumentInfo[] argumentInfo = new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null), CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null) };
                o__0.p__0 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "LotID", typeof(SWBinController), argumentInfo));
            }
            o__0.p__0.Target(o__0.p__0, base.ViewBag, lotID);
            return base.View();
        }

        [CompilerGenerated]
        private static class o__0
        {
            public static CallSite<Func<CallSite, object, string, object>> p__0;
        }
    }
}

