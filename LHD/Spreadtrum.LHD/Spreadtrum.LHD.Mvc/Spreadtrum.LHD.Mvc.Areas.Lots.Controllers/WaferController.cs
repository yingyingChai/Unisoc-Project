using Spreadtrum.LHD.Mvc.Areas.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Spreadtrum.LHD.Entity.Lots;
using Spreadtrum.LHD.Business;
using KaYi.Utilities;
using Newtonsoft.Json;
using System.Configuration;
using System.Data;
using Spreadtrum.LHD.Mvc.Export;
using Spreadtrum.LHD.Entity.Users;
using KaYi.Web.Infrastructure.Model.System.HandlerResponses;
using System.IO;

namespace Spreadtrum.LHD.Mvc.Areas.Lots.Controllers
{
    public class WaferController : BaseController
    {

        private WaferService service = new WaferService();
        private Wafer_HistoryService historyService = new Wafer_HistoryService();
        private Wafer_sbinService sbinService = new Wafer_sbinService();
        public ActionResult List(string searchType = "",string transformID="")
        {
            ViewData["searchType"] = searchType;
            ViewData["transformID"] = transformID;
            VendorsService vs = new VendorsService();
            IList<Vendors> vendorList = vs.VendorList(VendorTypes.CP.ToString());
            return base.View(vendorList);
        }
        public ActionResult mList(string searchType = "", string transformID = "")
        {
            ViewData["searchType"] = searchType;
            ViewData["transformID"] = transformID;
            VendorsService vs = new VendorsService();
            IList<Vendors> vendorList = vs.VendorList(VendorTypes.CP.ToString());
            return base.View(vendorList);
        }
        public ActionResult Search(int pageSize, int pageIndex)
        {
            WaferQuery query = WaferQueryUtility.GetWaferQuery(base.Request);
            if (StringHelper.isNullOrEmpty(query.OrderBy))
            {
                query.OrderBy = "CreateDate";
                query.OrderDesc = true;
            }
            if (CurrentUserInfo.Role == UserRoles.Fab || CurrentUserInfo.Role == UserRoles.FabAdmin)
            {
                query.Osat = CurrentUserInfo.BUName;
                query.Status = (int)WaferStatus.WaitVendor;
            }
            int recordCount = 0;
            IList<Wafer> list = service.GetAllWaferBy(query, pageSize, pageIndex, out recordCount);
            if (list != null && list.Count > 0)
            {
                foreach (Wafer item in list)
                {
                    item.StatusText = WaferHelper.waferStatusDes(item.Status);
                    item.PEDisposeText = WaferHelper.WaferSelectionDes(item.PEDispose);
                    item.QADisposeText = WaferHelper.WaferSelectionDes(item.QADispose);
                    item.SPRDDecisionText = WaferHelper.WaferSelectionDes(item.SPRDDecision);
                    Boolean IsTriggered = false;
                    IList<Wafer_Sbin> ListSbin = sbinService.GetWaferSbin(item.TransformID, item.ID);
                    foreach (Wafer_Sbin sbin in ListSbin)
                    {
                        if (sbin.IsTriggered)
                        {
                            IsTriggered = true;
                        }
                    }
                    item.IsTriggered = IsTriggered;
                }
            }
            var json = JsonConvert.SerializeObject(new
            {
                currentPage = pageIndex,
                totalPages = PagerUtility.GetPageCount(recordCount, pageSize),
                rows = list,
            });
            base.Response.Write(json);
            return null;
        }
        public ActionResult ExportData()
        {
            WaferQuery query = WaferQueryUtility.GetWaferQuery(base.Request);
            int recordCount = 0;
            string xlsName = string.Format("Wafer_{0}", DateTime.Now.ToString("yyyyMMdd_hhmmss"));
            if (StringHelper.isNullOrEmpty(query.OrderBy))
            {
                query.OrderBy = "CreateDate";
                query.OrderDesc = true;
            }
            if (CurrentUserInfo.Role == UserRoles.Fab || CurrentUserInfo.Role == UserRoles.FabAdmin)
            {
                query.Osat = CurrentUserInfo.BUName;
                query.Status = (int)WaferStatus.WaitVendor;
            }
            IList<Wafer> list = service.GetAllWaferBy(query, 0xf423f, 0, out recordCount);
            if (list != null && list.Count > 0)
            {
                foreach (Wafer item in list)
                {
                    item.StatusText = WaferHelper.waferStatusDes(item.Status);
                    item.PEDisposeText = WaferHelper.WaferSelectionDes(item.PEDispose);
                    item.QADisposeText = WaferHelper.WaferSelectionDes(item.QADispose);
                    item.SPRDDecisionText = WaferHelper.WaferSelectionDes(item.SPRDDecision);
                }
            }
            string path = "/Export/Excels/Wafer/";
            string mp=Server.MapPath("~") + path;
            if (!Directory.Exists(mp))
            {
                Directory.CreateDirectory(mp);
            }
            string nextUrl = Exporthelper.GetExport<Wafer>("LOTS", list, "/Export/Excels/Wafer/", "/Content/Exports/ExportWafer.xml", xlsName, 0, 0);
            ResponseTypes redirect = ResponseTypes.Redirect;
            TipTypes information = TipTypes.Information;
            base.Response.Write(new HandlerResponse("0", "导出Excel文件", redirect.ToString(), information.ToString(), nextUrl, "", "").GenerateJsonResponse());
            return null;
        }
        public ActionResult PEQADispose()
        {

            int type = 0;
            var role = CurrentUserInfo.Role;
            if (role == UserRoles.PE || role == UserRoles.PEAdmin)
            {
                type = 1;
            }
            if (role == UserRoles.QA || role == UserRoles.QAAdmin)
            {
                type = 2;
            }
            if (role == UserRoles.Fab || role == UserRoles.FabAdmin)
            {
                type = 3;
            }
            int suc=service.UpdateWaferStatus(base.Request, type, CurrentUserInfo.FullName);
            var json = JsonConvert.SerializeObject(new
            {
                suc = suc,
            });
            base.Response.Write(json);
            return null;
        }
        public ActionResult History()
        {
            return base.View();
        }
        public ActionResult mHistory()
        {
            return base.View();
        }
        public ActionResult HistorySearch(int pageSize, int pageIndex)
        {
            HistoryQuery query = HistoryQueryUtility.GetHistoryQuery(base.Request);
            int recordCount = 0;
            IList<VwWafer_History> list = historyService.HistoryList(query,pageSize,pageIndex,out recordCount);
            if (list != null && list.Count > 0)
            {
                foreach (VwWafer_History history in list)
                {
                    if (history.IsVendor)
                    {
                        history.DisposeText = "Confirmed";
                    }
                    else
                    {
                        history.DisposeText = WaferHelper.WaferSelectionDes(history.Dispose);
                    }
                }
            }
            var json = JsonConvert.SerializeObject(new
            {
                currentPage = pageIndex,
                totalPages = PagerUtility.GetPageCount(recordCount, pageSize),
                rows = list,
            });
            base.Response.Write(json);
            return null;
        }

