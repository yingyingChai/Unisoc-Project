using System;
namespace KaYi.Web.Infrastructure.Model.UI.Items
{
	public class Item
	{
		private string _id = string.Empty;
		private string _text = string.Empty;
		private object _tag = string.Empty;
		public string Id
		{
			get
			{
				return this._id;
			}
			set
			{
				this._id = value;
			}
		}
		public string Text
		{
			get
			{
				return this._text;
			}
			set
			{
				this._text = value;
			}
		}
		public object Tag
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
		public Item(string id, string text)
		{
			this._id = id;
			this._text = text;
		}
		public Item(string id, string text, object tag)
		{
			this._id = id;
			this._text = text;
			this._tag = tag;
		}
	}
}
