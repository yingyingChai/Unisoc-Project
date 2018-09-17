using KaYi.Web.Infrastructure.DAL.System.Urls;
using KaYi.Web.Infrastructure.Model.System.Urls;
using System;
namespace KaYi.Web.Infrastructure.Services
{
	public static class UrlService
	{
		private static UrlGateway urlGateway = new UrlGateway();
		public static Url GetUrlByUrlPath(string urlPath)
		{
			return UrlService.urlGateway.GetUrlsBy(urlPath);
		}
	}
}
