using System;
namespace KaYi.Database
{
	public class DataFormatUtilities
	{
		public static object processValues(object obj)
		{
			if (obj == null)
			{
				return "";
			}
			if (obj is string)
			{
				return obj.ToString().Replace("'", "''");
			}
			return obj;
		}
	}
}
