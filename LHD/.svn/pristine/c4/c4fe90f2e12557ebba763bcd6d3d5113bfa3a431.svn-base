using KaYi.Database;
using KaYi.Utilities;
using KaYi.Web.Infrastructure.Model.Evaluator;
using System;
using System.Collections.Generic;
using System.Data;
namespace KaYi.Web.Infrastructure.DAL.Evaluator
{
	public class EvaluateGateway
	{
		private DALGateway<Evaluate> dbGateway = new DALGateway<Evaluate>();
		public EvaluateGateway()
		{
			this.dbGateway.LoadSchema("T_EVALUATES");
		}
		public void AddNew(Evaluate newEvaluate)
		{
			this.dbGateway.AddNew(newEvaluate);
		}
		public void DeleteByEvaluateID(string EvaluateID)
		{
			this.dbGateway.DeleteByFieldValue("EvaluateID", EvaluateID);
		}
		public void UpdateByPK(Evaluate objEvaluate)
		{
			this.dbGateway.UpdateByFieldValue("EvaluateID", objEvaluate.EvaluateID, objEvaluate);
		}
		public IList<Evaluate> GetEvaluatesBy(string accountID, string objectID, int pageIndex, int pageSize, out int recordCount)
		{
			Conditions conditions = new Conditions();
			conditions.Connector = Connector.AND;
			if (!StringHelper.isNullOrEmpty(accountID))
			{
				conditions.ConditionExpressions.Add(new Condition("AccountID", Operator.EqualTo, accountID));
			}
			if (!StringHelper.isNullOrEmpty(objectID))
			{
				conditions.ConditionExpressions.Add(new Condition("ObjectID", Operator.EqualTo, objectID));
			}
			OrderExpression orderExpression = new OrderExpression("EvaluateTime", false);
			return this.dbGateway.getRecords(pageIndex, pageSize, conditions, orderExpression, null, Connector.AND, out recordCount);
		}
		public Evaluate GetEvaluateByID(string EvaluateID)
		{
			Conditions conditions = new Conditions();
			conditions.ConditionExpressions.Add(new Condition("EvaluateID", Operator.EqualTo, EvaluateID));
			return this.dbGateway.getRecord(conditions);
		}
		public IList<EvaluateLevel> GetSummaryByObjectID(string objectID)
		{
			string sQLStatement = string.Format("select Score,COUNT(*) as Count from T_EVALUATES where objectID='{0}' group by Score  order by score", objectID);
			DataTable arg_24_0 = this.dbGateway.getDataTableBySqlStatement(sQLStatement);
			IList<EvaluateLevel> list = new List<EvaluateLevel>();
			float num = 0f;
			foreach (DataRow dataRow in arg_24_0.Rows)
			{
				EvaluateLevel evaluateLevel = new EvaluateLevel();
				evaluateLevel.Score = (float)((int)dataRow["Score"]);
				evaluateLevel.Count = (int)dataRow["Count"];
				list.Add(evaluateLevel);
				num += (float)evaluateLevel.Count;
			}
			for (int i = 0; i <= list.Count - 1; i++)
			{
				float num2 = (float)list[i].Count / num;
				list[i].Percent = num2 * 100f;
			}
			return list;
		}
		public void DeleteEvaluateByObjectIDAndAccountID(string objectID, string accountID)
		{
			string sQLStatement = string.Format("delete from T_EVALUATES where ObjectID='{0}' and AccountID='{1}' ", objectID, accountID);
			this.dbGateway.ExecuteSQLStatement(sQLStatement);
		}
		public float GetAvgScoreByObjectID(string objectID)
		{
			string sQLStatement = string.Format("select isnull(AVG(Score),0) as AvgScore from T_EVALUATES where ObjectID='{0}'", objectID);
			return Convert.ToSingle(this.dbGateway.getDataTableBySqlStatement(sQLStatement).Rows[0]["AvgScore"]);
		}
	}
}
