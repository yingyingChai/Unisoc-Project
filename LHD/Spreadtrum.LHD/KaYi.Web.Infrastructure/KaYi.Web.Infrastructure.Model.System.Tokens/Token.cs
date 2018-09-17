using System;
namespace KaYi.Web.Infrastructure.Model.System.Tokens
{
	public class Token
	{
		private string _TokenID = string.Empty;
		private bool _Used = true;
		private string _Usage = string.Empty;
		private DateTime _CreateTime;
		private DateTime _UsedTime;
		private string objectID = string.Empty;
		private string tag = string.Empty;
		private string allowIPs = string.Empty;
		private DateTime expireTime;
		public string AllowIPs
		{
			get
			{
				return this.allowIPs;
			}
			set
			{
				this.allowIPs = value;
			}
		}
		public DateTime ExpireTime
		{
			get
			{
				return this.expireTime;
			}
			set
			{
				this.expireTime = value;
			}
		}
		public string ObjectID
		{
			get
			{
				return this.objectID;
			}
			set
			{
				this.objectID = value;
			}
		}
		public string Tag
		{
			get
			{
				return this.tag;
			}
			set
			{
				this.tag = value;
			}
		}
		public string TokenID
		{
			get
			{
				return this._TokenID;
			}
			set
			{
				this._TokenID = value;
			}
		}
		public bool Used
		{
			get
			{
				return this._Used;
			}
			set
			{
				this._Used = value;
			}
		}
		public string Usage
		{
			get
			{
				return this._Usage;
			}
			set
			{
				this._Usage = value;
			}
		}
		public DateTime CreateTime
		{
			get
			{
				return this._CreateTime;
			}
			set
			{
				this._CreateTime = value;
			}
		}
		public DateTime UsedTime
		{
			get
			{
				return this._UsedTime;
			}
			set
			{
				this._UsedTime = value;
			}
		}
	}
}
