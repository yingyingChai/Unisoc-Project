using System;
using System.Collections.Generic;
using System.Text;
using XJP.BPM.Platform.Log;
using XJP.BPM.Platform.XJPException;

namespace XJP.BPM.AMSService
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
            LogManagement.GetInstance().Log("BPM Schedule JOB", null, null, Message, XJP.BPM.Platform.Utility.Enums.LogLevel.Low, "BPM Schedule JOB");
        }

        public void Error(Exception exp)
        {
            ExceptionManagement.Instance.LogException("BPM Schedule JOB", null, null, exp);
        }
    }
}
