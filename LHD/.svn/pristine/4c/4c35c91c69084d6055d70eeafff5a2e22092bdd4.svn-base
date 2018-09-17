namespace Spreadtrum.LHD.MessageCenter
{
    using KaYi.Utilities;
    using Newtonsoft.Json;
    using Spreadtrum.LHD.Business;
    using Spreadtrum.LHD.Entity.Systems;
    using Spreadtrum.LHD.MessageCenter.Base;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using WebSocketSharp;
    using WebSocketSharp.Server;

    public class NotificationServer
    {
        private Thread messageThread = new Thread(new ThreadStart(NotificationServer.MessageMonitor));
        private static WebSocketServer wssv = null;

        [field: CompilerGenerated, DebuggerBrowsable(0)]
        public event MessageEvent throwMessage;

        private static void MessageMonitor()
        {
            while (true)
            {
                IList<NotificationCounter> allNotificationCounters = NotificationService.GetAllNotificationCounters();
                foreach (WebSocketServiceHost host in wssv.WebSocketServices.Hosts)
                {
                    foreach (IWebSocketSession session in host.Sessions.Sessions)
                    {
                        foreach (NotificationCounter counter in allNotificationCounters)
                        {
                            string str = session.Context.QueryString["uid"];
                            if (!StringHelper.isNullOrEmpty(str) && (str == counter.UserID))
                            {
                                host.Sessions.SendToAsync(JsonConvert.SerializeObject(counter), session.ID, null);
                            }
                        }
                    }
                }
                Thread.Sleep(0xbb8);
            }
        }

        public void StartService()
        {
            int port = Convert.ToInt32(ConfigurationManager.AppSettings["NotificationServicePort"]);
            wssv = new WebSocketServer(port);
            string message = string.Format("Starting Notification Service on port {0}", port);
            this.throwMessage(message);
            wssv.Log.Level = WebSocketSharp.LogLevel.Trace;
            wssv.WaitTime = TimeSpan.FromSeconds(2.0);
            wssv.AddWebSocketService<MessageService>("/Notifications");
            wssv.KeepClean = true;
            wssv.Start();
            this.messageThread.Start();
        }

        public void StopService()
        {
            this.messageThread.Abort();
            wssv.Stop();
            string message = string.Format("Notification Service stopped.", new object[0]);
            this.throwMessage(message);
        }

        public delegate void MessageEvent(string message);
    }
}

