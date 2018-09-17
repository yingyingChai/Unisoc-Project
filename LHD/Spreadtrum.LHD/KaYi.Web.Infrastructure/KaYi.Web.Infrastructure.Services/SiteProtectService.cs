using KaYi.Utilities;
using KaYi.Web.Infrastructure.DAL.System.WebSiteProtect;
using KaYi.Web.Infrastructure.Model.System.WebSiteProtect;
using System;
namespace KaYi.Web.Infrastructure.Services
{
	public static class SiteProtectService
	{
		private static BlackListGateway blackListGateway = new BlackListGateway();
		public static void AddBlackList(string ip, string reason, string _operator, BlockType blockType)
		{
			BlackList blackList = new BlackList();
			blackList.BlackID = Guid.NewGuid().ToString();
			blackList.BlockTime = DateTime.Now;
			blackList.BlockType = (int)blockType;
			blackList.CreateTime = DateTime.Now;
			blackList.EndIPInNumber = IP.IP2Long(ip);
			blackList.Operator = _operator;
			blackList.Reason = reason;
			blackList.StartIPInNumber = blackList.EndIPInNumber;
			blackList.UpdateStamp = Guid.NewGuid().ToString();
			blackList.UpdateTime = DateTime.Now;
			SiteProtectService.blackListGateway.AddNew(blackList);
		}
		public static void AddBlackList(string startIP, string endIP, string reason, string _operator, BlockType blockType)
		{
			BlackList blackList = new BlackList();
			blackList.BlackID = Guid.NewGuid().ToString();
			blackList.BlockTime = DateTime.Now;
			blackList.BlockType = (int)blockType;
			blackList.CreateTime = DateTime.Now;
			blackList.EndIPInNumber = IP.IP2Long(endIP);
			blackList.Operator = _operator;
			blackList.Reason = reason;
			blackList.StartIPInNumber = IP.IP2Long(startIP);
			blackList.UpdateStamp = Guid.NewGuid().ToString();
			blackList.UpdateTime = DateTime.Now;
			SiteProtectService.blackListGateway.AddNew(blackList);
		}
		public static BlackList TryToFindIPInBlackList(string ip)
		{
			return SiteProtectService.blackListGateway.TryToFindIPInBlackList(ip);
		}
	}
}
