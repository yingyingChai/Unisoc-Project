using KaYi.Database;
using KaYi.Web.Infrastructure.Model.System.Tokens;
using System;
namespace KaYi.Web.Infrastructure.DAL.System.Tokens
{
	public class TokenGateway
	{
		private DALGateway<Token> dbGateway = new DALGateway<Token>();
		public TokenGateway()
		{
			this.dbGateway.LoadSchema("T_TOKENS");
		}
		public void AddNew(Token newToken)
		{
			this.dbGateway.AddNew(newToken);
		}
		public void DeleteByTokenID(string TokenID)
		{
			this.dbGateway.DeleteByFieldValue("TokenID", TokenID);
		}
		public void UpdateByPK(Token objToken)
		{
			this.dbGateway.UpdateByFieldValue("TokenID", objToken.TokenID, objToken);
		}
		public Token GetTokenByID(string TokenID)
		{
			Conditions conditions = new Conditions();
			conditions.ConditionExpressions.Add(new Condition("TokenID", Operator.EqualTo, TokenID));
			return this.dbGateway.getRecord(conditions);
		}
	}
}
