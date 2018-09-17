using KaYi.Database;
using KaYi.Utilities;
using KaYi.Web.Infrastructure.Model.System.DictItems;
using System;
using System.Collections.Generic;
namespace KaYi.Web.Infrastructure.DAL.System.DictItems
{
	public class DictItemGateway
	{
		private DALGateway<DictItem> dbGateway = new DALGateway<DictItem>();
		public DictItemGateway()
		{
			this.dbGateway.LoadSchema("T_DICT_ITEMS");
		}
		public void AddNew(DictItem newDictItem)
		{
			this.dbGateway.AddNew(newDictItem);
		}
		public void DeleteByDictItemID(string DictItemID)
		{
			this.dbGateway.DeleteByFieldValue("ItemID", DictItemID);
		}
		public void UpdateByPK(DictItem objDictItem)
		{
			this.dbGateway.UpdateByFieldValue("ItemID", objDictItem.ItemID, objDictItem);
		}
		public IList<DictItem> GetDictItemsBy(string parentItemID, string orderBy, bool desc, int pageIndex, int pageSize, out int recordCount)
		{
			Conditions conditions = new Conditions();
			if (!StringHelper.isNullOrEmpty(parentItemID))
			{
				conditions.ConditionExpressions.Add(new Condition("ParentItemID", Operator.EqualTo, parentItemID));
			}
			OrderExpression orderExpression = null;
			if (!StringHelper.isNullOrEmpty(orderBy))
			{
				orderExpression = new OrderExpression(orderBy, desc);
			}
			return this.dbGateway.getRecords(pageIndex, pageSize, conditions, orderExpression, null, Connector.AND, out recordCount);
		}
		public DictItem GetDictItemByID(string DictItemID)
		{
			Conditions conditions = new Conditions();
			conditions.ConditionExpressions.Add(new Condition("ItemID", Operator.EqualTo, DictItemID));
			return this.dbGateway.getRecord(conditions);
		}
	}
}
