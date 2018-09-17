using KaYi.Services.EventServices;
using KaYi.Services.EventServices.Model;
using System;
using System.Data;
using System.Data.SqlClient;
namespace KaYi.Database
{
	public class DBSQLServer : DBAbstract
	{
		private SqlConnection m_conn;
		public override void Open()
		{
			if (this.m_conn != null && this.m_conn.State == ConnectionState.Closed)
			{
				try
				{
					this.m_conn.Open();
				}
				catch (Exception ex)
				{
					EventService.AppendLog(new EventLog("DAL_COMMON.DBSQLServer", "DBERROR:Open()", ex.Message));
					base.throwException(ex);
				}
			}
		}
		public override void Close()
		{
			if (this.m_conn != null && this.m_conn.State == ConnectionState.Open)
			{
				this.m_conn.Close();
			}
		}
		public override void ExeSQLStatement(string SQLStatement)
		{
			SqlCommand sqlCommand = new SqlCommand();
			try
			{
				if (this.m_conn == null || this.m_conn.State != ConnectionState.Open)
				{
					this.Open();
				}
				sqlCommand.Connection = this.m_conn;
				sqlCommand.CommandType = CommandType.Text;
				sqlCommand.CommandText = SQLStatement;
				sqlCommand.ExecuteNonQuery();
				sqlCommand.Dispose();
			}
			catch (SqlException ex)
			{
				EventService.AppendLog(new EventLog("DAL_COMMON.DBSQLServer", "DBERROR:void ExeSQLStatement", ex.Message));
				this.Close();
				throw ex;
			}
		}
		public override DataSet ExeSQLReturnDataSet(string SQLStatement, string TableName, int pageIndex, int pageSize)
		{
			SqlCommand sqlCommand = new SqlCommand();
			DataSet dataSet = new DataSet();
			try
			{
				if (this.m_conn == null || this.m_conn.State != ConnectionState.Open)
				{
					this.Open();
				}
				sqlCommand.Connection = this.m_conn;
				sqlCommand.CommandType = CommandType.Text;
				sqlCommand.CommandText = SQLStatement;
				using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter())
				{
					sqlDataAdapter.SelectCommand = sqlCommand;
					sqlDataAdapter.Fill(dataSet, pageIndex * pageSize, pageSize, TableName);
				}
				sqlCommand.Dispose();
				sqlCommand = null;
			}
			catch (Exception ex)
			{
				EventService.AppendLog(new EventLog("DAL_COMMON.DBSQLServer", "DBERROR:DataSet ExeSQLReturnDataSet(string SQLStatement, string TableName,int pageIndex, int pageSize)", ex.Message));
				this.Close();
			}
			return dataSet;
		}
		public override DataSet ExeSQLReturnDataSet(string SQLStatement, string TableName)
		{
			SqlCommand sqlCommand = new SqlCommand();
			DataSet dataSet = new DataSet();
			try
			{
				if (this.m_conn == null || this.m_conn.State != ConnectionState.Open)
				{
					this.Open();
				}
				sqlCommand.Connection = this.m_conn;
				sqlCommand.CommandType = CommandType.Text;
				sqlCommand.CommandText = SQLStatement;
				using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter())
				{
					sqlDataAdapter.SelectCommand = sqlCommand;
					sqlDataAdapter.Fill(dataSet, TableName);
				}
				sqlCommand.Dispose();
				sqlCommand = null;
			}
			catch (Exception ex)
			{
				EventService.AppendLog(new EventLog("DAL_COMMON.DBSQLServer", "DBERROR:DataSet ExeSQLReturnDataSet(string SQLStatement, string TableName)", ex.Message));
				this.Close();
			}
			return dataSet;
		}
		public DBSQLServer(string connectionString)
		{
			this.m_conn = new SqlConnection(connectionString);
		}
		public override string GetCompareExpression(string fieldName, Condition condition)
		{
			string result = string.Empty;
			switch (condition.Operator)
			{
			case Operator.Between:
				result = string.Format(" {0} between '{1}' and '{2}' ", fieldName, condition.OperandA.ToString(), condition.OperandB.ToString());
				break;
			case Operator.LessThan:
				result = string.Format(" {0}<'{1}' ", fieldName, condition.OperandA.ToString());
				break;
			case Operator.SmallerOrEqualTo:
				result = string.Format(" {0}<='{1}' ", fieldName, condition.OperandA.ToString());
				break;
			case Operator.EqualTo:
				if (condition.OperandA is int || condition.OperandA is short || condition.OperandA is int)
				{
					result = string.Format(" {0}={1} ", fieldName, condition.OperandA.ToString());
				}
				else
				{
					if (condition.OperandA is bool)
					{
						if ((bool)condition.OperandA)
						{
							result = string.Format(" {0}={1} ", fieldName, "1");
						}
						else
						{
							result = string.Format(" {0}={1} ", fieldName, "0");
						}
					}
					else
					{
						result = string.Format(" {0}='{1}' ", fieldName, condition.OperandA.ToString());
					}
				}
				break;
			case Operator.GreaterThan:
				result = string.Format(" {0}>'{1}' ", fieldName, condition.OperandA.ToString());
				break;
			case Operator.GreaterOrEqualTo:
				result = string.Format(" {0}>='{1}' ", fieldName, condition.OperandA.ToString());
				break;
			case Operator.Like:
				result = string.Format(" {0} like '%{1}%' ", fieldName, condition.OperandA.ToString());
				break;
			}
			return result;
		}
	}
}
