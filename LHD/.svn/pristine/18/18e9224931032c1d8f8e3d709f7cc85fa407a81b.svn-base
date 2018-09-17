namespace SilicondashSimulator.Base.StompClients
{
    using KaYi.Services.EventServices;
    using Spreadtrum.LHD.Business;
    using Spreadtrum.LHD.Entity.Lots;
    using Spreadtrum.LHD.Entity.Systems;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using WebSocketSharp;

    public class StompClient
    {
        private SilicondashOSATConfiguration configuration = null;
        private bool connected = false;
        private Handler handler = null;
        public string logFilename = string.Empty;
        private Thread monitorService = null;
        private Thread silicondashService = null;
        private bool subScribeSuccessed = false;
        private DateTime subScribeTime;
        public bool userClose = false;
        private WebSocket ws = null;

        [field: CompilerGenerated, DebuggerBrowsable(0)]
        public event MessageEvent throwMessage;

        public StompClient(SilicondashOSATConfiguration _configuration)
        {
            this.configuration = _configuration;
            this.handler = new Handler(this.configuration.URI, this.configuration.LoginID, this.configuration.LoginPWD);
            this.silicondashService = new Thread(new ThreadStart(this.doSilicondashService));
            this.monitorService = new Thread(new ThreadStart(this.doMonitorService));
        }

        public bool Connect()
        {
            this.ws = new WebSocket(this.handler.URI, new string[0]);
            this.ws.logFileName = this.logFilename = string.Format(@"{0}\SILICONDASH_{1}_{2}.log", ConfigurationManager.AppSettings["LogDirectory"], this.configuration.OSATID, DateTime.Now.ToString("yyyyMMdd"));
            this.ws.Compression = CompressionMethod.Deflate;
            this.ws.OnOpen += new EventHandler(this.ws_OnOpen);
            this.ws.Log.Level = WebSocketSharp.LogLevel.Debug;
            this.ws.OnMessage += new EventHandler<MessageEventArgs>(this.ws_OnMessage);
            this.ws.OnError += new EventHandler<ErrorEventArgs>(this.ws_OnError);
            this.ws.OnClose += new EventHandler<CloseEventArgs>(this.ws_OnClose);
            this.ws.SetCredentials(this.handler.Username, this.handler.Password, true);
            try
            {
                this.ws.Connect();
                Thread.Sleep(500);
                string data = "CONNECT\naccept-version:1.1,1.2\nheart-beat:10000,10000\n\n\0";
                this.ws.Send(data);
                this.throwMessage(data);
                this.connected = true;
                if (this.monitorService.ThreadState == System.Threading.ThreadState.Unstarted)
                {
                    this.monitorService.Start();
                }
                return true;
            }
            catch (Exception exception)
            {
                this.throwMessage(exception.Message);
                return false;
            }
        }

        private void doMonitorService()
        {
            while (true)
            {
                if (!this.connected)
                {
                    this.reConnect();
                }
                if ((DateTime.Now > this.subScribeTime.AddMinutes(5.0)) && !this.subScribeSuccessed)
                {
                    this.reConnect();
                }
                Thread.Sleep(0x4e20);
            }
        }

        private void doSilicondashService()
        {
            while (this.connected)
            {
                IList<Lot> lotsBySDStates = LotService.GetLotsBySDStates(this.configuration.OSATID, 0xff);
                foreach (Lot lot in lotsBySDStates)
                {
                    try
                    {
                        string str = LotService.GetLotJudgementObject(lot).ToString();
                        string oldValue = "\"[\\\"";
                        str = str.Replace(oldValue, "[\"");
                        string str3 = "\\\"]\"";
                        str = str.Replace(str3, "\"]");
                        string data = string.Format("SEND\ndestination:{0}\nreceipt:1\ncontent-type:text/plain;charset=UTF-8;\n\n{1}\0", this.configuration.OutboundChannel, str, str.Length);
                        this.ws.Send(data);
                        lot.SDStates = 1;
                        LotService.UpdateLot(lot);
                        EventService.AppendToLogFileToAbsFile(string.Format(@"{0}\{1}_{2}", this.configuration.MessageOutDirectory, lot.LotNO, DateTime.Now.ToString("yyyyMMddHHmmss")), data);
                    }
                    catch (Exception exception)
                    {
                        EventService.AppendToLogFileToAbsFile(string.Format(@"{0}\TO_SD_{1}_{2}", this.configuration.MessageOutDirectory, lot.LotNO, DateTime.Now.ToString("yyyyMMddHHmmss")), exception.Message);
                    }
                }
                Thread.Sleep(0x7530);
            }
        }

        private void reConnect()
        {
            Thread.Sleep(0x4e20);
            this.throwMessage("Reconnecting...");
            this.connected = false;
            while (!this.connected)
            {
                this.connected = this.Connect();
                Thread.Sleep(0x4e20);
            }
            this.Subscribe();
        }

        public void StartMonitor()
        {
            this.monitorService.Start();
        }

        public void StartService()
        {
            this.silicondashService.Start();
        }

        public void StopService()
        {
            try
            {
                this.silicondashService.Abort();
                this.monitorService.Abort();
                this.ws.Send("DISCONNECT\n\n\0");
                this.ws.Close();
            }
            catch (Exception)
            {
            }
        }

        public bool Subscribe()
        {
            string str = "LHD_" + this.configuration.OSATID + Guid.NewGuid().ToString();
            string data = string.Format("SUBSCRIBE\ndestination:{1}\nid:{0}\nreceipt:{0}\n\n\0", str, this.configuration.InboundChannel);
            try
            {
                this.subScribeSuccessed = false;
                this.subScribeTime = DateTime.Now;
                this.ws.Send(data);
                this.throwMessage(string.Format("Subscribe finished.\n{0}", data));
                return true;
            }
            catch (Exception exception)
            {
                this.throwMessage(string.Format("Subscribe error, {0}", exception.Message));
                return false;
            }
        }

        private void ws_OnClose(object obj, CloseEventArgs args)
        {
            this.throwMessage(string.Format("WebSocket Connection closed, {0}", args.Reason));
            if (this.userClose)
            {
                this.silicondashService.Abort();
                this.monitorService.Abort();
            }
            this.connected = false;
        }

        private void ws_OnError(object obj, ErrorEventArgs args)
        {
            this.throwMessage(string.Format("Websocket Connection Error, {0}", args.Message));
            this.connected = false;
        }

        private void ws_OnMessage(object obj, MessageEventArgs args)
        {
            if (args.Data == "\n")
            {
                try
                {
                    this.ws.Send("\n");
                    this.throwMessage("PONG");
                }
                catch (Exception)
                {
                    this.connected = false;
                }
            }
            else
            {
                this.throwMessage(string.Format("Message from Server: {0}", args.Data));
                if (args.Data.IndexOf("RECEIPT") >= 0)
                {
                    this.subScribeSuccessed = true;
                    this.throwMessage("Subscribe successed.");
                }
                if (args.Data.IndexOf("LOT_JUDGEMENT") >= 0)
                {
                    int index = args.Data.IndexOf("{\"LOT_JUDGEMENT");
                    string jsonStr = args.Data.Substring(index, (args.Data.Length - index) - 1);
                    string fileName = string.Format(@"{0}\{1}_{2}.json", this.configuration.MessageInDirectory, DateTime.Now.ToString("yyyyMMddHHmmss"), Guid.NewGuid().ToString());
                    EventService.AppendToLogFileToAbsFile(fileName, args.Data);
                    IList<string> messages = new List<string>();
                    try
                    {
                        Lot lot = LotService.ReadLotFromJson(this.configuration.OSATID, jsonStr, messages);
                        foreach (string str3 in messages)
                        {
                            this.throwMessage(str3);
                        }
                        LotService.SaveLotAndInformQA_AND_PE(lot);
                    }
                    catch (Exception)
                    {
                        this.throwMessage(string.Format("LOT_JUDGEMENT process failed. filename is {0}", fileName));
                        foreach (string str4 in messages)
                        {
                            this.throwMessage(str4);
                        }
                    }
                }
            }
        }

        private void ws_OnOpen(object obj, EventArgs args)
        {
            this.throwMessage("Connect to handler via websocket successed.");
        }

        public delegate void MessageEvent(string message);
    }
}

