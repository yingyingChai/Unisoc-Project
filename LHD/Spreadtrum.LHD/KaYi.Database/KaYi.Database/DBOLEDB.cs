using KaYi.Services.EventServices;
using KaYi.Services.EventServices.Model;
using System;
using System.Data;
using System.Data.OleDb;
namespace KaYi.Database
{
	public class DBOLEDB : DBAbstract
	{
		private OleDbConnection m_conn;
		private OleDbCommand m_cmd;
		public DBOLEDB(string connectionString)
		{
			this.m_conn = new OleDbConnection(connectionString);
		}
		public override void Open()
		{
			if (this.m_conn != null && this.m_conn.State == ConnectionState.Closed)
			{
				this.m_conn.Open();
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
			try
			{
				this.Open();
				this.m_cmd = new OleDbCommand();
				this.m_cmd.Connection = this.m_conn;
				this.m_cmd.CommandType = CommandType.Text;
				this.m_cmd.CommandText = SQLStatement;
				this.m_cmd.ExecuteNonQuery();
			}
			catch (OleDbException ex)
			{
				EventService.AppendLog(new EventLog("DAL_COMMON.OLEDB", "DBERROR", ex.Message));
				throw ex;
			}
		}
		public override DataSet ExeSQLReturnDataSet(string SQLStatement, string TableName, int pageIndex, int pageSize)
		{
			DataSet dataSet = new DataSet();
			try
			{
				this.Open();
				this.m_cmd = new OleDbCommand();
				this.m_cmd.Connection = this.m_conn;
				this.m_cmd.CommandType = CommandType.Text;
				this.m_cmd.CommandText = SQLStatement;
				using (OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter())
				{
					oleDbDataAdapter.SelectCommand = this.m_cmd;
					oleDbDataAdapter.Fill(dataSet, pageIndex * pageSize, pageSize, TableName);
				}
			}
			catch (Exception ex)
			{
				EventService.AppendLog(new EventLog("DAL_COMMON.DBOLEDB", "DBERROR", ex.Message));
				throw ex;
			}
			return dataSet;
		}
		public override DataSet ExeSQLReturnDataSet(string SQLStatement, string TableName)
		{
			DataSet dataSet = new DataSet();
			try
			{
				this.Open();
				this.m_cmd = new OleDbCommand();
				this.m_cmd.Connection = this.m_conn;
				this.m_cmd.CommandType = CommandType.Text;
				this.m_cmd.CommandText = SQLStatement;
				using (OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter())
				{
					oleDbDataAdapter.SelectCommand = this.m_cmd;
					oleDbDataAdapter.Fill(dataSet, TableName);
				}
			}
			catch (Exception ex)
			{
				EventService.AppendLog(new EventLog("DAL_COMMON.DBSQLServer", "DBERROR", ex.Message));
				throw ex;
			}
			return dataSet;
		}
		public override string GetCompareExpression(string fieldName, Condition condition)
		{
			string text = string.Empty;
			switch (condition.Operator)
			{
			case Operator.Between:
				text = string.Format(" {0} between '{1}' and '{2}' ", fieldName, condition.OperandA.ToString(), condition.OperandB.ToString());
				break;
			case Operator.LessThan:
				text = string.Format(" {0}<'{1}' ", fieldName, condition.OperandA.ToString());
				break;
			case Operator.SmallerOrEqualTo:
				text = string.Format(" {0}<='{1}' ", fieldName, condition.OperandA.ToString());
				break;
			case Operator.EqualTo:
				if (condition.OperandA is int || condition.OperandA is short || condition.OperandA is int)
				{
					text = string.Format(" {0}={1} ", fieldName, condition.OperandA.ToString());
				}
				else
				{
					text = string.Format(" {0}='{1}' ", fieldName, condition.OperandA.ToString());
				}
				break;
			case Operator.GreaterThan:
				text = string.Format(" {0}>'{1}' ", fieldName, condition.OperandA.ToString());
				break;
			case Operator.GreaterOrEqualTo:
				text = string.Format(" {0}>='{1}' ", fieldName, condition.OperandA.ToString());
				break;
			case Operator.Like:
				text = string.Format(" {0} like '%{1}%' ", fieldName, condition.OperandA.ToString());
				break;
			}
			if (condition.OperandA is DateTime || condition.OperandB is DateTime)
			{
				text = text.Replace("'", "#");
			}
			return text;
		}
	}
}
