using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Spreadtrum.LHD.Entity.Lots;
using KaYi.Utilities;


namespace Spreadtrum.LHD.Mvc.Areas.Lots.Controllers
{
    public class WaferQueryUtility
    {
        public static WaferQuery GetWaferQuery(HttpRequestBase req)
        {

            WaferQuery query = new WaferQuery {
                SearchType = req.QueryString["SearchType"],
                ProductName = req.QueryString["ProductName"],
                WaferCode = req.QueryString["WaferCode"],
                LotId = req.QueryString["LotId"],
                WaferID = req.QueryString["WaferID"],
                TransformID=req.QueryString["TransformID"],
                PEDispose = StringHelper.isNullOrEmpty(req.QueryString["PEDispose"]) ? -1 : Convert.ToInt32(req.QueryString["PEDispose"]),
                PEComment = req.QueryString["PEComment"],
                QADispose = StringHelper.isNullOrEmpty(req.QueryString["QADispose"]) ? -1 : Convert.ToInt32(req.QueryString["QADispose"]),
                QAComment=req.QueryString["QAComment"],
                SPRDDecision = StringHelper.isNullOrEmpty(req.QueryString["SPRDDecision"]) ? -1 : Convert.ToInt32(req.QueryString["SPRDDecision"]),
                VendorComment=req.QueryString["VendorComment"],
                Osat = req.QueryString["Osat"],
                Status = StringHelper.isNullOrEmpty(req.QueryString["Status"]) ? -1 : Convert.ToInt32(req.QueryString["Status"]),
                HoldReason = req.QueryString["HoldReason"],
                Yield = req.QueryString["Yield"],
                CompletionDate = req.QueryString["CompletionDate"],
                OrderBy = req.QueryString["OrderBy"],
                OrderDesc = StringHelper.isNullOrEmpty(req.QueryString["OrderDesc"]) ? false : Convert.ToBoolean(req.QueryString["OrderDesc"]),
                LastDays = StringHelper.isNullOrEmpty(req.QueryString["LastDays"]) ? 1 : Convert.ToInt32(req.QueryString["LastDays"]),

            };
            return query;
        }
    

    }
    public class HistoryQueryUtility
    {
        public static HistoryQuery GetHistoryQuery(HttpRequestBase req)
        {
            HistoryQuery query = new HistoryQuery {
                ProductName = req.QueryString["ProductName"],
                WaferCode = req.QueryString["WaferCode"],
                LotId = req.QueryString["LotId"],
                WaferID=req.QueryString["WaferID"],
                Dispose=StringHelper.isNullOrEmpty(req.QueryString["Dispose"])?0:int.Parse(req.QueryString["Dispose"]),
                UserID=req.QueryString["UserID"],
                Comment=req.QueryString["Comment"],
                CreateTime=req.QueryString["CreateTime"],
                OrderBy = req.QueryString["OrderBy"],
                OrderDesc = StringHelper.isNullOrEmpty(req.QueryString["OrderDesc"]) ? false : Convert.ToBoolean(req.QueryString["OrderDesc"]),
            };
            return query;
        }
    }
}