using KaYi.Database;
using KaYi.Web.Infrastructure.Model.System.Urls;
using System;
namespace KaYi.Web.Infrastructure.DAL.System.Urls
{
	public class UrlGateway
	{
		private DALGateway<Url> dbGateway = new DALGateway<Url>();
		public UrlGateway()
		{
			this.dbGateway.LoadSchema("URLS");
		}
		public void AddNew(Url newUrl)
		{
			this.dbGateway.AddNew(newUrl);
		}
		public void DeleteByUrlID(string UrlID)
		{
			this.dbGateway.DeleteByFieldValue("UrlID", UrlID);
		}
		public void UpdateByPK(Url objUrl)
		{
			this.dbGateway.UpdateByFieldValue("UrlID", objUrl.UrlID, objUrl);
		}
		public Url GetUrlsBy(string UrlPath)
		{
			Conditions conditions = new Conditions();
			conditions.ConditionExpressions.Add(new Condition("UrlPath", Operator.EqualTo, UrlPath));
			return this.dbGateway.getRecord(conditions);
		}
		public Url GetUrlByID(string UrlID)
		{
			Conditions conditions = new Conditions();
			conditions.ConditionExpressions.Add(new Condition("UrlID", Operator.EqualTo, UrlID));
			return this.dbGateway.getRecord(conditions);
		}
	}
}
