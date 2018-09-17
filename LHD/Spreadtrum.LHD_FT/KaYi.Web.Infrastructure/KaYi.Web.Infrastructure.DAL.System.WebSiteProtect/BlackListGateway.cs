using KaYi.Database;
using KaYi.Utilities;
using KaYi.Web.Infrastructure.Model.System.WebSiteProtect;
using System;
namespace KaYi.Web.Infrastructure.DAL.System.WebSiteProtect
{
	public class BlackListGateway
	{
		private DALGateway<BlackList> dbGateway = new DALGateway<BlackList>();
		public BlackListGateway()
		{
			this.dbGateway.LoadSchema("T_BLACK_LIST");
		}
		public void AddNew(BlackList newBlackList)
		{
			newBlackList.UpdateStamp = Guid.NewGuid().ToString();
			newBlackList.CreateTime = (newBlackList.UpdateTime = DateTime.Now);
			this.dbGateway.AddNew(newBlackList);
		}
		public void DeleteByBlackListID(string blacklistID)
		{
			this.dbGateway.DeleteByFieldValue("BlackID", blacklistID);
		}
		public void UpdateByPK(BlackList objBlackList)
		{
			objBlackList.UpdateStamp = Guid.NewGuid().ToString();
			this.dbGateway.UpdateByFieldValue("BlackID", objBlackList.BlackID, objBlackList);
		}
		public BlackList GetBlackListsByBlackListID(string blacklistID)
		{
			Conditions conditions = new Conditions();
			conditions.ConditionExpressions.Add(new Condition("BlackID", Operator.EqualTo, blacklistID));
			return this.dbGateway.getRecord(conditions);
		}
		public BlackList TryToFindIPInBlackList(string ip)
		{
			long ip2 = IP.IP2Long(ip);
			return this.TryToFindIPINBlackListByNum(ip2);
		}
		private BlackList TryToFindIPINBlackListByNum(long ip)
		{
			Conditions conditions = new Conditions();
			conditions.Connector = Connector.AND;
			conditions.ConditionExpressions.Add(new Condition("StartIPInNumber", Operator.SmallerOrEqualTo, ip));
			conditions.ConditionExpressions.Add(new Condition("EndIPInNumber", Operator.GreaterOrEqualTo, ip));
			return this.dbGateway.getRecord(conditions);
		}
	}
}
