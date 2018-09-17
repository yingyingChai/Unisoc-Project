using KaYi.Database;
using KaYi.FileSystem.Model;
using KaYi.Utilities;
using System;
using System.Collections.Generic;
namespace KaYi.FileSystem.DAL
{
	public class FileGateway
	{
		private DALGateway<File> dbGateway = new DALGateway<File>();
		public FileGateway()
		{
			this.dbGateway.LoadSchema("FILES");
		}
		public void AddNew(File newFile)
		{
			this.dbGateway.AddNew(newFile);
		}
		public void DeleteByFileID(string FileID)
		{
			this.dbGateway.DeleteByFieldValue("FileID", FileID);
		}
		public void UpdateByPK(File objFile)
		{
			this.dbGateway.UpdateByFieldValue("FileID", objFile.FileID, objFile);
		}
		public bool ExistSameFileNameInFolder(string parentID, string filename, string excludeFileID)
		{
			Conditions conditions = new Conditions();
			conditions.ConditionExpressions.Add(new Condition("ParentID", Operator.EqualTo, parentID));
			conditions.ConditionExpressions.Add(new Condition("OriginalFileName", Operator.EqualTo, filename));
			conditions.Connector = Connector.AND;
			string value = string.Format(" FileID <> '{0}' ", excludeFileID);
			int result = 0;
			this.dbGateway.getRecords(0, 9999, conditions, null, StringHelper.BuildStringList(value), Connector.AND, out result);
			return result != 0;
		}
		private IList<File> InnerGetFilesByCatIds(string ownerID, string keyword, string parentID, string startTime, string endTime, IList<string> catIds, FileStates fileStates, FileTypes fileType, bool displayInNotificationsOnly, bool publicReviewOnly, bool publicDownloadOnly, bool previewAfterPermitOnly, bool downloadAfterPermitOnly, string orderBy, bool desc, int pageIndex, int pageSize, out int recordCount)
		{
			Conditions conditions = new Conditions();
			if (!StringHelper.isNullOrEmpty(ownerID))
			{
				conditions.ConditionExpressions.Add(new Condition("OwnerID", Operator.EqualTo, ownerID));
			}
			if (!StringHelper.isNullOrEmpty(parentID))
			{
				conditions.ConditionExpressions.Add(new Condition("ParentID", Operator.EqualTo, parentID));
			}
			if (fileType != FileTypes.NotSpecifiled)
			{
				conditions.ConditionExpressions.Add(new Condition("FileType", Operator.EqualTo, fileType));
			}
			if (fileStates != FileStates.NotSpecified)
			{
				conditions.ConditionExpressions.Add(new Condition("FileState", Operator.EqualTo, fileStates));
			}
			if (!StringHelper.isNullOrEmpty(startTime) && !StringHelper.isNullOrEmpty(endTime))
			{
				conditions.ConditionExpressions.Add(new Condition("CreateTime", Operator.Between, startTime, endTime));
			}
			if (displayInNotificationsOnly)
			{
				conditions.ConditionExpressions.Add(new Condition("DisplayInNotifications", Operator.EqualTo, true));
			}
			if (publicReviewOnly)
			{
				conditions.ConditionExpressions.Add(new Condition("PublicReviewOnly", Operator.EqualTo, true));
			}
			if (publicDownloadOnly)
			{
				conditions.ConditionExpressions.Add(new Condition("PublicDownloadOnly", Operator.EqualTo, true));
			}
			if (previewAfterPermitOnly)
			{
				conditions.ConditionExpressions.Add(new Condition("PreviewAfterPermit", Operator.EqualTo, true));
			}
			if (downloadAfterPermitOnly)
			{
				conditions.ConditionExpressions.Add(new Condition("DownloadAfterPermit", Operator.EqualTo, true));
			}
			string text = "";
			if (catIds != null && catIds.Count > 0)
			{
				foreach (string current in catIds)
				{
					text += string.Format(" CategoryQueryIds like '%|{0}|%' or", current);
				}
				if (text.EndsWith("or"))
				{
					text = text.Substring(0, text.Length - 2);
				}
				text = "(" + text + ")";
			}
			conditions.Connector = Connector.AND;
			string text2 = string.Empty;
			if (!StringHelper.isNullOrEmpty(keyword))
			{
				text2 = string.Format("(OriginalFileName like '%{0}%' or Title like '%{0}%' or Keyword like '%{0}%' or Memo like '%{0}%' or RelativeProjectNames like '%{0}%' or RelativeUserNames like '%{0}%')", keyword);
			}
			OrderExpression orderExpression = null;
			if (!StringHelper.isNullOrEmpty(orderBy))
			{
				orderExpression = new OrderExpression(orderBy, desc);
			}
			IList<string> list = new List<string>();
			if (!StringHelper.isNullOrEmpty(text2))
			{
				list.Add(text2);
			}
			if (!StringHelper.isNullOrEmpty(text))
			{
				list.Add(text);
			}
			return this.dbGateway.getRecords(pageIndex, pageSize, conditions, orderExpression, list, Connector.AND, out recordCount);
		}
		private IList<File> InnerGetFilesBy(string ownerID, string keyword, string parentID, string startTime, string endTime, FileStates fileStates, FileTypes fileType, bool displayInNotificationsOnly, bool publicReviewOnly, bool publicDownloadOnly, bool previewAfterPermitOnly, bool downloadAfterPermitOnly, string orderBy, bool desc, int pageIndex, int pageSize, out int recordCount)
		{
			Conditions conditions = new Conditions();
			if (!StringHelper.isNullOrEmpty(ownerID))
			{
				conditions.ConditionExpressions.Add(new Condition("OwnerID", Operator.EqualTo, ownerID));
			}
			if (!StringHelper.isNullOrEmpty(parentID))
			{
				conditions.ConditionExpressions.Add(new Condition("ParentID", Operator.EqualTo, parentID));
			}
			if (fileType != FileTypes.NotSpecifiled)
			{
				conditions.ConditionExpressions.Add(new Condition("FileType", Operator.EqualTo, fileType));
			}
			if (fileStates != FileStates.NotSpecified)
			{
				conditions.ConditionExpressions.Add(new Condition("FileState", Operator.EqualTo, fileStates));
			}
			if (!StringHelper.isNullOrEmpty(startTime) && !StringHelper.isNullOrEmpty(endTime))
			{
				conditions.ConditionExpressions.Add(new Condition("CreateTime", Operator.Between, startTime, endTime));
			}
			if (displayInNotificationsOnly)
			{
				conditions.ConditionExpressions.Add(new Condition("DisplayInNotifications", Operator.EqualTo, true));
			}
			if (publicReviewOnly)
			{
				conditions.ConditionExpressions.Add(new Condition("PublicReviewOnly", Operator.EqualTo, true));
			}
			if (publicDownloadOnly)
			{
				conditions.ConditionExpressions.Add(new Condition("PublicDownloadOnly", Operator.EqualTo, true));
			}
			if (previewAfterPermitOnly)
			{
				conditions.ConditionExpressions.Add(new Condition("PreviewAfterPermit", Operator.EqualTo, true));
			}
			if (downloadAfterPermitOnly)
			{
				conditions.ConditionExpressions.Add(new Condition("DownloadAfterPermit", Operator.EqualTo, true));
			}
			conditions.Connector = Connector.AND;
			string value = string.Empty;
			if (!StringHelper.isNullOrEmpty(keyword))
			{
				value = string.Format("(OriginalFileName like '%{0}%' or Title like '%{0}%' or Keyword like '%{0}%' or Memo like '%{0}%' or RelativeProjectNames like '%{0}%' or RelativeUserNames like '%{0}%')", keyword);
			}
			OrderExpression orderExpression = null;
			if (!StringHelper.isNullOrEmpty(orderBy))
			{
				orderExpression = new OrderExpression(orderBy, desc);
			}
			return this.dbGateway.getRecords(pageIndex, pageSize, conditions, orderExpression, StringHelper.BuildStringList(value), Connector.AND, out recordCount);
		}
		public IList<File> GetFilesBy(string ownerID, string keyword, string parentID, string startTime, string endTime, FileStates fileStates, FileTypes fileType, string orderBy, bool desc, int pageIndex, int pageSize, out int recordCount)
		{
			return this.InnerGetFilesBy(ownerID, keyword, parentID, startTime, endTime, fileStates, fileType, false, false, false, false, false, orderBy, desc, pageIndex, pageSize, out recordCount);
		}
		public IList<File> GetFilesByCatIds(string ownerID, string keyword, string parentID, string startTime, string endTime, IList<string> catIds, FileStates fileStates, FileTypes fileType, string orderBy, bool desc, int pageIndex, int pageSize, out int recordCount)
		{
			return this.InnerGetFilesByCatIds(ownerID, keyword, parentID, startTime, endTime, catIds, fileStates, fileType, false, false, false, false, false, orderBy, desc, pageIndex, pageSize, out recordCount);
		}
		public IList<File> GetHotFiles(int pageIndex, int pageSize, out int recordCount)
		{
			Conditions conditions = new Conditions();
			conditions.ConditionExpressions.Add(new Condition("ReadCount", Operator.GreaterThan, 0));
			OrderExpression orderExpression = new OrderExpression("ReadCount", true);
			return this.dbGateway.getRecords(pageIndex, pageSize, conditions, orderExpression, null, Connector.AND, out recordCount);
		}
		public IList<File> GetFilesBy(string ownerID, string keyword, string parentID, string startTime, string endTime, FileStates fileStates, FileTypes fileType, bool displayInNotificationsOnly, bool publicReviewOnly, bool publicDownloadOnly, bool previewAfterPermitOnly, bool downloadAfterPermitOnly, string orderBy, bool desc, int pageIndex, int pageSize, out int recordCount)
		{
			return this.InnerGetFilesBy(ownerID, keyword, parentID, startTime, endTime, fileStates, fileType, displayInNotificationsOnly, publicReviewOnly, publicDownloadOnly, previewAfterPermitOnly, downloadAfterPermitOnly, orderBy, desc, pageIndex, pageSize, out recordCount);
		}
		public IList<File> GetFilesByUnknown(string ownerID, string keyword, string parentID, string startTime, string endTime, FileStates fileStates, FileTypes fileType, bool displayInNotificationsOnly, bool publicReviewOnly, bool publicDownloadOnly, bool previewAfterPermitOnly, bool downloadAfterPermitOnly, string orderBy, bool desc, int pageIndex, int pageSize, out int recordCount)
		{
			return this.InnerGetFilesBy(ownerID, keyword, parentID, startTime, endTime, fileStates, fileType, displayInNotificationsOnly, publicReviewOnly, publicDownloadOnly, previewAfterPermitOnly, downloadAfterPermitOnly, orderBy, desc, pageIndex, pageSize, out recordCount);
		}
		public File GetFileByID(string FileID)
		{
			Conditions conditions = new Conditions();
			conditions.ConditionExpressions.Add(new Condition("FileID", Operator.EqualTo, FileID));
			return this.dbGateway.getRecord(conditions);
		}
		public File GetFileByQuickID(string quickID)
		{
			Conditions conditions = new Conditions();
			conditions.ConditionExpressions.Add(new Condition("QuickID", Operator.EqualTo, quickID));
			return this.dbGateway.getRecord(conditions);
		}
	}
}
