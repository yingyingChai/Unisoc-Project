using System;
using System.Configuration;
using System.Xml;
namespace KaYi.Database
{
	public static class Statics
	{
		public static XmlDocument xdoc;
		public static void LoadXml()
		{
			Statics.xdoc = new XmlDocument();
			string filename = AppDomain.CurrentDomain.BaseDirectory + ConfigurationManager.AppSettings["DBSchemaFile"];
			Statics.xdoc.Load(filename);
		}
	}
}
