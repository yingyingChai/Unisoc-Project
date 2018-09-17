using KaYi.Services.EventServices;
using Spreadtrum.LHD.Business;
using Spreadtrum.LHD.Entity.Systems;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SPRD.LHD.SocketClientsService
{
    public partial class SocketClientsService : ServiceBase
    {
        IList<SilicondashOSATConfiguration> osatConfigurations;
        private string logDirectory = ConfigurationManager.AppSettings["LogDirectory"];
        private IList<SilicondashClient> silicondashClients = new List<SilicondashClient>();
        public SocketClientsService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            osatConfigurations = SilicondashService.GetSilicondashOSATConfigurations();
            InitializeService();
        }

        protected override void OnStop()
        {
            GC.Collect();
            base.OnStop();
        }
        private void InitializeService()
        {
            
            if (ConfigurationManager.AppSettings["ConnnectToSD"] == "1")
            {
                foreach (SilicondashOSATConfiguration configuration in osatConfigurations)
                {
                    //if (configuration.Type == "FT")
                    //{
                    //    continue;
                    //}
                    SilicondashClient item = new SilicondashClient(configuration);
                    item.MessageArrived += new SilicondashClient.MessageEvent(this.ProcessSilicondashLogs);
                    this.silicondashClients.Add(item);
                    item.InitializeService();
                }
            }

        }

        private void ProcessSilicondashLogs(string osatID, string message)
        {
            SilicondashOSATConfiguration configuration = this.FindOSATConfigurations(osatID);
            //ListBox box = (ListBox)this.tabControl.TabPages["tab" + osatID].Controls["lst" + osatID];
            string item = string.Format("{0}: {1}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), message);
            //if (box.Items.Count > 0x63)
            //{
            //    box.Items.Clear();
            //}
            //box.Items.Add(item);
            string fileName = string.Format(@"{0}\{1}_{2}.log", this.logDirectory, osatID, DateTime.Now.ToString("yyyyMMdd"));
            bool flag = false;
            while (!flag)
            {
                try
                {
                    EventService.AppendToLogFileToAbsFile(fileName, item + "\r\n");
                    Log.Instance.Info(osatID + "\r\n" + item + "\r\n");
                    flag = true;
                }
                catch (Exception)
                {
                }
            }
        }
        private SilicondashOSATConfiguration FindOSATConfigurations(string osatID)
        {
            for (int i = 0; i <= (this.osatConfigurations.Count - 1); i++)
            {
                if (this.osatConfigurations[i].OSATID == osatID)
                {
                    return this.osatConfigurations[i];
                }
            }
            return null;
        }
    }
    public class ThreadRun
    {
        private int sleeps;
        public ThreadRun(int s)
        {
            sleeps = s;
        }
        public void StartListenning()
        {
            
            while (true)
            {
                Log.Instance.Info(DateTime.Now.ToString("yyyy/MM/dd HH:mm"));
                Thread.Sleep(sleeps);
            }    
        }
    }


}
