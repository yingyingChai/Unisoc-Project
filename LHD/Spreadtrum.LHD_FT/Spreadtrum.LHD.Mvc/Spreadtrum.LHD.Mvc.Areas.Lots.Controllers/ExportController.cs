namespace Spreadtrum.LHD.Mvc.Areas.Lots.Controllers
{
    using KaYi.Utilities;
    using KaYi.Web.Infrastructure.Model.System.HandlerResponses;
    using Spreadtrum.LHD.Business;
    using Spreadtrum.LHD.Entity.Lots;
    using Spreadtrum.LHD.Mvc.Areas.Shared;
    using Spreadtrum.LHD.Mvc.Export;
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using System.Web.Mvc;

    public class ExportController : BaseController
    {
        public ActionResult ExportLots(string orderBy, bool desc)
        {
            int recordCount = 0;
            string xlsName = string.Format("LOTS_{0}", DateTime.Now.ToString("yyyyMMdd_hhmmss"));
            if (StringHelper.isNullOrEmpty(orderBy))
            {
                orderBy = "CreateTime";
                desc = true;
            }
            IList<LotView> tList = LotService.GetLotViews(QueryUtility.GetQueryFromRequest(base.Request, BaseController.CurrentUserInfo.UserID), orderBy, desc, 0, 0xf423f, out recordCount);
            string nextUrl = Exporthelper.GetExport<LotView>("LOTS", tList, "/Export/Excels/Lots/", "/Content/Exports/ExportLots.xml", xlsName, 0, 0);
            ResponseTypes redirect = ResponseTypes.Redirect;
            TipTypes information = TipTypes.Information;
            base.Response.Write(new HandlerResponse("0", "导出Excel文件", redirect.ToString(), information.ToString(), nextUrl, "", "").GenerateJsonResponse());
            return null;
        }

        public ActionResult ExportSwbin(string lotID, string code, string defect, string qty, string failRate, string isPassed, string limited, string orderBy, bool desc = false)
        {
            int recordCount = 0;
            LotView lotViewByLotID = LotService.GetLotViewByLotID(lotID);
            if (StringHelper.isNullOrEmpty(orderBy))
            {
                orderBy = "Code";
            }
            desc = false;
            string xlsName = "LOT_" + lotViewByLotID.LotNO + "_SWBin";
            IList<SWBin> tList = LotService.GetSWBinsBy(lotID, code, defect, qty, failRate, isPassed, limited, orderBy, desc, 0, 0x1869f, out recordCount);
            string nextUrl = Exporthelper.GetExport<SWBin>("SW Bin", tList, "/Export/Excels/Swbins/", "/Content/Exports/ExportSwbins.xml", xlsName, 0, 0);
            ResponseTypes redirect = ResponseTypes.Redirect;
            TipTypes information = TipTypes.Information;
            base.Response.Write(new HandlerResponse("0", "导出Excel文件", redirect.ToString(), information.ToString(), nextUrl, "", "").GenerateJsonResponse());
            return null;
        }
    }
}

