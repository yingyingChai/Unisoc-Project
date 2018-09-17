using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Spreadtrum.LHD.Entity.Lots;
using KaYi.Utilities;
namespace Spreadtrum.LHD.Mvc.Areas.Lots.Controllers
{
    public class TransformQueryUtility
    {
        public static Lot_TransformedQuery GetLotTransformQuery(HttpRequestBase request)
        {
            Lot_TransformedQuery lot = new Lot_TransformedQuery {
                SearchType = request.QueryString["SearchType"],
                ProductName = request.QueryString["ProductName"],
                WaferCode = request.QueryString["WaferCode"],
                Osat = request.QueryString["Osat"],
                LotId = request.QueryString["LotId"],
                Status = StringHelper.isNullOrEmpty(request.QueryString["Status"]) ? -1 : Convert.ToInt32(request.QueryString["Status"]),
                HoldReason = request.QueryString["HoldReason"],
                WfCount = StringHelper.isNullOrEmpty(request.QueryString["WfCount"]) ? -1 : Convert.ToInt32(request.QueryString["WfCount"]),
                Yield = request.QueryString["Yield"],
                CompletionDate = request.QueryString["CompletionDate"],
                OrderBy = request.QueryString["OrderBy"],
                OrderDesc = StringHelper.isNullOrEmpty(request.QueryString["OrderDesc"]) ? false : Convert.ToBoolean(request.QueryString["OrderDesc"]),
                LastDays = StringHelper.isNullOrEmpty(request.QueryString["LastDays"]) ? -1 : Convert.ToInt32(request.QueryString["LastDays"]),
                OutClose = StringHelper.isNullOrEmpty(request.QueryString["OutClose"]) ? 0 : Convert.ToInt32(request.QueryString["OutClose"]),
                OperatorStatus= StringHelper.isNullOrEmpty(request.QueryString["OperatorStatus"]) ? -1 : Convert.ToInt32(request.QueryString["OperatorStatus"]),
            };
            return lot;
        }
    }
}