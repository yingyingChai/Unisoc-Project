using System;
namespace KaYi.Web.Infrastructure.Model.System.VisitStatistics
{
	public class VisitorCounterByDate
	{
		private DateTime startDate;
		private int visitorCount;
		private string hostName = string.Empty;
		public string HostName
		{
			get
			{
				return this.hostName;
			}
			set
			{
				this.hostName = value;
			}
		}
		public DateTime StartDate
		{
			get
			{
				return this.startDate;
			}
			set
			{
				this.startDate = value;
			}
		}
		public int VisitorCount
		{
			get
			{
				return this.visitorCount;
			}
			set
			{
				this.visitorCount = value;
			}
		}
	}
}
