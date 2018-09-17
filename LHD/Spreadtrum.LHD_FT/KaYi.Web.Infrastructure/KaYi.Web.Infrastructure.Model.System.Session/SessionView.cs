using System;
namespace KaYi.Web.Infrastructure.Model.System.Session
{
	public class SessionView
	{
		private DateTime _StartDate;
		private string _SessionID = string.Empty;
		private string _HostName = string.Empty;
		private string _Browser = string.Empty;
		private string _AccountID = string.Empty;
		private string _ClientIP = string.Empty;
		private int _VisitTimes;
		private string _Source = string.Empty;
		private string _Keyword = string.Empty;
		private DateTime _StartTime;
		private DateTime _LastRequestTime;
		private int _Duration;
		private string _Country = string.Empty;
		private string _Province = string.Empty;
		private string _City = string.Empty;
		private string _District = string.Empty;
		private string _CountryCode = string.Empty;
		private string _ProvinceCode = string.Empty;
		private string _DistrictCode = string.Empty;
		private string _ISP = string.Empty;
		private string _ConnectType = string.Empty;
		private string _Description = string.Empty;
		private string _LocationID = string.Empty;
		private int _YY;
		private int _MM;
		private int _DD;
		private int _PageHits;
		public DateTime StartDate
		{
			get
			{
				return this._StartDate;
			}
			set
			{
				this._StartDate = value;
			}
		}
		public string SessionID
		{
			get
			{
				return this._SessionID;
			}
			set
			{
				this._SessionID = value;
			}
		}
		public string HostName
		{
			get
			{
				return this._HostName;
			}
			set
			{
				this._HostName = value;
			}
		}
		public string Browser
		{
			get
			{
				return this._Browser;
			}
			set
			{
				this._Browser = value;
			}
		}
		public string AccountID
		{
			get
			{
				return this._AccountID;
			}
			set
			{
				this._AccountID = value;
			}
		}
		public string ClientIP
		{
			get
			{
				return this._ClientIP;
			}
			set
			{
				this._ClientIP = value;
			}
		}
		public int VisitTimes
		{
			get
			{
				return this._VisitTimes;
			}
			set
			{
				this._VisitTimes = value;
			}
		}
		public string Source
		{
			get
			{
				return this._Source;
			}
			set
			{
				this._Source = value;
			}
		}
		public string Keyword
		{
			get
			{
				return this._Keyword;
			}
			set
			{
				this._Keyword = value;
			}
		}
		public DateTime StartTime
		{
			get
			{
				return this._StartTime;
			}
			set
			{
				this._StartTime = value;
			}
		}
		public DateTime LastRequestTime
		{
			get
			{
				return this._LastRequestTime;
			}
			set
			{
				this._LastRequestTime = value;
			}
		}
		public int Duration
		{
			get
			{
				return this._Duration;
			}
			set
			{
				this._Duration = value;
			}
		}
		public string Country
		{
			get
			{
				return this._Country;
			}
			set
			{
				this._Country = value;
			}
		}
		public string Province
		{
			get
			{
				return this._Province;
			}
			set
			{
				this._Province = value;
			}
		}
		public string City
		{
			get
			{
				return this._City;
			}
			set
			{
				this._City = value;
			}
		}
		public string District
		{
			get
			{
				return this._District;
			}
			set
			{
				this._District = value;
			}
		}
		public string CountryCode
		{
			get
			{
				return this._CountryCode;
			}
			set
			{
				this._CountryCode = value;
			}
		}
		public string ProvinceCode
		{
			get
			{
				return this._ProvinceCode;
			}
			set
			{
				this._ProvinceCode = value;
			}
		}
		public string DistrictCode
		{
			get
			{
				return this._DistrictCode;
			}
			set
			{
				this._DistrictCode = value;
			}
		}
		public string ISP
		{
			get
			{
				return this._ISP;
			}
			set
			{
				this._ISP = value;
			}
		}
		public string ConnectType
		{
			get
			{
				return this._ConnectType;
			}
			set
			{
				this._ConnectType = value;
			}
		}
		public string Description
		{
			get
			{
				return this._Description;
			}
			set
			{
				this._Description = value;
			}
		}
		public string LocationID
		{
			get
			{
				return this._LocationID;
			}
			set
			{
				this._LocationID = value;
			}
		}
		public int YY
		{
			get
			{
				return this._YY;
			}
			set
			{
				this._YY = value;
			}
		}
		public int MM
		{
			get
			{
				return this._MM;
			}
			set
			{
				this._MM = value;
			}
		}
		public int DD
		{
			get
			{
				return this._DD;
			}
			set
			{
				this._DD = value;
			}
		}
		public int PageHits
		{
			get
			{
				return this._PageHits;
			}
			set
			{
				this._PageHits = value;
			}
		}
	}
}
