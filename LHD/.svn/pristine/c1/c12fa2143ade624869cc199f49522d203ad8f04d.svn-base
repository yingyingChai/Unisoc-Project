using System;
using System.Reflection;
namespace KaYi.Web.Infrastructure.Pages
{
	public class Entity
	{
		private string entityName = string.Empty;
		private string entityID = string.Empty;
		private object entityInstance;
		public string EntityName
		{
			get
			{
				return this.entityName;
			}
			set
			{
				this.entityName = value;
			}
		}
		public string ID
		{
			get
			{
				return this.entityID;
			}
			set
			{
				this.entityID = value;
			}
		}
		public object EntityInstance
		{
			get
			{
				return this.entityInstance;
			}
			set
			{
				this.entityInstance = value;
			}
		}
		public Entity(object obj)
		{
			this.EntityInstance = obj;
		}
		public object GetValue(string propertyName)
		{
			PropertyInfo[] properties = this.EntityInstance.GetType().GetProperties();
			for (int i = 0; i < properties.Length; i++)
			{
				PropertyInfo propertyInfo = properties[i];
				if (propertyInfo.Name.ToUpper().Equals(propertyName.ToUpper()))
				{
					return propertyInfo.GetValue(this.EntityInstance, null);
				}
			}
			return null;
		}
		public string GetString(string propertyName)
		{
			object value = this.GetValue(propertyName);
			if (value == null)
			{
				return null;
			}
			return value.ToString();
		}
		public DateTime GetDateTime(string propertyName)
		{
			object value = this.GetValue(propertyName);
			if (value == null)
			{
				return Convert.ToDateTime("0001-01-01");
			}
			return Convert.ToDateTime(value);
		}
		public bool GetBool(string propertyName)
		{
			object value = this.GetValue(propertyName);
			return value != null && Convert.ToBoolean(value);
		}
		public int GetInt32(string propertyName)
		{
			return Convert.ToInt32(this.GetValue(propertyName));
		}
		public float GetSingle(string propertyName)
		{
			return Convert.ToSingle(this.GetValue(propertyName));
		}
		public double GetDouble(string propertyName)
		{
			return Convert.ToDouble(this.GetValue(propertyName));
		}
	}
}
