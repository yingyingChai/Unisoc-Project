using System;
namespace KaYi.Services.EventServices.Model
{
	public class EventLog
	{
		private string eventID = string.Empty;
		private DateTime time;
		private string eventCode = string.Empty;
		private string eventDescription = string.Empty;
		private string eventSource = string.Empty;
		private string eventType = string.Empty;
		public string EventSource
		{
			get
			{
				return this.eventSource;
			}
			set
			{
				this.eventSource = value;
			}
		}
		public string EventID
		{
			get
			{
				return this.eventID;
			}
			set
			{
				this.eventID = value;
			}
		}
		public DateTime Time
		{
			get
			{
				return this.time;
			}
			set
			{
				this.time = value;
			}
		}
		public string EventCode
		{
			get
			{
				return this.eventCode;
			}
			set
			{
				this.eventCode = value;
			}
		}
		public string EventDescription
		{
			get
			{
				return this.eventDescription;
			}
			set
			{
				this.eventDescription = value;
			}
		}
		public EventLog(string Source, string EventCode, string EventDescription)
		{
			this.eventSource = Source;
			this.eventCode = EventCode;
			this.eventDescription = EventDescription;
			this.eventID = Guid.NewGuid().ToString();
			this.time = DateTime.Now;
		}
	}
}
