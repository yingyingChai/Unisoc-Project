using KaYi.Database;
using KaYi.Web.Infrastructure.Model.System.IPLocation;
using System;
using System.Collections.Generic;
using System.Data;
namespace KaYi.Web.Infrastructure.DAL.System.IPLocation
{
	public class IpLocationGateway
	{
		private DALGateway<IpLocation> dbGateway = new DALGateway<IpLocation>();
		public IpLocationGateway()
		{
			this.dbGateway.LoadSchema("T_IP_LOCATIONS");
		}
		public void AddNew(IpLocation newIpLocation)
		{
			Conditions conditions = new Conditions();
			conditions.ConditionExpressions.Add(new Condition("StartAt", Operator.EqualTo, newIpLocation.StartAt));
			conditions.ConditionExpressions.Add(new Condition("EndAt", Operator.EqualTo, newIpLocation.EndAt));
			conditions.Connector = Connector.AND;
			if (this.dbGateway.getRecord(conditions) == null)
			{
				newIpLocation.UpdateTime = (newIpLocation.CreateTime = DateTime.Now);
				newIpLocation.UpdateStamp = Guid.NewGuid().ToString();
				this.dbGateway.AddNew(newIpLocation);
			}
		}
		public void DeleteByIpLocationID(string iplocationID)
		{
			this.dbGateway.DeleteByFieldValue("LocationID", iplocationID);
		}
		public void UpdateByPK(IpLocation objIpLocation)
		{
			objIpLocation.UpdateTime = DateTime.Now;
			objIpLocation.UpdateStamp = Guid.NewGuid().ToString();
			this.dbGateway.UpdateByFieldValue("LocationID", objIpLocation.LocationID, objIpLocation);
		}
		public IpLocation GetIpLocationsByIpLocationID(string iplocationID)
		{
			Conditions conditions = new Conditions();
			conditions.ConditionExpressions.Add(new Condition("LocationID", Operator.EqualTo, iplocationID));
			return this.dbGateway.getRecord(conditions);
		}
		public IpLocation GetLocationByIP(long ip)
		{
			IpLocation result = null;
			Conditions conditions = new Conditions();
			conditions.Connector = Connector.AND;
			OrderExpression orderExpression = new OrderExpression("UpdateTime", true);
			IList<IpLocation> records = this.dbGateway.getRecords(0, 1, conditions, orderExpression, string.Format(" '{0}' between StartIPNum and EndIPNum ", ip));
			if (records != null && records.Count > 0)
			{
				result = records[0];
			}
			return result;
		}
		public IList<string> GetUnlocateIPs()
		{
			DataTable dataTableBySqlStatement = this.dbGateway.getDataTableBySqlStatement("select * from V_UNLOCATE_IPS");
			if (dataTableBySqlStatement == null)
			{
				return null;
			}
			IList<string> list = new List<string>();
			foreach (DataRow dataRow in dataTableBySqlStatement.Rows)
			{
				list.Add(dataRow["IP"].ToString());
			}
			return list;
		}
	}
}
