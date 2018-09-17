using KaYi.Web.Infrastructure.DAL.Evaluator;
using KaYi.Web.Infrastructure.Model.Evaluator;
using KaYi.Web.Infrastructure.Model.System.Session;
using System;
using System.Collections.Generic;
namespace KaYi.Web.Infrastructure.Services
{
	public static class EvaluateService
	{
		private static EvaluateGateway evaluateGateway = new EvaluateGateway();
		public static void Evaluate(WebSession session, string accountID, string objectID, int score)
		{
			Evaluate evaluate = new Evaluate();
			evaluate.AccountID = accountID;
			evaluate.ClientID = session.ClientID;
			evaluate.EvaluateID = Guid.NewGuid().ToString();
			evaluate.EvaluateTime = DateTime.Now;
			evaluate.ObjectID = objectID;
			evaluate.Score = score;
			evaluate.SessionID = session.SessionID;
			EvaluateService.evaluateGateway.AddNew(evaluate);
		}
		public static void UnEvaluate(string objectID, string accountID)
		{
			EvaluateService.evaluateGateway.DeleteEvaluateByObjectIDAndAccountID(objectID, accountID);
		}
		public static bool DoesThisGuyEvaluatedThisObject(string accountID, string objectID)
		{
			int num = 0;
			EvaluateService.evaluateGateway.GetEvaluatesBy(accountID, objectID, 0, 1, out num);
			return num > 0;
		}
		public static EvaluateLevel GetEvaluateLevelByObjectIDAndScore(string objectID, int score)
		{
			EvaluateLevel evaluateLevel = new EvaluateLevel();
			foreach (EvaluateLevel current in EvaluateService.evaluateGateway.GetSummaryByObjectID(objectID))
			{
				if (current.Score == (float)score)
				{
					evaluateLevel = current;
				}
			}
			evaluateLevel.Score = (float)score;
			return evaluateLevel;
		}
		public static IList<Evaluate> GetEvaluatesByObjectID(string objectID)
		{
			int num = 0;
			return EvaluateService.evaluateGateway.GetEvaluatesBy("", objectID, 0, 99999, out num);
		}
		public static Evaluate GetEvaluateByAccountIDAndObjectID(string accountID, string objectID)
		{
			int num = 0;
			Evaluate result = null;
			IList<Evaluate> evaluatesBy = EvaluateService.evaluateGateway.GetEvaluatesBy(accountID, objectID, 0, 1, out num);
			if (evaluatesBy != null && evaluatesBy.Count > 0)
			{
				result = evaluatesBy[0];
			}
			return result;
		}
		public static int GetEvaluatesCountByObjectID(string objectID)
		{
			int result = 0;
			EvaluateService.evaluateGateway.GetEvaluatesBy("", objectID, 0, 1, out result);
			return result;
		}
		public static float GetAvgScoreByObjectID(string objectID)
		{
			return EvaluateService.evaluateGateway.GetAvgScoreByObjectID(objectID);
		}
		public static IList<EvaluateLevel> GetSummaryByObjectID(string objectID)
		{
			return EvaluateService.evaluateGateway.GetSummaryByObjectID(objectID);
		}
	}
}
