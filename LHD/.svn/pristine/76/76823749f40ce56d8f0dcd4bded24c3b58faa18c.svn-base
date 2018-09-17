using KaYi.Utilities;
using KaYi.Web.Infrastructure.Model.System.DictItems;
using KaYi.Web.Infrastructure.Model.System.ProvinceAndCity;
using System;
using System.Collections.Generic;
namespace KaYi.Web.Infrastructure.Services
{
	public static class GeoService
	{
		public static IList<Province> GetAllProvince()
		{
			IList<Province> list = new List<Province>();
			foreach (DictItem current in DictItemService.GetDictItemsBy("DISTINCT_0", "Sequence", false))
			{
				list.Add(new Province
				{
					ProvinceID = current.ItemID,
					ProvinceName = current.Title,
					Sequence = current.Sequence
				});
			}
			return list;
		}
		public static IList<City> GetCitiesByProvince(string provinceID)
		{
			IList<City> list = new List<City>();
			foreach (DictItem current in DictItemService.GetDictItemsBy(provinceID, "Sequence", false))
			{
				list.Add(new City
				{
					CityID = current.ItemID,
					CityName = current.Title,
					Sequence = current.Sequence,
					ProvinceID = provinceID
				});
			}
			return list;
		}
		public static City GetCityByID(string cityID)
		{
			DictItem dictItemByID = DictItemService.GetDictItemByID(cityID);
			if (dictItemByID != null)
			{
				return new City
				{
					CityID = dictItemByID.ItemID,
					CityName = dictItemByID.Title,
					ProvinceID = dictItemByID.ParentItemID,
					Sequence = dictItemByID.Sequence
				};
			}
			return null;
		}
		public static Province GetProvinceByID(string provinceID)
		{
			DictItem dictItemByID = DictItemService.GetDictItemByID(provinceID);
			if (dictItemByID != null)
			{
				return new Province
				{
					ProvinceID = dictItemByID.ItemID,
					ProvinceName = dictItemByID.Title,
					Sequence = dictItemByID.Sequence
				};
			}
			return null;
		}
		public static string GetAddress(IList<string> ids)
		{
			if (ids == null || ids.Count < 1)
			{
				return "";
			}
			string text = string.Empty;
			foreach (string current in ids)
			{
				if (!StringHelper.isNullOrEmpty(current))
				{
					DictItem dictItemByID = DictItemService.GetDictItemByID(current);
					if (dictItemByID != null)
					{
						text = text + dictItemByID.Title + " ";
					}
				}
			}
			return text;
		}
		public static string GetAddressByIds(string provinceID, string cityID, string distinctId, string streetID)
		{
			return GeoService.GetAddress(new List<string>
			{
				provinceID,
				cityID,
				distinctId,
				streetID
			});
		}
	}
}
