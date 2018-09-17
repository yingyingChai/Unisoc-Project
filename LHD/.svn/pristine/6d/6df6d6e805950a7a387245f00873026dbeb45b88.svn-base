using System;
using System.Collections.Generic;
using System.Text;

namespace SPRD.LHD.SD.MailReceive
{
    public class Logger
    {
        private static log4net.ILog log = null;
        private static object lockHelper = new object();
        public static log4net.ILog Log
        {
            get
            {
                if (log == null)
                {
                    lock (lockHelper)
                    {
                        if (log == null)
                            log = log4net.LogManager.GetLogger("logger");
                    }
                }
                return log;
            }
        }
    }
}
