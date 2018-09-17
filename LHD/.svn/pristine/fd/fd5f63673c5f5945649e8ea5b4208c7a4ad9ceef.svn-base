using KaYi.Utilities;
using KaYi.Web.Infrastructure.DAL.System.IPLocation;
using KaYi.Web.Infrastructure.Model.System.IPLocation;
using System;
using System.Collections.Generic;
namespace KaYi.Web.Infrastructure.Services
{
	public static class IPLocateService
	{
		private static IpLocationGateway ipLocationGateway = new IpLocationGateway();
		public static IList<string> GetAllUnlocatedIPs()
		{
			return IPLocateService.ipLocationGateway.GetUnlocateIPs();
		}
		public static IpLocation GetLocationByIP(string ip)
		{
			long ip2 = IP.IP2Long(ip);
			return IPLocateService.ipLocationGateway.GetLocationByIP(ip2);
		}
		public static IpLocation GetLocationByIP(long ip)
		{
			return IPLocateService.ipLocationGateway.GetLocationByIP(ip);
		}
		public static void AddLocation(IpLocation location)
		{
			IPLocateService.ipLocationGateway.AddNew(location);
		}
		public static void UpdateLocation(IpLocation location)
		{
			IPLocateService.ipLocationGateway.UpdateByPK(location);
		}
		public static void DeleteLocation(string locationID)
		{
			IPLocateService.ipLocationGateway.DeleteByIpLocationID(locationID);
		}
	}
}
