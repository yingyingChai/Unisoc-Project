using KaYi.Database;
using KaYi.Web.Infrastructure.Model.Login;
using System;
namespace KaYi.Web.Infrastructure.DAL.Login
{
	public class PasswordResetRequestGateway
	{
		private DALGateway<PasswordResetRequest> dbGateway = new DALGateway<PasswordResetRequest>();
		public PasswordResetRequestGateway()
		{
			this.dbGateway.LoadSchema("T_PASSWORD_RESET_REQUESTS");
		}
		public void AddNew(PasswordResetRequest request)
		{
			this.dbGateway.AddNew(request);
		}
		public void Update(PasswordResetRequest request)
		{
			this.dbGateway.UpdateByFieldValue("RequestID", request.RequestID, request);
		}
		public void Delete(string requestID)
		{
			this.dbGateway.DeleteByFieldValue("RequestID", requestID);
		}
		public PasswordResetRequest GetRequestByID(string requestID)
		{
			Conditions conditions = new Conditions();
			conditions.ConditionExpressions.Add(new Condition("RequestID", Operator.EqualTo, requestID));
			return this.dbGateway.getRecord(conditions);
		}
	}
}
