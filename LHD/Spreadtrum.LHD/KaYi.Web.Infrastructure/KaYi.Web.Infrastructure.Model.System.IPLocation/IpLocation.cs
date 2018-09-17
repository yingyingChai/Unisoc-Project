using System;
namespace KaYi.Web.Infrastructure.Model.System.IPLocation
{
	public class IpLocation
	{
		private string countryCode = string.Empty;
		private string provinceCode = string.Empty;
		private string cityCode = string.Empty;
		private string districtCode = string.Empty;
		private string postalCode = string.Empty;
		private double longitude;
		private double latitude;
		private long startIPNum;
		private long endIPNum;
		private DateTime createTime;
		private DateTime updateTime;
		private string updateStamp;
		private string _locationid = string.Empty;
		private string _startat = string.Empty;
		private string _endat = string.Empty;
		private string _country = string.Empty;
		private string _province = string.Empty;
		private string _city = string.Empty;
		private string _district = string.Empty;
		private string _isp = string.Empty;
		private string _connecttype = string.Empty;
		private string _description = string.Empty;
		public string CountryCode
		{
			get
			{
				return this.countryCode;
			}
			set
			{
				this.countryCode = value;
			}
		}
		public string ProvinceCode
		{
			get
			{
				return this.provinceCode;
			}
			set
			{
				this.provinceCode = value;
			}
		}
		public string CityCode
		{
			get
			{
				return this.cityCode;
			}
			set
			{
				this.cityCode = value;
			}
		}
		public string DistrictCode
		{
			get
			{
				return this.districtCode;
			}
			set
			{
				this.districtCode = value;
			}
		}
		public string PostalCode
		{
			get
			{
				return this.postalCode;
			}
			set
			{
				this.postalCode = value;
			}
		}
		public double Longitude
		{
			get
			{
				return this.longitude;
			}
			set
			{
				this.longitude = value;
			}
		}
		public double Latitude
		{
			get
			{
				return this.latitude;
			}
			set
			{
				this.latitude = value;
			}
		}
		public long StartIPNum
		{
			get
			{
				return this.startIPNum;
			}
			set
			{
				this.startIPNum = value;
			}
		}
		public long EndIPNum
		{
			get
			{
				return this.endIPNum;
			}
			set
			{
				this.endIPNum = value;
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
		public string LocationID
		{
			get
			{
				return this._locationid;
			}
			set
			{
				this._locationid = value;
			}
		}
		public string StartAt
		{
			get
			{
				return this._startat;
			}
			set
			{
				this._startat = value;
			}
		}
		public string EndAt
		{
			get
			{
				return this._endat;
			}
			set
			{
				this._endat = value;
			}
		}
		public string Country
		{
			get
			{
				return this._country;
			}
			set
			{
				this._country = value;
			}
		}
		public string Province
		{
			get
			{
				return this._province;
			}
			set
			{
				this._province = value;
			}
		}
		public string City
		{
			get
			{
				return this._city;
			}
			set
			{
				this._city = value;
			}
		}
		public string District
		{
			get
			{
				return this._district;
			}
			set
			{
				this._district = value;
			}
		}
		public string ISP
		{
			get
			{
				return this._isp;
			}
			set
			{
				this._isp = value;
			}
		}
		public string ConnectType
		{
			get
			{
				return this._connecttype;
			}
			set
			{
				this._connecttype = value;
			}
		}
		public string Description
		{
			get
			{
				return this._description;
			}
			set
			{
				this._description = value;
			}
		}
	}
}
