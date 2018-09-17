using KaYi.Web.Infrastructure.DAL.System.DictItems;
using KaYi.Web.Infrastructure.Model.System.DictItems;
using System;
using System.Collections.Generic;
namespace KaYi.Web.Infrastructure.Services
{
	public static class DictItemService
	{
		private static DictItemGateway itemGateway = new DictItemGateway();
		public static IList<DictItem> GetDictItemsByParentID(string parentItemID, string orderBy, bool desc)
		{
			int num = 0;
			return DictItemService.GetExtendPropertiesOfItems(DictItemService.itemGateway.GetDictItemsBy(parentItemID, orderBy, desc, 0, 9999, out num));
		}
		public static IList<DictItem> GetDictItemsBy(string parentItemID, string orderBy, bool desc)
		{
			int num = 0;
			return DictItemService.GetExtendPropertiesOfItems(DictItemService.itemGateway.GetDictItemsBy(parentItemID, orderBy, desc, 0, 9999, out num));
		}
		public static DictItem GetDictItemByID(string itemID)
		{
			return DictItemService.GetExtendPropertiesOfItem(DictItemService.itemGateway.GetDictItemByID(itemID));
		}
		private static DictItem GetExtendPropertiesOfItem(DictItem item)
		{
			if (item == null)
			{
				return new DictItem();
			}
			int subItemCount = 0;
			DictItemService.itemGateway.GetDictItemsBy(item.ItemID, "", false, 0, 0, out subItemCount);
			item.SubItemCount = subItemCount;
			return item;
		}
		private static IList<DictItem> GetExtendPropertiesOfItems(IList<DictItem> items)
		{
			for (int i = 0; i <= items.Count - 1; i++)
			{
				items[i] = DictItemService.GetExtendPropertiesOfItem(items[i]);
			}
			return items;
		}
	}
}
