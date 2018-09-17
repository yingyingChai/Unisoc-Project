using System;
using System.Data;
using System.Runtime.CompilerServices;
namespace KaYi.Database
{
	public abstract class DBAbstract
	{
		public delegate void ErrorEventHandler(Exception ex);
		private string m_DB_TYPE = string.Empty;
		[method: CompilerGenerated]
		[CompilerGenerated]
		public event DBAbstract.ErrorEventHandler ErrorArrived;
		public string DB_TYPE
		{
			get
			{
				return this.m_DB_TYPE;
			}
			set
			{
				this.m_DB_TYPE = value;
			}
		}
		public DBAbstract()
		{
		}
		public abstract void Open();
		public abstract void Close();
		public abstract void ExeSQLStatement(string SQLStatement);
		public abstract DataSet ExeSQLReturnDataSet(string SQLStatement, string TableName);
		public abstract DataSet ExeSQLReturnDataSet(string SQLStatement, string TableName, int pageIndex, int pageSize);
		public abstract string GetCompareExpression(string fieldName, Condition condition);
		public void throwException(Exception ex)
		{
			this.Close();
			if (this.ErrorArrived != null)
			{
				this.ErrorArrived(ex);
			}
		}
	}
}
