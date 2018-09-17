using KaYi.Web.Infrastructure.DAL.System.VisitStatistics;
using KaYi.Web.Infrastructure.Model.System.VisitStatistics;
using System;
using System.Collections.Generic;
namespace KaYi.Web.Infrastructure.Services
{
	public static class VisitStatisticsService
	{
		private static VisitCounterByDateGateway visitorCounterByDateGateway = new VisitCounterByDateGateway();
		private static VisitorCounterByDate findCounterInList(IList<VisitorCounterByDate> records, DateTime date)
		{
			VisitorCounterByDate result = null;
			foreach (VisitorCounterByDate current in records)
			{
				if (current.StartDate == date)
				{
					result = current;
					break;
				}
			}
			return result;
		}
		public static IList<VisitorCounterByDate> GetVisitorCounterByDate(string hostName, DateTime startDate, DateTime endDate, int pageIndex, int pageSize, out int recordCount)
		{
			IList<VisitorCounterByDate> visitorCounterByDate = VisitStatisticsService.visitorCounterByDateGateway.GetVisitorCounterByDate(hostName, startDate, endDate, pageIndex, pageSize, out recordCount);
			IList<VisitorCounterByDate> list = new List<VisitorCounterByDate>();
			DateTime dateTime = startDate;
			while (dateTime <= endDate)
			{
				VisitorCounterByDate visitorCounterByDate2 = VisitStatisticsService.findCounterInList(visitorCounterByDate, dateTime);
				if (visitorCounterByDate2 == null)
				{
					visitorCounterByDate2 = new VisitorCounterByDate();
					visitorCounterByDate2.StartDate = dateTime;
					visitorCounterByDate2.VisitorCount = 0;
					visitorCounterByDate2.HostName = hostName;
				}
				list.Add(visitorCounterByDate2);
				dateTime = dateTime.AddDays(1.0);
			}
			return list;
		}
	}
}
