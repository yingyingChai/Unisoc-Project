namespace SPRD.LHD.SocketClientsService
{
    using SilicondashSimulator.Base.StompClients;
    using Spreadtrum.LHD.Entity.Systems;
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Threading;

    public class SilicondashClient
    {
        private StompClient client = null;
        private SilicondashOSATConfiguration configuration = null;

        [field: CompilerGenerated, DebuggerBrowsable(0)]
        public event MessageEvent MessageArrived;

        public SilicondashClient(SilicondashOSATConfiguration _configuration)
        {
            this.configuration = _configuration;
        }

        public void InitializeService()
        {
            this.client = new StompClient(this.configuration);
            this.client.throwMessage += new StompClient.MessageEvent(this.throwMessage);
            try
            {
                this.throwMessage("Connecting to Silicondash...");
                bool flag = this.client.Connect();
                if (!flag)
                {
                    this.throwMessage("Connect to Silicondash failed.");
                }
                else
                {
                    Thread.Sleep(0x3e8);
                    flag = this.client.Subscribe();
                }
                if (!flag)
                {
                    this.throwMessage("Subscribe failed.");
                }
                else
                {
                    Thread.Sleep(0x3e8);
                    this.client.StartService();
                    this.throwMessage("Start listening...");
                }
            }
            catch (Exception exception)
            {
                this.throwMessage(string.Format("Connnecting to {0} Silicondash failed, {1}", this.configuration.OSATID, exception.Message));
            }
        }

        public void StopService()
        {
            this.client.userClose = true;
            this.client.StopService();
        }

        private void throwMessage(string message)
        {
            this.MessageArrived(this.configuration.OSATID, message);
        }

        public delegate void MessageEvent(string osatID, string message);
    }
}

