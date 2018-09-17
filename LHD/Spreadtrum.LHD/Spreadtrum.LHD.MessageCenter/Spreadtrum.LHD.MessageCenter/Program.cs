namespace Spreadtrum.LHD.MessageCenter
{
    using KaYi.Services.EventServices;
using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;

    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
            //MessageBox.Show("Message Center Main 已启动");
            EventService.AppendToLogFileToAbsFile(@"C:\LHD_Application\MessageCenter\Logs\SpreadtrumErrors.LOG", DateTime.Now.ToString("yyyyMMddHHmmss") + "\r\nMessage Center Started");
            Application.Run(new frmMain());
            Application.Exit();
        }

        static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            //作为示例，这里用消息框显示异常的信息  
            //MessageBox.Show(e.Exception.Message, "Application_ThreadException异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
            EventService.AppendToLogFileToAbsFile(@"C:\LHD_Application\MessageCenter\Logs\SpreadtrumErrors.LOG", DateTime.Now.ToString("yyyyMMddHHmmss") + "---Application_ThreadException\r\n" + e.Exception.Message);
            
            CmdStartCTIProc(Application.ExecutablePath, "cmd params");

        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            EventService.AppendToLogFileToAbsFile(@"C:\LHD_Application\MessageCenter\Logs\SpreadtrumErrors.LOG", DateTime.Now.ToString("yyyyMMddHHmmss") + "---CurrentDomain_UnhandledException\r\n");
            
            CmdStartCTIProc(Application.ExecutablePath, "cmd params");
        }
        /// <summary>
        /// 在命令行窗口中执行
        /// </summary>
        /// <param name="sExePath"></param>
        /// <param name="sArguments"></param>
        static void CmdStartCTIProc(string sExePath, string sArguments)
        {
            Process p = new Process();
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.CreateNoWindow = false;
            p.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            p.Start();
            p.StandardInput.WriteLine(sExePath + " " + sArguments);
            p.StandardInput.WriteLine("exit");
            p.Close();

            System.Threading.Thread.Sleep(2000);//必须等待，否则重启的程序还未启动完成；根据情况调整等待时间
        }

    }
}

