using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Web;
namespace KaYi.Web.Infrastructure.Services
{
	public class ExcelService<T> where T : new()
	{
		public void CreateExcel(HttpResponse resp, IList<T> lt, string FileName)
		{
			PropertyInfo[] properties = typeof(T).GetProperties(BindingFlags.Instance | BindingFlags.Public);
			resp.Charset = "UTF-8";
			resp.ContentEncoding = Encoding.Default;
			resp.ContentType = "application/ms-excel";
			resp.AddHeader("Content-Disposition", "attachment; filename=" + HttpUtility.UrlEncode(FileName, Encoding.UTF8));
			string text = "";
			string text2 = "";
			int i = 0;
			int num = properties.Length;
			while (i < num)
			{
				string name = properties[i].Name;
				text = text + name + "\t";
				i++;
			}
			text += "\n";
			resp.Write(text);
			foreach (T current in lt)
			{
				if (current != null)
				{
					i = 0;
					num = properties.Length;
					while (i < num)
					{
						PropertyInfo propertyInfo = properties[i];
						string text3 = string.Format("{0}", propertyInfo.GetValue(current, null)).Replace("\n", "");
						if (text3 == "")
						{
							text2 += "\t";
						}
						else
						{
							text2 = text2 + text3 + "\t";
						}
						i++;
					}
					text2 += "\n";
					resp.Write(text2);
					text2 = "";
				}
			}
			resp.End();
		}
	}
}
