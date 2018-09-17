using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spreadtrum.LHD.Entity.Lots;
using KaYi.Database;
using KaYi.Utilities;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Configuration;
using System.Collections;

namespace Spreadtrum.LHD.DAL.Lots
{
   public class WaferGateway
    {
        private DALGateway<Wafer> dbGateway = new DALGateway<Wafer>();
        private int rule =int.Parse(ConfigurationManager.AppSettings["WaferRule"].ToString().Trim());
        public WaferGateway()
        {
            this.dbGateway.LoadSchema("VW_WAFERS");
        }
        public IList<Wafer> GetAllWaferBy(WaferQuery query, int pageSize, int pageIndex,out int recoedCount)
        {
            Conditions conditions = GetCondition(query);
            OrderExpression orderExpression = null;
           // List<string> orderList = new List<string>();
            if (!StringHelper.isNullOrEmpty(query.OrderBy))
            {
                orderExpression = new OrderExpression(query.OrderBy, query.OrderDesc);
               // orderList.Add(query.OrderBy);
            }
            orderExpression = new OrderExpression("WaferID", false);
            // orderList.Add("WaferID");
            return this.dbGateway.getRecords(pageIndex, pageSize, conditions, orderExpression, null, Connector.AND, out recoedCount);
        }
        private Conditions GetCondition(WaferQuery query)
        {
            Conditions conditions = new Conditions();
            if (!StringHelper.isNullOrEmpty(query.SearchType))
            {
                conditions.ConditionExpressions.Add(new Condition("AutoJudeResult",Operator.EqualTo,query.SearchType));
            }
            if (!StringHelper.isNullOrEmpty(query.ProductName))
            {
                conditions.ConditionExpressions.Add(new Condition("ProductName", Operator.Like, query.ProductName));
            }
            if (!StringHelper.isNullOrEmpty(query.WaferCode))
            {
                conditions.ConditionExpressions.Add(new Condition("WaferCode", Operator.Like, query.WaferCode));
            }

            if (!StringHelper.isNullOrEmpty(query.Osat))
            {
                conditions.ConditionExpressions.Add(new Condition("Vendor", Operator.EqualTo, query.Osat));
            }
            if (!StringHelper.isNullOrEmpty(query.LotId))
            {
                conditions.ConditionExpressions.Add(new Condition("LotId", Operator.Like, query.LotId));
            }

            if (!StringHelper.isNullOrEmpty(query.WaferID))
            {
                conditions.ConditionExpressions.Add(new Condition("WaferID", Operator.EqualTo, query.WaferID));
            }
            if (!StringHelper.isNullOrEmpty(query.TransformID))
            {
                conditions.ConditionExpressions.Add(new Condition("TransformID", Operator.EqualTo, query.TransformID));
            }
            if (query.PEDispose > 0)
            {
                conditions.ConditionExpressions.Add(new Condition("PEDispose", Operator.EqualTo, query.PEDispose));
            }
            if (!StringHelper.isNullOrEmpty(query.PEComment))
            {
                conditions.ConditionExpressions.Add(new Condition("PEComment", Operator.Like, query.PEComment));
            }
            if (query.QADispose > 0)
            {
                conditions.ConditionExpressions.Add(new Condition("QADispose", Operator.EqualTo, query.QADispose));
            }
            if (!StringHelper.isNullOrEmpty(query.QAComment))
            {
                conditions.ConditionExpressions.Add(new Condition("QAComment", Operator.Like, query.QAComment));
            }
            if (query.SPRDDecision > 0)
            {
                conditions.ConditionExpressions.Add(new Condition("SPRDDecision", Operator.EqualTo, query.SPRDDecision));
            }
            if (!StringHelper.isNullOrEmpty(query.VendorComment))
            {
                conditions.ConditionExpressions.Add(new Condition("VendorComment", Operator.Like, query.VendorComment));
            }
            if(!StringHelper.isNullOrEmpty(query.Osat))
            {
                conditions.ConditionExpressions.Add(new Condition("Vendor", Operator.Like, query.Osat));
            }
            if (query.Status>0)
            {
                conditions.ConditionExpressions.Add(new Condition("Status", Operator.EqualTo, query.Status));
            }
            if (!StringHelper.isNullOrEmpty(query.HoldReason))
            {
                conditions.ConditionExpressions.Add(new Condition("HoldReason", Operator.Like, query.HoldReason));
            }
            if (!StringHelper.isNullOrEmpty(query.Yield))
            {
                conditions.ConditionExpressions.Add(new Condition("Yield", Operator.EqualTo, query.Yield));
            }
            if (!StringHelper.isNullOrEmpty(query.CompletionDate))
            {
                string[] dateArray = query.CompletionDate.Split('-');
                conditions.ConditionExpressions.Add(new Condition("CompletionDate", Operator.Between, dateArray[0], dateArray[1]+ " 23:59:59"));
            }
           
            if (query.LastDays>0)
            {
                var dt = DateTime.Now.ToLocalTime();
                if (query.LastDays == 1)
                {
                    conditions.ConditionExpressions.Add(new Condition("CompletionDate", Operator.Between, dt.AddHours(-24), dt));
                }
                else
                {
                    string str_startTime = dt.AddDays(-2).ToShortDateString();
                    conditions.ConditionExpressions.Add(new Condition("CompletionDate", Operator.Between, str_startTime, dt.ToShortDateString()));
                }
            }
           
            conditions.Connector = Connector.AND;
            return conditions;
        }
        public IList<Wafer> GetWaferByLotid(DetailWaferQuery query,int pageIndex,int pageSize, out int recoedCount)
        {
            Conditions conditions = new Conditions();
            conditions.ConditionExpressions.Add(new Condition("TransformID",Operator.EqualTo,query.TransformID));
            if (!StringHelper.isNullOrEmpty(query.WaferID)) {
                conditions.ConditionExpressions.Add(new Condition("WaferID", Operator.Like, query.WaferID));
            }
            if (!string.IsNullOrEmpty(query.Program))
            {
                conditions.ConditionExpressions.Add(new Condition("Program", Operator.Like, query.Program));
            }
            if (!string.IsNullOrEmpty(query.StartTime))
            {
                conditions.ConditionExpressions.Add(new Condition("StartTime", Operator.Between, query.StartTime+" 00:00:00", query.StartTime + " 23:59:59"));
            }
            if (query.TotalDieCount>0)
            {
                conditions.ConditionExpressions.Add(new Condition("TotalDieCount", Operator.Like, query.TotalDieCount));
            }
            if (!string.IsNullOrEmpty(query.Yield))
            {
                conditions.ConditionExpressions.Add(new Condition("Yield", Operator.Like, query.Yield));
            }
            OrderExpression exp = null;
            if (!string.IsNullOrEmpty(query.OrderBy)) {
                exp = new OrderExpression(query.OrderBy,query.OrderDesc);
            }
            conditions.Connector = Connector.AND;
            return this.dbGateway.getRecords(pageIndex, pageSize, conditions, exp, null, Connector.AND, out recoedCount);
        }
        public Dictionary<int, ArrayList> UpdateWaferStatus(HttpRequestBase req,int type,string userName)
        {
            string wids = req.QueryString["hidwaferids"];
            string lids= req.QueryString["hidlotids"];
            string[] waferids = wids.Split(',');
            string[] lotids = lids.Split(',');
            ArrayList arrayList = new ArrayList();
            int suc = 0;
            Dictionary<int, ArrayList> dic = new Dictionary<int, ArrayList>();
            if (waferids != null && waferids.Length > 0) {
                string sql = "";
               // bool feedBack = false;
                int status = 0;
                string lotid = "";
                int i = 0;
                string strcomment = "";

                #region pe/qa
                if (type == 1 || type == 2) { //PE QA
                    int dispose = StringHelper.isNullOrEmpty(req.QueryString["HidStatus"]) ? 0 : int.Parse(req.QueryString["HidStatus"]);
                    dic=WaferDispose(dispose, waferids, lotids, type, req, userName);
                    #region 原始
                    //#region 定义
                    //int dispose = StringHelper.isNullOrEmpty(req.QueryString["HidStatus"]) ? 0 : int.Parse(req.QueryString["HidStatus"]);
                    //DataTable dt;
                    //int sprddispose = 0;
                    //string updatecolumn = "PEDispose";
                    //string updatedescolumn = "PEDisposeText";
                    //string updatecomment = "PEComment";
                    //string selectcolunm = "QADispose";
                    //status = (int)WaferStatus.WaitQA;
                    //if (type == 2)//qa
                    //{
                    //    updatecolumn = "QADispose";
                    //    updatedescolumn = "QADisposeText";
                    //    updatecomment = "QAComment";
                    //    selectcolunm = "PEDispose";
                    //    status = (int)WaferStatus.WaitPE;
                    //}
                    //#endregion
                    //if (rule == 1) // PE 或QA dispose
                    //{
                    //    //wafer sprd==PE/QA dispose
                    //    sprddispose = dispose;
                    //    #region //根据sprd查询feedback
                    //    sql = "select top 1 FeedBack from WAFE_STATUS_LIST where SprdDecision=" + sprddispose;
                    //    dt = this.dbGateway.getDataTableBySqlStatement(sql);
                    //    if (dt != null && dt.Rows.Count > 0){
                    //        feedBack = bool.Parse(dt.Rows[0]["FeedBack"].ToString());
                    //        if (feedBack) status = (int)WaferStatus.WaitVendor;
                    //        else status = (int)WaferStatus.WaitPE;
                    //    }
                    //    #endregion
                    //    #region 数据处理
                    //    foreach (string s in waferids)
                    //    {
                    //        strcomment = req.QueryString["petxtcomment_"+s];// comment == null ? "" : comment[i];
                    //        if (type == 2) {
                    //            strcomment = req.QueryString["qatxtcomment_" + s];
                    //        }
                    //        lotid = lotids == null ? "" : lotids[i];
                    //        if (feedBack) {
                    //            if (!arrayList.Contains(lotid))
                    //            {
                    //                arrayList.Add(lotid);
                    //            }
                    //        }
                    //        suc +=UpdateWafer(updatecolumn, dispose, updatedescolumn, updatecomment, strcomment, status, sprddispose, s, lotid, userName, false);
                    //        i++;
                    //    }
                    //    #endregion
                    //}
                    //else
                    //{ //PE和QA dispose
                    //    int pedispose = 0;
                    //    int qadispose = 0;
                    //    bool isfeedback = false;
                    //    #region 数据处理
                    //    foreach (string s in waferids) {
                    //        strcomment = req.QueryString["petxtcomment_" + s];// comment == null ? "" : comment[i];
                    //        if (type == 2)
                    //        {
                    //            strcomment = req.QueryString["qatxtcomment_" + s];
                    //        }
                    //        lotid = lotids == null ? "" : lotids[i];
                    //        sql = "select " + selectcolunm + " from WAFER where ID='" + s+"'";
                    //        dt = this.dbGateway.getDataTableBySqlStatement(sql);
                    //        if (dt != null && dt.Rows.Count > 0)
                    //        {
                    //            if (type == 1)
                    //            {//pe
                    //                pedispose = dispose;
                    //                qadispose = 0;
                    //                if (dt.Rows[0]["QADispose"] != DBNull.Value) {
                    //                    qadispose = int.Parse(dt.Rows[0]["QADispose"].ToString());
                    //                }
                    //            }
                    //            else
                    //            {//qa
                    //                pedispose = 0;
                    //                if (dt.Rows[0]["PEDispose"] != DBNull.Value) {
                    //                    pedispose = int.Parse(dt.Rows[0]["PEDispose"].ToString());
                    //                }
                    //                qadispose = dispose;
                    //            }
                    //            sql = "select top 1 SprdDecision,FeedBack from WAFE_STATUS_LIST  where PeDispose=" + pedispose + " and QaDispose=" + qadispose;
                    //            dt = this.dbGateway.getDataTableBySqlStatement(sql);
                    //            if (dt != null && dt.Rows.Count > 0)
                    //            {
                    //                sprddispose = int.Parse(dt.Rows[0]["SprdDecision"].ToString());
                    //                isfeedback = bool.Parse(dt.Rows[0]["FeedBack"].ToString());
                    //            }
                    //            else {
                    //                isfeedback = false;
                    //                sprddispose = 0;
                    //            }
                    //            if (isfeedback) {
                    //                status = (int)WaferStatus.WaitVendor;
                    //                if (!arrayList.Contains(lotid))
                    //                {
                    //                    arrayList.Add(lotid);
                    //                }
                    //            }
                    //            suc+=UpdateWafer(updatecolumn, dispose, updatedescolumn, updatecomment, strcomment, status, sprddispose, s, lotid, userName, false);
                    //        }
                    //        i++;
                    //    }
                    //    #endregion
                    //}
                    #endregion
                }
                #endregion
                #region vendor
                if (type == 3) {//vendor
                    status = (int)WaferStatus.Close;
                    foreach (string s in waferids)
                    {
                        lotid = lotids == null ? "" : lotids[i];
                        strcomment = req.QueryString["vendortxtcomment_" + s];
                        try
                        {
                            sql = "update WAFER set Status=" + status + ","+
                            "VendorConfirm=1,VendorConfirmDate='" + DateTime.Now.ToLocalTime() +
                            "',VendorComment='" + strcomment + "' where ID='" + s + "'";
                            this.dbGateway.ExecuteSQLStatement(sql);
                            AddHistory(lotid, s, userName, strcomment, true,111);
                            suc++;
                        }
                        catch
                        {
                            suc += 0;
                            throw;
                        }
                    }
                    dic.Add(suc, null);
                }
                #endregion
                #region  更新lot表
                ArrayList array = new ArrayList();
                foreach (string s in lotids)
                {
                    if (!array.Contains(s))
                    {
                        array.Add(s);
                    }
                }
                foreach (string s in array)
                {
                    
                    UpdateLotStatus(s, type);
                }
                #endregion
            }
           
            return dic;
            //return suc;
        }

