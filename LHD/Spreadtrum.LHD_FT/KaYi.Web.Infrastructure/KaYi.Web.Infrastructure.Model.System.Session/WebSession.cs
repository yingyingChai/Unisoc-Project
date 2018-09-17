using KaYi.Web.Infrastructure.Model.Login;
using KaYi.Web.Infrastructure.Model.System.WebSiteProtect;
using System;
using System.Collections.Generic;
namespace KaYi.Web.Infrastructure.Model.System.Session
{
	public class WebSession
	{
		private IList<string> joinedDepartments = new List<string>();
		private UserTypes userType = UserTypes.UnAuthorized;
		private DateTime lastRequestTime;
		private string _sessionid = string.Empty;
		private string _clientip = string.Empty;
		private DateTime _starttime;
		private DateTime _endtime;
		private string _source = string.Empty;
		private string _keyword = string.Empty;
		private string _hostname = string.Empty;
		private string _browser = string.Empty;
		private string _clientid = string.Empty;
		private int _visittimes;
		private DateTime createTime;
		private DateTime updateTime;
		private string updateStamp = string.Empty;
		private string accountID = string.Empty;
		private BlockType blockType;
		private string requestPage = string.Empty;
		private string storagePath = string.Empty;
		private QQClient qqClientInfo = new QQClient();
		private SinaClient sinaClientInfo = new SinaClient();
		private object loginAccount;
		private string storageRelativePath = string.Empty;
		public SinaClient SinaClientInfo
		{
			get
			{
				return this.sinaClientInfo;
			}
			set
			{
				this.sinaClientInfo = value;
			}
		}
		public object LoginAccount
		{
			get
			{
				return this.loginAccount;
			}
			set
			{
				this.loginAccount = value;
			}
		}
		public QQClient QqClientInfo
		{
			get
			{
				return this.qqClientInfo;
			}
			set
			{
				this.qqClientInfo = value;
			}
		}
		public string StoragePath
		{
			get
			{
				return this.storagePath;
			}
			set
			{
				this.storagePath = value;
			}
		}
		public string StorageRelativePath
		{
			get
			{
				return this.storageRelativePath;
			}
			set
			{
				this.storageRelativePath = value;
			}
		}
		public IList<string> JoinedDepartments
		{
			get
			{
				return this.joinedDepartments;
			}
			set
			{
				this.joinedDepartments = value;
			}
		}
		public UserTypes UserType
		{
			get
			{
				return this.userType;
			}
			set
			{
				this.userType = value;
			}
		}
		public DateTime LastRequestTime
		{
			get
			{
				return this.lastRequestTime;
			}
			set
			{
				this.lastRequestTime = value;
			}
		}
		public string SessionID
		{
			get
			{
				return this._sessionid;
			}
			set
			{
				this._sessionid = value;
			}
		}
		public string ClientIP
		{
			get
			{
				return this._clientip;
			}
			set
			{
				this._clientip = value;
			}
		}
		public DateTime StartTime
		{
			get
			{
				return this._starttime;
			}
			set
			{
				this._starttime = value;
			}
		}
		public DateTime EndTime
		{
			get
			{
				return this._endtime;
			}
			set
			{
				this._endtime = value;
			}
		}
		public string Source
		{
			get
			{
				return this._source;
			}
			set
			{
				this._source = value;
			}
		}
		public string Keyword
		{
			get
			{
				return this._keyword;
			}
			set
			{
				this._keyword = value;
			}
		}
		public string HostName
		{
			get
			{
				return this._hostname;
			}
			set
			{
				this._hostname = value;
			}
		}
		public string Browser
		{
			get
			{
				return this._browser;
			}
			set
			{
				this._browser = value;
			}
		}
		public string ClientID
		{
			get
			{
				return this._clientid;
			}
			set
			{
				this._clientid = value;
			}
		}
		public int VisitTimes
		{
			get
			{
				return this._visittimes;
			}
			set
			{
				this._visittimes = value;
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
		public BlockType BlockType
		{
			get
			{
				return this.blockType;
			}
			set
			{
				this.blockType = value;
			}
		}
		public string RequestPage
		{
			get
			{
				return this.requestPage;
			}
			set
			{
				this.requestPage = value;
			}
		}
	}
}
