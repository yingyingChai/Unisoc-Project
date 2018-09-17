using System;
using System.Configuration;
namespace KaYi.Database
{
	public static class DALService
	{
		private static DBFactory dbFactory = new DBFactory();
		private static string DBTYPE = ConfigurationManager.AppSettings["Database"];
		private static string DBCONN = ConfigurationManager.AppSettings["dbConnectString"];
		public static DBAbstract Database = DALService.dbFactory.OpenDatabase(DALService.DBTYPE, DALService.DBCONN);
	}
}
