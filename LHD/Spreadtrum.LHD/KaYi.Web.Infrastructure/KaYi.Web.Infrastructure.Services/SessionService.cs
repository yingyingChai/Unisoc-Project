using KaYi.Utilities;
using KaYi.Web.Infrastructure.DAL.System.Log;
using KaYi.Web.Infrastructure.DAL.System.Session;
using KaYi.Web.Infrastructure.DAL.System.Tokens;
using KaYi.Web.Infrastructure.Model.System.Session;
using KaYi.Web.Infrastructure.Model.System.Tokens;
using KaYi.Web.Infrastructure.Model.System.WebSiteProtect;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web;
namespace KaYi.Web.Infrastructure.Services
{
	public static class SessionService
	{
		private static SessionGateway sessionGateway = new SessionGateway();
		private static SysLogGateway logGateway = new SysLogGateway();
		private static SessionViewGateway sessionViewGateway = new SessionViewGateway();
		private static TokenGateway tokenGateway = new TokenGateway();
		public static int GetAccessCountInMinus(string ip, int mins)
		{
			return SessionService.logGateway.GetAccessCountIn(ip, mins);
		}
		public static WebSession CreateSession(HttpRequest request)
		{
			int visitTimes = 1;
			string clientID = string.Empty;
			string empty = string.Empty;
			string source;
			string keyword;
			SessionService.TryToFindSearchEnginSourceAndKeyword(request, out source, out keyword);
			if (request.Cookies["VisitTime"] != null)
			{
				visitTimes = Convert.ToInt32(request.Cookies["VisitTime"].Value) + 1;
			}
			if (request.Cookies["ClientID"] != null && request.Cookies["ClientID"].Value != "")
			{
				clientID = request.Cookies["ClientID"].Value;
			}
			else
			{
				clientID = Guid.NewGuid().ToString();
			}
			WebSession webSession = new WebSession();
			StringHelper.isNullOrEmpty(empty);
			webSession.AccountID = empty;
			webSession.Browser = request.ServerVariables["HTTP_USER_AGENT"];
			webSession.ClientID = clientID;
			webSession.ClientIP = WebClientOperator.GetClientIP(request);
			webSession.CreateTime = DateTime.Now;
			webSession.EndTime = webSession.CreateTime;
			webSession.HostName = request.ServerVariables["HTTP_HOST"];
			webSession.LastRequestTime = webSession.CreateTime;
			webSession.SessionID = Guid.NewGuid().ToString();
			webSession.StartTime = webSession.CreateTime;
			webSession.UpdateStamp = Guid.NewGuid().ToString();
			webSession.UpdateTime = webSession.CreateTime;
			webSession.VisitTimes = visitTimes;
			webSession.Source = source;
			webSession.Keyword = keyword;
			webSession.StoragePath = ConfigurationManager.AppSettings["StoragePath"];
			webSession.StorageRelativePath = ConfigurationManager.AppSettings["StorageRelativePath"];
			BlackList blackList = SiteProtectService.TryToFindIPInBlackList(webSession.ClientIP);
			if (blackList == null)
			{
				webSession.BlockType = BlockType.NoBlock;
			}
			else
			{
				webSession.BlockType = (BlockType)blackList.BlockType;
			}
			SessionService.sessionGateway.AddNew(webSession);
			return webSession;
		}
		public static void UpdateSession(WebSession session)
		{
			SessionService.sessionGateway.UpdateByPK(session);
		}
		public static IList<SessionView> GetSessionViewsByAccountID(string accountID, int pageIndex, int pageSize, out int recordCount)
		{
			return SessionService.sessionViewGateway.GetSessionViewsBy(accountID, pageIndex, pageSize, out recordCount);
		}
		public static void TryToFindSearchEnginSourceAndKeyword(HttpRequest request, out string searchEngine, out string keyword)
		{
			searchEngine = "";
			keyword = "";
			if (request.UrlReferrer != null)
			{
				string text = request.UrlReferrer.ToString();
				SearchEngineHelper searchEngineHelper = new SearchEngineHelper();
				if (searchEngineHelper.IsSearchEnginesGet(text))
				{
					keyword = searchEngineHelper.SearchKey(text);
					searchEngine = searchEngineHelper.EngineName;
				}
			}
		}
		public static void StopSession(string sessionID)
		{
			WebSession sessionsBySessionID = SessionService.sessionGateway.GetSessionsBySessionID(sessionID);
			if (sessionsBySessionID != null)
			{
				sessionsBySessionID.EndTime = DateTime.Now;
				SessionService.sessionGateway.UpdateByPK(sessionsBySessionID);
			}
		}
		public static void SessionLogin(string sessionID, string accountID)
		{
			WebSession sessionsBySessionID = SessionService.sessionGateway.GetSessionsBySessionID(sessionID);
			sessionsBySessionID.AccountID = accountID;
			SessionService.sessionGateway.UpdateByPK(sessionsBySessionID);
		}
		public static void Logout()
		{
		}
		public static void Request(string sessionID)
		{
		}
		public static void AddToken(Token token)
		{
			SessionService.tokenGateway.AddNew(token);
		}
		public static bool TryToUseToken(string tokenID, string usage, string objectID, string requestIP)
		{
			Token tokenByID = SessionService.tokenGateway.GetTokenByID(tokenID);
			bool result = false;
			if (tokenByID != null && tokenByID.ObjectID.Equals(objectID) && tokenByID.ExpireTime >= DateTime.Now && !tokenByID.Used && tokenByID.Usage == usage && (tokenByID.AllowIPs.IndexOf(requestIP) >= 0 || tokenByID.AllowIPs.Equals("*")))
			{
				tokenByID.UsedTime = DateTime.Now;
				tokenByID.Used = true;
				SessionService.tokenGateway.UpdateByPK(tokenByID);
				result = true;
			}
			return result;
		}
	}
}
