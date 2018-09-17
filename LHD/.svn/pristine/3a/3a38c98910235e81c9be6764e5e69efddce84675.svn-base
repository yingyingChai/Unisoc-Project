namespace Spreadtrum.LHD.MessageCenter
{
    using KaYi.Services.EventServices;
    using KaYi.Utilities;
    using Spreadtrum.LHD.Business;
    using Spreadtrum.LHD.Entity.Lots;
    using Spreadtrum.LHD.Entity.Systems;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Configuration;
    using System.Drawing;
    using System.IO;
    using System.Windows.Forms;

    public class frmMain : Form
    {
        private Button button1;
        private IContainer components = null;
        private Label lblPrompt;
        private string logDirectory = ConfigurationManager.AppSettings["LogDirectory"];
        private NotificationServer notificationServer = new NotificationServer();
        private IList<SilicondashOSATConfiguration> osatConfigurations = SilicondashService.GetSilicondashOSATConfigurations();
        private IList<SilicondashClient> silicondashClients = new List<SilicondashClient>();
        private TextBox txtOSAT;
        private TabControl tabControl = new TabControl();

        public frmMain()
        {
            this.InitializeComponent();
            this.InitializeTabPages();
            Control.CheckForIllegalCrossThreadCalls = false;
        }

        private void btnAlterLotJudegement_Click(object sender, EventArgs e)
        {
            LotService.GetLotJudgementObject(LotService.GetLotByLotID("f2927ee6-370b-4d1d-bf68-ad9ee2dcd0dc"));
        }

        private void btnClearSystem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure to clear ALL lots in LHD system?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                LotService.ClearSystem();
            }
        }

        private void btnLoadFromFile_Click(object sender, EventArgs e)
        {
            this.ImportFromFile("Amkor");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.ImportFromFile(txtOSAT.Text);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
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

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach (SilicondashClient client in this.silicondashClients)
            {
                client.StopService();
            }
            this.notificationServer.StopService();
        }

        private void frmMain_Shown(object sender, EventArgs e)
        {
            this.InitializeService();
        }

        private void ImportFromFile(string osatID)
        {
            FileInfo[] files = new DirectoryInfo(@"C:\LHD_APPLICATION\MessageCenter\Messages\" + osatID + @"\In\Reload\").GetFiles();
            int num = 0;
            for (int i = 0; i <= (files.Length - 1); i++)
            {
                IList<string> messages = new List<string>();
                num++;
                FileInfo info2 = files[i];
                string str2 = FileService.ReadTextFile(info2.FullName);
                int index = str2.IndexOf("{\"LOT_JUDGEMENT");
                string jsonStr = str2.Substring(index, (str2.Length - index) - 1);
                Lot lot = LotService.ReadLotFromJson(osatID, jsonStr, messages);
                this.WriteToLog(osatID, messages);
                if (lot != null)
                {
                    LotService.SaveLotAndInformQA_AND_PE(lot);
                    this.lblPrompt.Text = string.Format("{0} lots loaded", i + 1);
                }
                Application.DoEvents();
            }
            MessageBox.Show(string.Format("Import from file successed. {0} lots imported.", num));
        }

        private void InitializeComponent()
        {
            this.lblPrompt = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.txtOSAT = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // lblPrompt
            // 
            this.lblPrompt.AutoSize = true;
            this.lblPrompt.Location = new System.Drawing.Point(13, 12);
            this.lblPrompt.Name = "lblPrompt";
            this.lblPrompt.Size = new System.Drawing.Size(0, 12);
            this.lblPrompt.TabIndex = 7;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(300, 7);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(99, 25);
            this.button1.TabIndex = 8;
            this.button1.Text = "ReLoad By OSAT";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtOSAT
            // 
            this.txtOSAT.Location = new System.Drawing.Point(414, 9);
            this.txtOSAT.Name = "txtOSAT";
            this.txtOSAT.Size = new System.Drawing.Size(100, 21);
            this.txtOSAT.TabIndex = 9;
            this.txtOSAT.Text = "SPIL";
            // 
            // frmMain
            // 
            this.ClientSize = new System.Drawing.Size(876, 698);
            this.Controls.Add(this.txtOSAT);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lblPrompt);
            this.Name = "frmMain";
            this.Text = "LHD Message Centre";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Shown += new System.EventHandler(this.frmMain_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void InitializeService()
        {
            if (ConfigurationManager.AppSettings["ConnnectToSD"] == "1")
            {
                foreach (SilicondashOSATConfiguration configuration in this.osatConfigurations)
                {
                    SilicondashClient item = new SilicondashClient(configuration);
                    item.MessageArrived += new SilicondashClient.MessageEvent(this.ProcessSilicondashLogs);
                    this.silicondashClients.Add(item);
                    item.InitializeService();
                }
            }
            //邮件通知，测试时需要移除
            this.notificationServer.throwMessage += new NotificationServer.MessageEvent(this.ProcessNotificationServiceLog);
            this.notificationServer.StartService();
        }

        public void InitializeTabPages()
        {
            this.tabControl.Location = new Point(12, 0x1f);
            this.tabControl.Name = "tabLogs";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new Size(0x357, 0x25f);
            this.tabControl.TabIndex = 6;
            this.tabControl.SuspendLayout();
            if (ConfigurationManager.AppSettings["ConnnectToSD"] == "1")
            {
                foreach (SilicondashOSATConfiguration configuration in this.osatConfigurations)
                {
                    TabPage page2 = new TabPage {
                        Location = new Point(4, 0x16),
                        Name = "tab" + configuration.ConfigurationID,
                        Padding = new Padding(3),
                        Size = new Size(0x34f, 0x245),
                        TabIndex = 0,
                        Text = configuration.OSATID,
                        UseVisualStyleBackColor = true
                    };
                    ListBox box2 = new ListBox {
                        BackColor = Color.Black,
                        Dock = DockStyle.Fill,
                        ForeColor = Color.Yellow,
                        FormattingEnabled = true,
                        Location = new Point(3, 3),
                        Name = "lst" + configuration.ConfigurationID
                    };
                    page2.Controls.Add(box2);
                    this.tabControl.Controls.Add(page2);
                    //this.btnClearSystem.Visible = true;
                    //this.btnLoadFromFile.Visible = true;
                }
            }
            TabPage page = new TabPage {
                Location = new Point(4, 0x16),
                Name = "tabPageNotificationService",
                Padding = new Padding(3),
                Size = new Size(0x34f, 0x245),
                TabIndex = 0,
                Text = "Notification Service",
                UseVisualStyleBackColor = true
            };
            ListBox box = new ListBox {
                BackColor = Color.Black,
                Dock = DockStyle.Fill,
                ForeColor = Color.Yellow,
                FormattingEnabled = true,
                Location = new Point(3, 3),
                Name = "lstNotificationService"
            };
            page.Controls.Add(box);
            this.tabControl.Controls.Add(page);
            base.Controls.Add(this.tabControl);
        }

        private void ProcessNotificationServiceLog(string message)
        {
            string fileName = string.Format(@"{0}\NOTIFICATION_SERVICE__{1}.log", this.logDirectory, DateTime.Now.ToString("yyyyMMdd"));
            string item = string.Format("{0}: {1}", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"), message);
            ListBox box = (ListBox) this.tabControl.TabPages["tabPageNotificationService"].Controls["lstNotificationService"];
            box.Items.Add(item);
            bool flag = false;
            while (!flag)
            {
                try
                {
                    EventService.AppendToLogFileToAbsFile(fileName, item + "\r\n");
                    flag = true;
                }
                catch (Exception)
                {
                }
            }
        }

        private void ProcessSilicondashLogs(string osatID, string message)
        {
            SilicondashOSATConfiguration configuration = this.FindOSATConfigurations(osatID);
            ListBox box = (ListBox) this.tabControl.TabPages["tab" + osatID].Controls["lst" + osatID];
            string item = string.Format("{0}: {1}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), message);
            if (box.Items.Count > 0x63)
            {
                box.Items.Clear();
            }
            box.Items.Add(item);
            string fileName = string.Format(@"{0}\{1}_{2}.log", this.logDirectory, osatID, DateTime.Now.ToString("yyyyMMdd"));
            bool flag = false;
            while (!flag)
            {
                try
                {
                    EventService.AppendToLogFileToAbsFile(fileName, item + "\r\n");
                    flag = true;
                }
                catch (Exception)
                {
                }
            }
        }

        private void WriteToLog(string osatID, IList<string> messages)
        {
            string fileName = string.Format(@"{0}\{1}_IMPORT_LOG_{2}.log", this.logDirectory, osatID, DateTime.Now.ToString("yyyyMMdd"));
            foreach (string str2 in messages)
            {
                string logMessage = string.Format("{0}{1}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), str2);
                EventService.AppendToLogFileToAbsFile(fileName, logMessage);
            }
        }
    }
}