        public ActionResult ExportHistory()
        {
            string xlsName = string.Format("History_{0}", DateTime.Now.ToString("yyyyMMdd_hhmmss"));
            HistoryQuery query = HistoryQueryUtility.GetHistoryQuery(base.Request);
            int recordCount = 0;
            IList<VwWafer_History> list = historyService.HistoryList(query, 0xf423f, 0, out recordCount);
            if (list != null && list.Count > 0)
            {
                foreach (VwWafer_History history in list)
                {
                    if (history.IsVendor)
                    {
                        history.DisposeText = "Confirmed";
                    }
                    else
                    {
                        history.DisposeText = WaferHelper.WaferSelectionDes(history.Dispose);
                    }
                }
            }
            string nextUrl = Exporthelper.GetExport<VwWafer_History>("Historys", list, "/Export/Excels/History/", "/Content/Exports/ExportHistory.xml", xlsName, 0, 0);
            ResponseTypes redirect = ResponseTypes.Redirect;
            TipTypes information = TipTypes.Information;
            base.Response.Write(new HandlerResponse("0", "导出Excel文件", redirect.ToString(), information.ToString(), nextUrl, "", "").GenerateJsonResponse());
            return null;
        }
        public ActionResult WaferSbin(DetailWaferQuery query,int pageIndex,int pageSize)
        {
            int recordCount = 0;
            IList<Wafer> list = service.GetWaferByLotid(query, pageIndex, pageSize, out recordCount);
            if (list != null && list.Count > 0) {
                foreach (Wafer item in list) {
                    if (item.StartTime != null) {
                        if (item.StartTime.Year == 1900)
                        {
                            item.StrStartTime = "";
                        }
                        else {
                            item.StrStartTime = item.StartTime.ToString("yyyy/MM/dd");
                        }
                    }

                    item.ListSbin = sbinService.GetWaferSbin(query.TransformID, item.ID);
                }
            }
            var json = JsonConvert.SerializeObject(new
            {
                currentPage = pageIndex,
                totalPages = PagerUtility.GetPageCount(recordCount, pageSize),
                rows = list,
            });
            base.Response.Write(json);
            return null;
        }
        public ActionResult ExportWafer(DetailWaferQuery query)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Wafer_Code");
            dt.Columns.Add("Wafer_id");
            dt.Columns.Add("Program");
            dt.Columns.Add("Start Time");
            dt.Columns.Add("Total die count");
            dt.Columns.Add("Yield(%)");
            IList<Wafer_Sbin> list = sbinService.GetSbinTextByLotId(query.TransformID);
            if (list != null && list.Count > 0) {
                foreach (Wafer_Sbin item in list) {
                    dt.Columns.Add(item.SbinText);
                }
            }
            int recordCount = 0;
            IList<Wafer_Sbin> listValue = null;
            IList<Wafer> waferlist = service.GetWaferByLotid(query, 0, 0xf423f, out recordCount);
            if (waferlist != null && waferlist.Count > 0) {
                var index = 0;
                var columnindex = 0;
                foreach (Wafer item in waferlist) {
                    dt.Rows.Add(dt.NewRow());
                    dt.Rows[index][0] = item.WaferCode;
                    dt.Rows[index][1] = item.WaferID;
                    dt.Rows[index][2] = item.Program;
                    if (item.StartTime != null)
                    {
                        if (item.StartTime.Year == 1900)
                        {
                            dt.Rows[index][3] = "";
                        }
                        else
                        {
                            dt.Rows[index][3] = item.StartTime.ToString("yyyy/MM/dd");
                        }
                    }
                    // dt.Rows[index][3] = item.StartTime;
                    dt.Rows[index][4] = item.TotalDieCount;
                    dt.Rows[index][5] = item.Yield+"%";
                    listValue = sbinService.GetWaferSbin(query.TransformID, item.ID);
                    if (listValue != null && listValue.Count > 0) {
                        columnindex = 6;
                        foreach (Wafer_Sbin sbin in listValue) {
                            dt.Rows[index][columnindex] = sbin.SbinValue;
                            columnindex++;
                        }
                    }
                    index++;
                }
            }
            if (dt.Rows.Count > 0)
            {
                string xlsName = string.Format("Wafer_{0}", DateTime.Now.ToString("yyyyMMdd_hhmmss"));
                Exporthelper.ExportToExecel(System.Web.HttpContext.Current.Response, dt, xlsName+".xls");
            }
            return null;
        }
    }
}