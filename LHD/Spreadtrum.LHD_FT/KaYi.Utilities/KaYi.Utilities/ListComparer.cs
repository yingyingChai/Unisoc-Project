using System;
using System.Collections.Generic;
using System.Reflection;
namespace KaYi.Utilities
{
	public class ListComparer<T> : IComparer<T>
	{
		private string propertyName;
		public ListComparer(string PropertyName)
		{
			this.propertyName = PropertyName;
		}
		public int Compare(T x, T y)
		{
			PropertyInfo property = typeof(T).GetProperty(this.propertyName);
			if (property.PropertyType == Type.GetType("System.Int16"))
			{
				int num = 0;
				int value = 0;
				if (property.GetValue(x, null) != null)
				{
					num = (int)Convert.ToInt16(property.GetValue(x, null).ToString());
				}
				if (property.GetValue(y, null) != null)
				{
					value = Convert.ToInt32(property.GetValue(y, null).ToString());
				}
				return num.CompareTo(value);
			}
			if (property.PropertyType == Type.GetType("System.Int32"))
			{
				int num2 = 0;
				int value2 = 0;
				if (property.GetValue(x, null) != null)
				{
					num2 = Convert.ToInt32(property.GetValue(x, null).ToString());
				}
				if (property.GetValue(y, null) != null)
				{
					value2 = Convert.ToInt32(property.GetValue(y, null).ToString());
				}
				return num2.CompareTo(value2);
			}
			if (property.PropertyType == Type.GetType("System.Double"))
			{
				double num3 = 0.0;
				double value3 = 0.0;
				if (property.GetValue(x, null) != null)
				{
					num3 = Convert.ToDouble(property.GetValue(x, null).ToString());
				}
				if (property.GetValue(y, null) != null)
				{
					value3 = Convert.ToDouble(property.GetValue(y, null).ToString());
				}
				return num3.CompareTo(value3);
			}
			if (property.PropertyType == Type.GetType("System.DateTime"))
			{
				DateTime dateTime = DateTime.Now;
				DateTime value4 = DateTime.Now;
				if (property.GetValue(x, null) != null)
				{
					dateTime = Convert.ToDateTime(property.GetValue(x, null).ToString());
				}
				if (property.GetValue(y, null) != null)
				{
					value4 = Convert.ToDateTime(property.GetValue(y, null).ToString());
				}
				return dateTime.CompareTo(value4);
			}
			if (property.PropertyType == Type.GetType("System.String") || property.PropertyType == Type.GetType("System.Boolean"))
			{
				string text = string.Empty;
				string strB = string.Empty;
				if (property.GetValue(x, null) != null)
				{
					text = property.GetValue(x, null).ToString();
				}
				if (property.GetValue(y, null) != null)
				{
					strB = property.GetValue(y, null).ToString();
				}
				return text.CompareTo(strB);
			}
			return 0;
		}
	}
}
