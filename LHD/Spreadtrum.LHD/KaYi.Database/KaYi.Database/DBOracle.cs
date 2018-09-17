using System;
using System.Data;
using System.Data.OracleClient;
namespace KaYi.Database
{
	public class DBOracle : DBAbstract
	{
		private OracleConnection m_OracleConnection;
		private OracleCommand m_OracleCommand;
		public override void Open()
		{
		}
		public override void Close()
		{
		}
		public override void ExeSQLStatement(string SQLStatement)
		{
		}
		public override DataSet ExeSQLReturnDataSet(string SQLStatement, string TableName, int pageIndex, int pageSize)
		{
			return null;
		}
		public override DataSet ExeSQLReturnDataSet(string SQLStatement, string TableName)
		{
			return null;
		}
		public DBOracle(string ConnectionString)
		{
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
					result = string.Format(" {0}='{1}' ", fieldName, condition.OperandA.ToString());
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
