using System;
namespace KaYi.Web.Infrastructure.Model.System.ProvinceAndCity
{
	[Serializable]
	public class City
	{
		private string _cityid = string.Empty;
		private string _provinceid = string.Empty;
		private string _cityname = string.Empty;
		private int _sequence;
		private string _updateStamp = string.Empty;
		private int _recordState;
		private string _lastOperator = string.Empty;
		private DateTime _lastUpdateTime;
		private string _lastOperatorName = string.Empty;
		public string CityID
		{
			get
			{
				return this._cityid;
			}
			set
			{
				this._cityid = value;
			}
		}
		public string ProvinceID
		{
			get
			{
				return this._provinceid;
			}
			set
			{
				this._provinceid = value;
			}
		}
		public string CityName
		{
			get
			{
				return this._cityname;
			}
			set
			{
				this._cityname = value;
			}
		}
		public int Sequence
		{
			get
			{
				return this._sequence;
			}
			set
			{
				this._sequence = value;
			}
		}
		public string UpdateStamp
		{
			get
			{
				return this._updateStamp;
			}
			set
			{
				this._updateStamp = value;
			}
		}
		public int RecordState
		{
			get
			{
				return this._recordState;
			}
			set
			{
				this._recordState = value;
			}
		}
		public string LastOperator
		{
			get
			{
				return this._lastOperator;
			}
			set
			{
				this._lastOperator = value;
			}
		}
		public DateTime LastUpdateTime
		{
			get
			{
				return this._lastUpdateTime;
			}
			set
			{
				this._lastUpdateTime = value;
			}
		}
		public string LastOperatorName
		{
			get
			{
				return this._lastOperatorName;
			}
			set
			{
				this._lastOperatorName = value;
			}
		}
	}
}
