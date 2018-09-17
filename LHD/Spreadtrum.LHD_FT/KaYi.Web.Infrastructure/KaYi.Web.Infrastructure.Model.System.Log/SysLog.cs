using System;
namespace KaYi.Web.Infrastructure.Model.System.Log
{
	public class SysLog
	{
		private string absolutePath = string.Empty;
		private string _operateid = string.Empty;
		private string sessionID = string.Empty;
		private string accountID = string.Empty;
		private string clientID = string.Empty;
		private string _operator = string.Empty;
		private DateTime _operatetime;
		private string _ipadd = string.Empty;
		private string _operation = string.Empty;
		private string _memo = string.Empty;
		private string _parameters = string.Empty;
		private DateTime createTime;
		private DateTime updateTime;
		private string updateStamp = string.Empty;
		public string AbsolutePath
		{
			get
			{
				return this.absolutePath;
			}
			set
			{
				this.absolutePath = value;
			}
		}
		public string OperateID
		{
			get
			{
				return this._operateid;
			}
			set
			{
				this._operateid = value;
			}
		}
		public string SessionID
		{
			get
			{
				return this.sessionID;
			}
			set
			{
				this.sessionID = value;
			}
		}
		public string AccountID
		{
			get
			{
				return this.accountID;
			}
			set
			{
				this.accountID = value;
			}
		}
		public string ClientID
		{
			get
			{
				return this.clientID;
			}
			set
			{
				this.clientID = value;
			}
		}
		public string Operator
		{
			get
			{
				return this._operator;
			}
			set
			{
				this._operator = value;
			}
		}
		public DateTime OperateTime
		{
			get
			{
				return this._operatetime;
			}
			set
			{
				this._operatetime = value;
			}
		}
		public string IPAdd
		{
			get
			{
				return this._ipadd;
			}
			set
			{
				this._ipadd = value;
			}
		}
		public string Operation
		{
			get
			{
				return this._operation;
			}
			set
			{
				this._operation = value;
			}
		}
		public string Memo
		{
			get
			{
				return this._memo;
			}
			set
			{
				this._memo = value;
			}
		}
		public string Parameters
		{
			get
			{
				return this._parameters;
			}
			set
			{
				this._parameters = value;
			}
		}
		public DateTime CreateTime
		{
			get
			{
				return this.createTime;
			}
			set
			{
				this.createTime = value;
			}
		}
		public DateTime UpdateTime
		{
			get
			{
				return this.updateTime;
			}
			set
			{
				this.updateTime = value;
			}
		}
		public string UpdateStamp
		{
			get
			{
				return this.updateStamp;
			}
			set
			{
				this.updateStamp = value;
			}
		}
	}
}
