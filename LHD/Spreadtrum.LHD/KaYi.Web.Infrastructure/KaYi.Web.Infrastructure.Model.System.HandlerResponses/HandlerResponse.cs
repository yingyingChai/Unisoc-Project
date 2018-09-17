using KaYi.Utilities;
using Newtonsoft.Json;
using System;
using System.Web;
namespace KaYi.Web.Infrastructure.Model.System.HandlerResponses
{
	public class HandlerResponse
	{
		private string _code = string.Empty;
		private string _message = string.Empty;
		private string _responseType = string.Empty;
		private string _tipType = string.Empty;
		private string _nextUrl = string.Empty;
		private string _script = string.Empty;
		private string _tag = string.Empty;
		private HandlerResponse _this;
		public string Code
		{
			get
			{
				return this._code;
			}
			set
			{
				this._code = value;
			}
		}
		public string Message
		{
			get
			{
				return this._message;
			}
			set
			{
				this._message = value;
			}
		}
		public string ResponseType
		{
			get
			{
				return this._responseType;
			}
			set
			{
				this._responseType = value;
			}
		}
		public string TipType
		{
			get
			{
				return this._tipType;
			}
			set
			{
				this._tipType = value;
			}
		}
		public string NextUrl
		{
			get
			{
				return this._nextUrl;
			}
			set
			{
				this._nextUrl = value;
			}
		}
		public string Script
		{
			get
			{
				return this._script;
			}
			set
			{
				this._script = value;
			}
		}
		public string Tag
		{
			get
			{
				return this._tag;
			}
			set
			{
				this._tag = value;
			}
		}
		public HandlerResponse(string code, string message, string responseType, string tipType, string nextUrl, string script, string tag)
		{
			this._code = code;
			this._message = message;
			this._responseType = responseType;
			this._tipType = tipType;
			this._nextUrl = nextUrl;
			this._script = script;
			this._tag = tag;
			this._this = this;
		}
		public static string GetResponseByCondition(HttpResponse response, string value, Comparations comparations, string errorPrompt)
		{
			bool flag = false;
			if (comparations != Comparations.NotNull)
			{
				if (comparations == Comparations.IsDate && !ValueChecker.IsDate(value))
				{
					flag = true;
				}
			}
			else
			{
				if (StringHelper.isNullOrEmpty(value))
				{
					flag = true;
				}
			}
			if (flag)
			{
				response.Write(new HandlerResponse("-1", errorPrompt, ResponseTypes.Tip.ToString(), TipTypes.Error.ToString(), "", "", "").GetResponseText());
				return "KAYI_ERROR";
			}
			return value;
		}
		public string GetResponseText()
		{
			return JsonConvert.SerializeObject(this._this);
		}
		public string GenerateJsonResponse()
		{
			return JsonConvert.SerializeObject(this._this);
		}
	}
}
