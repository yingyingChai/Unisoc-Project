using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace SPRD.LHD.MessageService
{
    public class Log
    {
        private Log()
        { }

        static Log _log;
        public static Log Instance
        {
            get
            {
                if (_log == null)
                {
                    _log = new Log();
                }
                return _log;
            }
        }

        public void Info(string Message)
        {
            EventLog.WriteEntry("CVMS Triger Mail Service", Message, EventLogEntryType.Information);
        }

        public void Error(Exception exp)
        {
            EventLog.WriteEntry("CVMS Triger Mail Service", exp.Message + exp.Source, EventLogEntryType.Error);
        }
    }
}
