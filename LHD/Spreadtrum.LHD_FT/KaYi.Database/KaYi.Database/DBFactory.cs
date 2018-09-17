using System;
namespace KaYi.Database
{
	public class DBFactory
	{
		private string m_DB_CONNECTION_STRING = string.Empty;
		public DBAbstract OpenDatabase(string dbType, string dbConnectString)
		{
			DBAbstract dBAbstract = null;
			if (!(dbType == "OLEDB"))
			{
				if (!(dbType == "SQLSERVER"))
				{
					if (dbType == "ORACLE")
					{
						dBAbstract = new DBOracle(dbConnectString);
						dBAbstract.DB_TYPE = dbType;
					}
				}
				else
				{
					dBAbstract = new DBSQLServer(dbConnectString);
					dBAbstract.DB_TYPE = dbType;
				}
			}
			else
			{
				dBAbstract = new DBOLEDB(dbConnectString);
				dBAbstract.DB_TYPE = dbType;
			}
			return dBAbstract;
		}
	}
}