        private Dictionary<int, ArrayList> WaferDispose(int dispose, string[] waferids, string[] lotids, int type, HttpRequestBase req, string userName,string lotid="", bool iswafer = true)
        {
            string sql = "";
            bool feedBack = false;
            int status = 0;
           // string lotid = "";
            int i = 0;
            ArrayList arrayList = new ArrayList();
            int suc = 0;
            string strcomment = "";
            #region pe/qa
          
            #region 定义
            DataTable dt;
            int sprddispose = 0;
            string updatecolumn = "PEDispose";
            string updatedescolumn = "PEDisposeText";
            string updatecomment = "PEComment";
            string selectcolunm = "QADispose";
            status = (int)WaferStatus.WaitQA;
            if (type == 2)//qa
            {
                updatecolumn = "QADispose";
                updatedescolumn = "QADisposeText";
                updatecomment = "QAComment";
                selectcolunm = "PEDispose";
                status = (int)WaferStatus.WaitPE;
            }
            #endregion
            if (rule == 1) // PE 或QA dispose
            {
                //wafer sprd==PE/QA dispose
                sprddispose = dispose;
                #region //根据sprd查询feedback
                sql = "select top 1 FeedBack from WAFE_STATUS_LIST where SprdDecision=" + sprddispose;
                dt = this.dbGateway.getDataTableBySqlStatement(sql);
                if (dt != null && dt.Rows.Count > 0)
                {
                    feedBack = bool.Parse(dt.Rows[0]["FeedBack"].ToString());
                    if (feedBack) status = (int)WaferStatus.WaitVendor;
                    else status = (int)WaferStatus.WaitPE;
                }
                #endregion
                #region 数据处理
                foreach (string s in waferids)
                {
                    if (iswafer)
                    {
                        strcomment = req.QueryString["petxtcomment_" + s];// comment == null ? "" : comment[i];
                        if (type == 2)
                        {
                            strcomment = req.QueryString["qatxtcomment_" + s];
                        }
                        lotid = lotids == null ? "" : lotids[i];
                    }
                    
                    if (feedBack)
                    {
                        if (!arrayList.Contains(lotid))
                        {
                            arrayList.Add(lotid);
                        }
                    }
                    suc += UpdateWafer(updatecolumn, dispose, updatedescolumn, updatecomment, strcomment, status, sprddispose, s, lotid, userName, false);
                    i++;
                }
                #endregion
            }
            else
            { //PE和QA dispose
                int pedispose = 0;
                int qadispose = 0;
                bool isfeedback = false;
                #region 数据处理
                foreach (string s in waferids)
                {
                    if (iswafer)
                    {
                        strcomment = req.QueryString["petxtcomment_" + s];// comment == null ? "" : comment[i];
                        if (type == 2)
                        {
                            strcomment = req.QueryString["qatxtcomment_" + s];
                        }
                        lotid = lotids == null ? "" : lotids[i];
                    }
                    sql = "select " + selectcolunm + " from WAFER where ID='" + s + "'";
                    dt = this.dbGateway.getDataTableBySqlStatement(sql);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        if (type == 1)
                        {//pe
                            pedispose = dispose;
                            qadispose = 0;
                            if (dt.Rows[0]["QADispose"] != DBNull.Value)
                            {
                                qadispose = int.Parse(dt.Rows[0]["QADispose"].ToString());
                            }
                        }
                        else
                        {//qa
                            pedispose = 0;
                            if (dt.Rows[0]["PEDispose"] != DBNull.Value)
                            {
                                pedispose = int.Parse(dt.Rows[0]["PEDispose"].ToString());
                            }
                            qadispose = dispose;
                        }
                        sql = "select top 1 SprdDecision,FeedBack from WAFE_STATUS_LIST  where PeDispose=" + pedispose + " and QaDispose=" + qadispose;
                        dt = this.dbGateway.getDataTableBySqlStatement(sql);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            sprddispose = int.Parse(dt.Rows[0]["SprdDecision"].ToString());
                            isfeedback = bool.Parse(dt.Rows[0]["FeedBack"].ToString());
                        }
                        else
                        {
                            isfeedback = false;
                            sprddispose = 0;
                        }
                        if (isfeedback)
                        {
                            status = (int)WaferStatus.WaitVendor;
                            if (!arrayList.Contains(lotid))
                            {
                                arrayList.Add(lotid);
                            }
                        }
                        suc += UpdateWafer(updatecolumn, dispose, updatedescolumn, updatecomment, strcomment, status, sprddispose, s, lotid, userName, false);
                    }
                    i++;
                }
                #endregion
            }
            #endregion
            Dictionary<int, ArrayList> dic = new Dictionary<int, ArrayList>();
            dic.Add(suc, arrayList);
            return dic;
        }
        public Dictionary<int, ArrayList> DisposeByLot(string lotids,int type,int dispose, HttpRequestBase req, string userName)
        {
            string[] array = lotids.Split(',');
            WaferGeteway2 wafer = new WaferGeteway2();
            string[] waferids = null;
          
            int suc = 0;
            Dictionary<int, ArrayList> resultDic = null;
            ArrayList list = new ArrayList();
            foreach (string s in array)
            {
                waferids = wafer.LoadList(s);
                if (waferids != null && waferids.Length > 0) {
                    resultDic = WaferDispose(dispose, waferids, null, type, req, userName,s, false);
                    if (resultDic != null) {
                        foreach (KeyValuePair<int, ArrayList> kvp in resultDic) {
                            suc += kvp.Key;
                            if (kvp.Value != null) {
                                list.Add(kvp.Value[0]);
                            }
                        }
                    }
                }
                UpdateLotStatusByLot(s, dispose);
            }
            Dictionary<int, ArrayList> dic = new Dictionary<int, ArrayList>();
            dic.Add(suc,list);
            return dic;
        }
        private int UpdateWafer(string updatecolumn,int dispose,string updatedescolumn,string updatecomment,string strcomment,int status,int sprddispose,string s,string lotid,string userName,bool isvendor)
        {
            try
            {
                //更新wafer
                string sql = "update WAFER set " + updatecolumn + "=" + dispose + "," 
                             +updatecomment + "='" + strcomment + "',Status=" + status + 
                             ",SPRDDecision=" + sprddispose +
                             "  where ID='" + s + "'";
                this.dbGateway.ExecuteSQLStatement(sql);
                //添加 wafer operator history
                AddHistory(lotid, s, userName, strcomment, isvendor, dispose);
                return 1;
            }
            catch
            {
                return 0;
            }
        }
        private int AddHistory(string lotid,string s,string userName,string strcomment,bool isvendor,int dispose)
        {
            Wafer_History history = new Wafer_History();
            history.ID = Guid.NewGuid().ToString();
            history.TransformID = lotid;
            history.WaferID = s;
            history.Dispose = dispose;
            history.UserID = userName;
            history.Comment = strcomment;
            history.CreateTime = DateTime.Now.ToLocalTime();
            history.IsVendor = isvendor;
            Wafer_HistoryGateway dbhistory = new Wafer_HistoryGateway();
            return  dbhistory.AddHistory(history);
        }
        public void UpdateLotStatus(string lotid,int type)
        {
            Lot_TransformedGateway lotGateway = new Lot_TransformedGateway();
            Lot_Transformed lot = lotGateway.GetTransformById(lotid);
            if (lot != null) {
                int status = GetLotStatus(lotid); 
                lot.Status = status;
                if (type != 3)
                {
                    lot.OperatorStatus = GetLotOperatorStatus(lotid);
                    //string sql = "select ID from vw_Wafers where TransformID='" + lotid+ "' and SPRDDecision!="+(int)WaferSelection.Split;
                    //int count = this.dbGateway.GetRecordCountBySQLStatement(sql);
                    lot.WfCount = GetWaferCountNoSplitByLotId(lotid);
                }
                lotGateway.UpdateLot(lot);
            }
        }
        public void UpdateLotStatusByLot(string lotid,int dispose)
        {
            Lot_TransformedGateway lotGateway = new Lot_TransformedGateway();
            Lot_Transformed lot = lotGateway.GetTransformById(lotid);
            if (lot != null)
            {
                lot.Status = GetLotStatus(lotid);
                lot.OperatorStatus = GetLotOperatorStatus(lotid);
                if (dispose == (int)WaferSelection.Split)
                {
                    lot.WfCount = 0;
                }
                else {
                    //string sql = "select ID from vw_Wafers where TransformID='" + lotid + "' and SPRDDecision!=" + (int)WaferSelection.Split;
                    //int count = this.dbGateway.GetRecordCountBySQLStatement(sql);
                    lot.WfCount = GetWaferCountNoSplitByLotId(lotid);
                }
                lotGateway.UpdateLot(lot);
            }
        }
        private int GetWaferCountNoSplitByLotId(string lotid)
        {
            string sql = "select ID from vw_Wafers where TransformID='" + lotid + "' and SPRDDecision!=" + (int)WaferSelection.Split;
            int count = this.dbGateway.GetRecordCountBySQLStatement(sql);
            return count;
        }
        //lot status
        private int GetLotStatus(string lotid)
        {
            Conditions condtion = new Conditions();
            condtion.ConditionExpressions.Add(new Condition("TransformID", Operator.EqualTo, lotid));
            condtion.ConditionExpressions.Add(new Condition("Status", Operator.EqualTo, (int)WaferStatus.WaitQAPE));
            condtion.Connector = Connector.AND;
            int peqacount = this.dbGateway.GetRecordCountByConditions(condtion);
            condtion = new Conditions();
            condtion.ConditionExpressions.Add(new Condition("TransformID", Operator.EqualTo, lotid));
            condtion.ConditionExpressions.Add(new Condition("Status", Operator.EqualTo, (int)WaferStatus.WaitPE));
            condtion.Connector = Connector.AND;
            int pecount = this.dbGateway.GetRecordCountByConditions(condtion);
            condtion = new Conditions();
            condtion.ConditionExpressions.Add(new Condition("TransformID", Operator.EqualTo, lotid));
            condtion.ConditionExpressions.Add(new Condition("Status", Operator.EqualTo, (int)WaferStatus.WaitQA));
            condtion.Connector = Connector.AND;
            int qacount = this.dbGateway.GetRecordCountByConditions(condtion);
            condtion = new Conditions();
            condtion.ConditionExpressions.Add(new Condition("TransformID", Operator.EqualTo, lotid));
            condtion.ConditionExpressions.Add(new Condition("Status", Operator.EqualTo, (int)WaferStatus.WaitVendor));
            condtion.Connector = Connector.AND;
            int vendorcount = this.dbGateway.GetRecordCountByConditions(condtion);
            condtion = new Conditions();
            condtion.ConditionExpressions.Add(new Condition("TransformID", Operator.EqualTo, lotid));
            condtion.ConditionExpressions.Add(new Condition("Status", Operator.EqualTo, (int)WaferStatus.Close));
            condtion.Connector = Connector.AND;
            int closecount = this.dbGateway.GetRecordCountByConditions(condtion);

            //string sql = "select ID from vw_Wafers where TransformID='" + lotid+"'";
            //string peqasql = sql + " and Status=" + (int)WaferStatus.WaitQAPE;
            //int peqacount = this.dbGateway.GetRecordCountBySQLStatement(peqasql);
            //string pesql = sql + " and Status=" + (int)WaferStatus.WaitPE;
            //int pecount = this.dbGateway.GetRecordCountBySQLStatement(pesql);
            //string qasql = sql + " and Status=" + (int)WaferStatus.WaitQA;
            //int qacount = this.dbGateway.GetRecordCountBySQLStatement(qasql);
            //string vendorsql = sql + " and Status=" + (int)WaferStatus.WaitVendor;
            //int vendorcount = this.dbGateway.GetRecordCountBySQLStatement(vendorsql);
            //string closesql = sql + " and Status=" + (int)WaferStatus.Close;
            //int closecount = this.dbGateway.GetRecordCountBySQLStatement(closesql);
            if (peqacount > 0 || (pecount > 0 && qacount > 0) || (peqacount <= 0 && pecount <= 0 && qacount <= 0 && vendorcount <= 0 && closecount <= 0))
            {
                return (int)WaferStatus.WaitQAPE;
            }
            else if (pecount > 0 && qacount <= 0 && vendorcount<=0)
            {
                return (int)WaferStatus.WaitPE;
            }
            else if (pecount <= 0 && qacount > 0)
            {
                return (int)WaferStatus.WaitQA;
            }
            else if (pecount > 0 && vendorcount > 0) {
                return (int)WaferStatus.WaitPEVendor;
            }
            else if (peqacount <= 0 && pecount <= 0 && qacount <= 0 && vendorcount > 0)
            {
                return (int)WaferStatus.WaitVendor;
            }
            else
            {
                return (int)WaferStatus.Close;
            }
        }
        // lot operatorstatus
        private int GetLotOperatorStatus(string lotid)
        {
            Conditions condition = new Conditions();
            condition.ConditionExpressions.Add(new Condition("TransformID",Operator.EqualTo,lotid));
            condition.ConditionExpressions.Add(new Condition("PEDispose", Operator.EqualTo, (int)WaferSelection.Hold));
            condition.Connector = Connector.AND;
            int pecount = this.dbGateway.GetRecordCountByConditions(condition);
            //condition = new Conditions();
            //condition.ConditionExpressions.Add(new Condition("TransformID", Operator.EqualTo, lotid));
            //condition.ConditionExpressions.Add(new Condition("QADispose", Operator.EqualTo, (int)WaferSelection.Hold));
            //condition.Connector = Connector.AND;
            //int qacount = this.dbGateway.GetRecordCountByConditions(condition);
            if (pecount > 0 /*|| qacount > 0*/) return 1;
            else return 2;
           
        }

        public string LoadCountByDispose(string lotid,int status)
        {
            string str = "";
            Conditions conditions = null;
            if (status == (int)WaferStatus.Close)
            {
                str = "Release:";
                conditions = new Conditions();
                conditions.ConditionExpressions.Add(new Condition("TransformID", Operator.EqualTo, lotid));
                conditions.Connector = Connector.AND;
                int totalcount = this.dbGateway.GetRecordCountByConditions(conditions);
                str += totalcount;
            }
            else
            {
                int count = 0;
                conditions = new Conditions();
                conditions.ConditionExpressions.Add(new Condition("TransformID", Operator.EqualTo, lotid));
                conditions.ConditionExpressions.Add(new Condition("SPRDDecision", Operator.Between, 0, (int)WaferSelection.Hold));
                conditions.Connector = Connector.AND;
                int holdCount = this.dbGateway.GetRecordCountByConditions(conditions);
                conditions = new Conditions();
                conditions.ConditionExpressions.Add(new Condition("TransformID", Operator.EqualTo, lotid));
                conditions.ConditionExpressions.Add(new Condition("SPRDDecision", Operator.EqualTo, (int)WaferSelection.Release));
                conditions.Connector = Connector.AND;
                int releaseCount = this.dbGateway.GetRecordCountByConditions(conditions);
                conditions = new Conditions();
                conditions.ConditionExpressions.Add(new Condition("TransformID", Operator.EqualTo, lotid));
                conditions.ConditionExpressions.Add(new Condition("SPRDDecision", Operator.EqualTo, (int)WaferSelection.Ink));
                conditions.Connector = Connector.AND;
                int inkCount = this.dbGateway.GetRecordCountByConditions(conditions);
                conditions = new Conditions();
                conditions.ConditionExpressions.Add(new Condition("TransformID", Operator.EqualTo, lotid));
                conditions.ConditionExpressions.Add(new Condition("SPRDDecision", Operator.EqualTo, (int)WaferSelection.Split));
                conditions.Connector = Connector.AND;
                int splitCount = this.dbGateway.GetRecordCountByConditions(conditions);
                conditions = new Conditions();
                conditions.ConditionExpressions.Add(new Condition("TransformID", Operator.EqualTo, lotid));
                conditions.ConditionExpressions.Add(new Condition("SPRDDecision", Operator.EqualTo, (int)WaferSelection.RMA));
                conditions.Connector = Connector.AND;
                int rmaCount = this.dbGateway.GetRecordCountByConditions(conditions);
                conditions = new Conditions();
                conditions.ConditionExpressions.Add(new Condition("TransformID", Operator.EqualTo, lotid));
                conditions.ConditionExpressions.Add(new Condition("SPRDDecision", Operator.EqualTo, (int)WaferSelection.Scrap));
                conditions.Connector = Connector.AND;
                int scrpCount = this.dbGateway.GetRecordCountByConditions(conditions);
                conditions = new Conditions();
                conditions.ConditionExpressions.Add(new Condition("TransformID", Operator.EqualTo, lotid));
                conditions.ConditionExpressions.Add(new Condition("SPRDDecision", Operator.EqualTo, (int)WaferSelection.Others));
                conditions.Connector = Connector.AND;
                int othersCount = this.dbGateway.GetRecordCountByConditions(conditions);
                if (holdCount > 0)
                {
                    str += "Hold:" + holdCount;
                    count++;
                }
                if (releaseCount > 0)
                {
                    if (!string.IsNullOrEmpty(str))
                    {
                        str += ",";
                    }
                    str += "Release:" + releaseCount;
                    count++;
                }
                if (inkCount > 0)
                {
                    if (!string.IsNullOrEmpty(str))
                    {
                        str += ",";
                    }
                    str += "Ink:" + inkCount;
                    count++;
                }
                if (splitCount > 0)
                {
                    if (!string.IsNullOrEmpty(str))
                    {
                        str += ",";
                    }
                    str += "Split:" + splitCount;
                    count++;
                }
                if (rmaCount > 0)
                {
                    if (!string.IsNullOrEmpty(str))
                    {
                        str += ",";
                    }
                    str += "RMA:" + rmaCount;
                    count++;
                }
                if (scrpCount > 0)
                {
                    if (!string.IsNullOrEmpty(str))
                    {
                        str += ",";
                    }
                    str += "Scrap:" + scrpCount;
                    count++;
                }
                if (othersCount > 0)
                {
                    if (!string.IsNullOrEmpty(str))
                    {
                        str += ",";
                    }
                    str += "Others:" + othersCount;
                    count++;
                }
                if (count <= 0)
                {
                    str = "Hold:";
                    conditions = new Conditions();
                    conditions.ConditionExpressions.Add(new Condition("TransformID", Operator.EqualTo, lotid));
                    conditions.Connector = Connector.AND;
                    int totalcount = this.dbGateway.GetRecordCountByConditions(conditions);
                    str += totalcount;
                }
            }
            return str;
        }
    }
    public class WaferGeteway2
    {
        private DALGateway<SqlWafer> dbGateway = new DALGateway<SqlWafer>();
        public WaferGeteway2()
        {
            this.dbGateway.LoadSchema("WAFER");
        }
        public int SaveWafer(SqlWafer wafer)
        {
            return this.dbGateway.AddNew(wafer);
        }
        public string[] LoadList(string lotid)
        {
            Conditions conditions = new Conditions();
            conditions.ConditionExpressions.Add(new Condition("TransformID", Operator.EqualTo, lotid));
            IList< SqlWafer> list=this.dbGateway.getRecords(0, 0xf423f,conditions,null);
            ArrayList waferArray = new ArrayList();
            if (list != null && list.Count > 0) {
                int i = 0;
                foreach (SqlWafer item in list)
                {
                    waferArray.Add(item.ID);
                    i++;
                }
            }
            return (string[])waferArray.ToArray(typeof(string));
        }

       
        
    }
}
