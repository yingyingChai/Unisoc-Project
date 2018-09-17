using System;
using System.Data;
using Quartz;
using XJP.BPM.AMSService.WebService;
using XJP.BPM.Platform.DataAccess.DAO;
using XJP.BPM.Platform.Utility.Enums;
using XJP.BPM.Platform.XJPException;
using System.Configuration;

namespace XJP.BPM.AMSService
{
    public class K2ExecuteAMS : IStatefulJob
    {
        #region Define Object
        public static string EVENT_SOURCE = "K2ExecuteService_AMS";
        private DataBase dbBPM = DataBaseFactory.Create("BPM");
        #endregion

        public void Execute(JobExecutionContext context)
        {
            string requestId = string.Empty;
            string activityId = string.Empty;
            string filesQuery = string.Empty;
            AMSEntity entity = new AMSEntity();
            DataTable dtRequest = null;
            DataTable dtDetail = null;
            DataTable dtFFS = null;
            //同步所有的信息
            try
            {
                /* 表中3种状态:
                 * Error:已完成单子即将处理(Pending)
                 * Pending:处理过程中发生错误单子(Error)
                 * Executing:已完成单子标识处理中(Executing)
                  */
                DataSet ds = dbBPM.ExecuteQuery(string.Format("SELECT ID,RequestID " +
                    "FROM BPM.dbo.T_WorkQueueAMS WHERE [Status]='Pending'"));
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        requestId = dr["RequestID"].ToString();
                        //即将处理该单子,修改状态Executing
                        dbBPM.ExecuteNonQuery(string.Format("UPDATE BPM.dbo.T_WorkQueueAMS SET [Status]='Executing' WHERE RequestID='{0}'", requestId));

                        entity.AMSSobjRequestBase = FormatAMSSobjRequestBase(requestId);
                        filesQuery = "SELECT * FROM SmartBox.dbo.T_AMS_Request WITH(NOLOCK) WHERE RequestID='{0}'";
                        dtRequest = dbBPM.ExecuteQuery(string.Format(filesQuery, requestId)).Tables[0];
                        entity.AMSRequest = FormatAMSRequest(dtRequest);
                        filesQuery = "SELECT * FROM SmartBox.dbo.T_AMS_Request_Activity_Detail WITH(NOLOCK) WHERE RequestID='{0}'";
                        dtDetail = dbBPM.ExecuteQuery(string.Format(filesQuery, requestId)).Tables[0];
                        entity.AMSRequestActivityDetail = FormatActivity(dtDetail);
                        filesQuery = "SELECT * FROM SmartBox.dbo.T_AMS_Request_Activity_FFS WITH(NOLOCK) WHERE RequestID='{0}'";
                        dtFFS = dbBPM.ExecuteQuery(string.Format(filesQuery, requestId)).Tables[0];
                        entity.AMSRequestActivityFFS = FormatFFS(dtFFS);

                        new MeetingAMS().AMSInfomation(entity); //调用

                        //记录日志并删除
                        dbBPM.ExecuteNonQuery(string.Format("INSERT INTO BPM.dbo.T_WorkQueueAMS_Log (RequestID,ProcInstID,ProcessName,ActionName,Result,OccuredDate,[Status])" +
    "SELECT RequestID,ProcInstID,ProcessName,ActionName,Result,OccuredDate,'Pending' FROM BPM.dbo.T_WorkQueueAMS WHERE ID={0};DELETE BPM.dbo.T_WorkQueueAMS WHERE ID={0}", dr["ID"].ToString()));
                    }
                }
            }
            catch (Exception ex)
            {
                if (requestId.Length > 0)
                {
                    //记录错误信息
                    dbBPM.ExecuteNonQuery(string.Format("UPDATE BPM.dbo.T_WorkQueueAMS SET [Status]='Error',Exception='{1}' WHERE RequestID='{0}'", requestId,
                        string.Format("StackTrace:{0},Message:{1}", ex.StackTrace.ToString(), ex.Message.ToString())));
                }
                ExceptionManagement.Instance.LogException(EVENT_SOURCE, requestId, LogLevel.High.ToString(), ex);
            }
        }

        #region [ AMSRequest ]
        public AMSRequest[] FormatAMSRequest(DataTable dt)
        {
            AMSRequest[] _list = new AMSRequest[dt.Rows.Count];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                _list[i] = FormatAMSRequest(dt.Rows[i]);
            }
            return _list;
        }
        AMSRequest FormatAMSRequest(DataRow row)
        {
            AMSRequest entry = new AMSRequest();
            entry.RequestID = row["RequestID"].ToString();
            entry.Category = row["Category"].ToString();
            entry.IONumber = row["IONumber"].ToString();
            entry.IOSubject = row["IOSubject"].ToString();
            entry.IODes = row["IODes"].ToString();
            entry.AMSNumber = row["AMSNumber"].ToString();
            entry.TA = row["TA"].ToString();
            entry.InitiatedBy = row["InitiatedBy"].ToString();
            entry.InsititutionType = row["InsititutionType"].ToString();
            entry.InsititutionName = row["InsititutionName"].ToString();
            entry.JustifiedBusinessSelectionCriteria = row["JustifiedBusinessSelectionCriteria"].ToString();
            entry.Status = row["Status"].ToString();
            entry.CreateTime = string.IsNullOrEmpty(row["CreateTime"].ToString()) ? DateTime.MinValue : DateTime.Parse(row["CreateTime"].ToString());
            entry.CreatedBy = row["CreatedBy"].ToString();
            entry.UpdateTime = string.IsNullOrEmpty(row["UpdateTime"].ToString()) ? DateTime.MinValue : DateTime.Parse(row["UpdateTime"].ToString());
            entry.UpdatedBy = row["UpdatedBy"].ToString();
            return entry;
        }
        #endregion

        #region [ Activity ]
        public AMSRequestActivityDetail[] FormatActivity(DataTable dt)
        {
            AMSRequestActivityDetail[] _list = new AMSRequestActivityDetail[dt.Rows.Count];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                _list[i] = FormatActivity(dt.Rows[i]);
            }
            return _list;
        }
        AMSRequestActivityDetail FormatActivity(DataRow row)
        {
            AMSRequestActivityDetail entry = new AMSRequestActivityDetail();
            entry.ID = row["ID"].ToString();
            entry.RequestID = row["RequestID"].ToString();
            if (!string.IsNullOrEmpty(row["HasServiceProvider"].ToString()))
            {
                entry.HasServiceProvider = bool.Parse(row["HasServiceProvider"].ToString());
            }
            if (!string.IsNullOrEmpty(row["ProductRelated"].ToString()))
            {
                entry.ProductRelated = bool.Parse(row["ProductRelated"].ToString());
            }
            entry.DepartmentAndProduct = row["DepartmentAndProduct"].ToString();
            entry.Category = row["Category"].ToString();
            entry.CostCenter = row["CostCenter"].ToString();
            entry.GLAccount = row["GLAccount"].ToString();
            entry.Sub_AMSNumber = row["Sub_AMSNumber"].ToString();
            entry.BeginDate = string.IsNullOrEmpty(row["BeginDate"].ToString()) ? DateTime.MinValue : DateTime.Parse(row["BeginDate"].ToString());
            entry.EndDate = string.IsNullOrEmpty(row["EndDate"].ToString()) ? DateTime.MinValue : DateTime.Parse(row["EndDate"].ToString());
            entry.Planning_Participant_InternalQuantity = string.IsNullOrEmpty(row["Planning_Participant_InternalQuantity"].ToString()) ? 0 : int.Parse(row["Planning_Participant_InternalQuantity"].ToString());
            entry.Planning_Participant_ExternalQuantity = string.IsNullOrEmpty(row["Planning_Participant_ExternalQuantity"].ToString()) ? 0 : int.Parse(row["Planning_Participant_ExternalQuantity"].ToString());
            entry.Planning_Country = row["Planning_Country"].ToString();
            entry.Planning_City = row["Planning_City"].ToString();
            entry.Planning_Location = row["Planning_Location"].ToString();
            entry.Planning_Description = row["Planning_Description"].ToString();
            entry.Actual_BeginDate = string.IsNullOrEmpty(row["Actual_BeginDate"].ToString()) ? DateTime.MinValue : DateTime.Parse(row["Actual_BeginDate"].ToString());
            entry.Actual_EndDate = string.IsNullOrEmpty(row["Actual_EndDate"].ToString()) ? DateTime.MinValue : DateTime.Parse(row["Actual_EndDate"].ToString());
            entry.Actual_Participant_InternalQuantity = string.IsNullOrEmpty(row["Actual_Participant_InternalQuantity"].ToString()) ? 0 : int.Parse(row["Actual_Participant_InternalQuantity"].ToString());
            entry.Actual_Participant_ExternalQuantity = string.IsNullOrEmpty(row["Actual_Participant_ExternalQuantity"].ToString()) ? 0 : int.Parse(row["Actual_Participant_ExternalQuantity"].ToString());
            entry.Actual_City = row["Actual_City"].ToString();
            entry.Actual_Location = row["Actual_Location"].ToString();
            entry.Category = row["Category"].ToString();
            entry.ExpenseSubTotal = string.IsNullOrEmpty(row["ExpenseSubTotal"].ToString()) ? 0 : float.Parse(row["ExpenseSubTotal"].ToString());
            entry.FFSSubTotal = string.IsNullOrEmpty(row["FFSSubTotal"].ToString()) ? 0 : float.Parse(row["FFSSubTotal"].ToString());
            entry.Total = string.IsNullOrEmpty(row["Total"].ToString()) ? 0 : float.Parse(row["Total"].ToString());
            if (!string.IsNullOrEmpty(row["Default"].ToString()))
            {
                entry.Default = bool.Parse(row["Default"].ToString());
            }
            entry.Status = row["Status"].ToString();
            entry.CreateTime = string.IsNullOrEmpty(row["CreateTime"].ToString()) ? DateTime.MinValue : DateTime.Parse(row["CreateTime"].ToString());
            entry.CreatedBy = row["CreatedBy"].ToString();
            entry.UpdateTime = string.IsNullOrEmpty(row["UpdateTime"].ToString()) ? DateTime.MinValue : DateTime.Parse(row["UpdateTime"].ToString());
            entry.UpdatedBy = row["UpdatedBy"].ToString();
            entry.CostCenter = row["CostCenter"].ToString();
            entry.GLAccount = row["GLAccount"].ToString();
            entry.FreezeStatus = row["FreezeStatus"].ToString();
            entry.MonitorStatus = row["MonitorStatus"].ToString();
            //entry.CRCNO = row["CRCNO"].ToString();//add by hdwang 20140416
            return entry;
        }
        #endregion

        #region [ FFS ]
        public AMSRequestActivityFFS[] FormatFFS(DataTable dt)
        {
            AMSRequestActivityFFS[] _list = new AMSRequestActivityFFS[dt.Rows.Count];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                _list[i] = FormatFFS(dt.Rows[i]);
            }
            return _list;
        }
        public AMSRequestActivityFFS FormatFFS(DataRow row)
        {
            AMSRequestActivityFFS entry = new AMSRequestActivityFFS();
            entry.ID = row["ID"].ToString();
            entry.RequestID = row["RequestID"].ToString();
            entry.Activity_ID = row["Activity_ID"].ToString();
            entry.Category = row["Category"].ToString();
            entry.CVPoolID = row["CVPoolID"].ToString();
            entry.ProviderName = row["ProviderName"].ToString();
            entry.ProviderBelongs = row["ProviderBelongs"].ToString();
            entry.ProviderLevel = row["ProviderLevel"].ToString();
            entry.SelectionCriteria = row["SelectionCriteria"].ToString();
            entry.Planed_ProvidingTime = string.IsNullOrEmpty(row["Planed_ProvidingTime"].ToString()) ? 0 : float.Parse(row["Planed_ProvidingTime"].ToString());
            entry.Planed_FeePerTime = string.IsNullOrEmpty(row["Planed_FeePerTime"].ToString()) ? 0 : float.Parse(row["Planed_FeePerTime"].ToString());
            if (!string.IsNullOrEmpty(row["IsBackUp"].ToString()))
            {
                entry.IsBackUp = bool.Parse(row["IsBackUp"].ToString());
            }
            entry.Planed_Total = string.IsNullOrEmpty(row["Planed_Total"].ToString()) ? 0 : float.Parse(row["Planed_Total"].ToString());
            entry.Actual_ProvidingTime = string.IsNullOrEmpty(row["Actual_ProvidingTime"].ToString()) ? 0 : float.Parse(row["Actual_ProvidingTime"].ToString());
            entry.Actual_FeePerTime = string.IsNullOrEmpty(row["Actual_FeePerTime"].ToString()) ? 0 : float.Parse(row["Actual_FeePerTime"].ToString());
            if (!string.IsNullOrEmpty(row["Attended"].ToString()))
            {
                entry.Attended = bool.Parse(row["Attended"].ToString());
            }
            entry.Actual_Total = string.IsNullOrEmpty(row["Actual_Total"].ToString()) ? 0 : float.Parse(row["Actual_Total"].ToString());
            entry.ProvidingUnit = row["ProvidingUnit"].ToString();
            entry.SAPCode = row["SAPCode"].ToString();
            entry.Currency = row["Currency"].ToString();
            entry.Exchange = string.IsNullOrEmpty(row["Exchange"].ToString()) ? 0 : float.Parse(row["Exchange"].ToString());
            entry.Tax = row["Tax"].ToString();
            entry.ActualPayment = string.IsNullOrEmpty(row["CreateTime"].ToString()) ? 0 : float.Parse(row["ActualPayment"].ToString());
            entry.CreateTime = string.IsNullOrEmpty(row["CreateTime"].ToString()) ? DateTime.MinValue : DateTime.Parse(row["CreateTime"].ToString());
            entry.CreatedBy = row["CreatedBy"].ToString();
            entry.UpdateTime = string.IsNullOrEmpty(row["UpdateTime"].ToString()) ? DateTime.MinValue : DateTime.Parse(row["UpdateTime"].ToString());
            entry.UpdatedBy = row["UpdatedBy"].ToString();
            entry.IDCard = row["IDCard"].ToString();
            //entry.ParticipateCount = string.IsNullOrEmpty(row["ParticipatCount"].ToString()) ? 0 : int.Parse(row["ParticipatCount"].ToString());
            //entry.ParticipateAmountSUM = string.IsNullOrEmpty(row["ParticipatAmountSUM"].ToString()) ? 0 : decimal.Parse(row["ParticipatAmountSUM"].ToString());
            entry.ParticipateAmountLimit = string.IsNullOrEmpty(row["ParticipateAmountLimit"].ToString()) ? 1000000 : float.Parse(row["ParticipateAmountLimit"].ToString());
            entry.AmountPercent = string.IsNullOrEmpty(row["AmountPercent"].ToString()) ? 0 : float.Parse(row["AmountPercent"].ToString());
            return entry;
        }
        #endregion

        #region [ AMSSobjRequestBase ]
        public AMSSobjRequestBase[] FormatAMSSobjRequestBase(string RequestID)
        {
            AMSSobjRequestBase entry = new AMSSobjRequestBase();
            string SQL = @"SELECT 
    R.ProcessType,
    R.RequestStatus,
    R.RequestSubject,
    R.Description,
    R.Department,
    R.CompanyCode,
    R.ApplicantWWID,
    ISNULL(U.CN_Name+' / ','')+U.EN_Name AS ApplicantName,
    R.ApplicantAdAccount,
    R.ApplicantPhone,
    R.ApplicantEmail,
    R.SMSNumber AS ApplicantSMSNumber,
    R.ApplicantTitle,
    R.CreatorWWID,
    R.CreatorName,
    R.CreatorAdAccount,
    R.SupervisorWWID,
    R.SupervisorName,
    R.SupervisorAdAccount,
    R.SMSNotification,
    R.Extended,
    R.CreatedTime,
    R.DueDate,
    R.LatestUpdateTime,
    R.ApprovedDate,
    R.ProcessName
FROM SmartBox.dbo.sobjRequestBase R WITH(NOLOCK)
INNER JOIN BPM.dbo.T_User U WITH(NOLOCK)
ON R.ApplicantWWID=U.WWID
WHERE R.ObjectID=" + RequestID.ToString();

            DataSet ds = dbBPM.ExecuteQuery(SQL);

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataRow row = ds.Tables[0].Rows[0];
                entry.ProcessType = row["ProcessType"].ToString();
                entry.RequestStatus = row["RequestStatus"].ToString();
                entry.RequestSubject = row["RequestSubject"].ToString();
                entry.Description = row["Description"].ToString();
                entry.Department = row["Department"].ToString();
                entry.CompanyCode = row["CompanyCode"].ToString();
                entry.ApplicantWWID = row["ApplicantWWID"].ToString();
                //entry.ValidateApplicantWWID = ApplicantWWID;
                entry.ApplicantName = row["ApplicantName"].ToString();
                entry.ApplicantAdAccount = row["ApplicantAdAccount"].ToString();
                entry.ApplicantPhone = row["ApplicantPhone"].ToString();
                entry.ApplicantEmail = row["ApplicantEmail"].ToString();
                //entry.ApplicantSMSNumber = row["ApplicantSMSNumber"].ToString();
                entry.ApplicantTitle = row["ApplicantTitle"].ToString();
                entry.SupervisorWWID = row["SupervisorWWID"].ToString();
                entry.SupervisorName = row["SupervisorName"].ToString();
                entry.SupervisorAdAccount = row["SupervisorAdAccount"].ToString();
                entry.CreatorWWID = row["CreatorWWID"].ToString();
                entry.CreatorName = row["CreatorName"].ToString();
                entry.CreatorAdAccount = row["CreatorAdAccount"].ToString();
                entry.ProcessName = row["ProcessName"].ToString();
                entry.SMSNotification = bool.Parse(row["SMSNotification"].ToString());
                //entry.ExtendedValue = row["Extended"].ToString();
                DateTime t = DateTime.MinValue;
                DateTime.TryParse(row["CreatedTime"].ToString(), out t);
                if (t != DateTime.MinValue) { entry.CreatedTime = t; t = DateTime.MinValue; };
                DateTime.TryParse(row["DueDate"].ToString(), out t);
                if (t != DateTime.MinValue) { entry.DueDate = t; t = DateTime.MinValue; };
                DateTime.TryParse(row["LatestUpdateTime"].ToString(), out t);
                if (t != DateTime.MinValue) { entry.LatestUpdateTime = t; t = DateTime.MinValue; };
                //DateTime.TryParse(row["ApprovedDate"].ToString(), out t);
                //if (t != DateTime.MinValue) { entry.ApprovedDate = t; t = DateTime.MinValue; };
            }
            return new AMSSobjRequestBase[] { entry };
        }
        #endregion

    }
}
