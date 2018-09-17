using KaYi.Database;
using KaYi.Services.Emails.Library.Model;
using System;
using System.Collections.Generic;
namespace KaYi.Services.Emails.Library.Gateway
{
	public class EmailGateway
	{
		private DALGateway<Email> dbGateway = new DALGateway<Email>();
		public EmailGateway()
		{
			this.dbGateway.LoadSchema("EMAILS");
		}
		public void AddNew(Email email)
		{
			this.dbGateway.AddNew(email);
		}
		public void Update(Email email)
		{
			this.dbGateway.UpdateByFieldValue("EmailID", email.EmailID, email);
		}
		public void Delete(string emailID)
		{
			this.dbGateway.DeleteByFieldValue("EmailID", emailID);
		}
		public Email GetEmailByID(string emailID)
		{
			Conditions conditions = new Conditions();
			conditions.ConditionExpressions.Add(new Condition("EmailID", Operator.EqualTo, emailID));
			return this.dbGateway.getRecord(conditions);
		}
		public IList<Email> GetEmailsBy(string sender, string recipient, EmailState state, int maxTryTime, bool immediatelyMailOnly, int pageIndex, int pageSize, out int recordCount)
		{
			Conditions conditions = new Conditions();
			if (sender != null && sender != "")
			{
				conditions.ConditionExpressions.Add(new Condition("Sender", Operator.Like, sender));
			}
			if (recipient != null && recipient != "")
			{
				conditions.ConditionExpressions.Add(new Condition("Recipients", Operator.Like, recipient));
			}
			conditions.ConditionExpressions.Add(new Condition("State", Operator.EqualTo, state));
			if (maxTryTime != -1)
			{
				conditions.ConditionExpressions.Add(new Condition("TryTime", Operator.SmallerOrEqualTo, maxTryTime));
			}
			if (immediatelyMailOnly)
			{
				conditions.ConditionExpressions.Add(new Condition("NextTryTime", Operator.SmallerOrEqualTo, DateTime.Now));
			}
			conditions.Connector = Connector.AND;
			OrderExpression orderExpression = new OrderExpression("Priority", false);
			return this.dbGateway.getRecords(pageIndex, pageSize, conditions, orderExpression, null, Connector.AND, out recordCount);
		}
	}
}
