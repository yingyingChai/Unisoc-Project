using System;
using System.Text;
namespace KaYi.Utilities
{
	public static class IP
	{
		public static long IP2Long(string strIP)
		{
			long[] array = new long[4];
			string[] array2 = strIP.Split(new char[]
			{
				'.'
			});
			array[0] = long.Parse(array2[0]);
			array[1] = long.Parse(array2[1]);
			array[2] = long.Parse(array2[2]);
			array[3] = long.Parse(array2[3]);
			return (array[0] << 24) + (array[1] << 16) + (array[2] << 8) + array[3];
		}
		public static string Long2IP(long longIP)
		{
			StringBuilder expr_0A = new StringBuilder("");
			expr_0A.Append(longIP >> 24);
			expr_0A.Append(".");
			expr_0A.Append((longIP & 16777215L) >> 16);
			expr_0A.Append(".");
			expr_0A.Append((longIP & 65535L) >> 8);
			expr_0A.Append(".");
			expr_0A.Append(longIP & 255L);
			return expr_0A.ToString();
		}
	}
}
