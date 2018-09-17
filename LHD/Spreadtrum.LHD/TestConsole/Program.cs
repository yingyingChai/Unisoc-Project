using KaYi.Services.Emails.Library.Gateway;
using KaYi.Services.Emails.Library.Model;
using KaYi.Utilities;
using Spreadtrum.LHD.Business;
using Spreadtrum.LHD.DAL.Lots;
using Spreadtrum.LHD.Entity.Lots;
using Spreadtrum.LHD.MessageCenter;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            string osatID = "Ardentec";
            FileInfo[] files = new DirectoryInfo(@"C:\LHD_APPLICATION\MessageCenter\Messages\" + osatID + @"\In\").GetFiles();
            int num = 0;
            for (int i = 0; i <= (files.Length - 1); i++)
            {
                IList<string> messages = new List<string>();
                num++;
                FileInfo info2 = files[i];
                string str2 = FileService.ReadTextFile(info2.FullName);
                int index = str2.IndexOf("{\"LOT_JUDGEMENT");
                string jsonStr = str2.Substring(index, (str2.Length - index) - 1);
                //Lot lot = LotService.ReadLotFromJson(osatID, jsonStr, messages);
                //Console.WriteLine(osatID + messages);
                //if (lot != null)
                //{
                //    LotService.SaveLotAndInformQA_AND_PE(lot);
                //    //this.lblPrompt.Text = string.Format("{0} lots loaded", i + 1);
                //}
                 
                Lot_TransformService lts = new Lot_TransformService();
                Lot_Transformed lt = lts.ReadJson(jsonStr, messages);

                if (lt != null)
                {
                    lts.SaveCPJsonlot(lt);
                }


                //Application.DoEvents();
            }
            Console.WriteLine(string.Format("Import from file successed. {0} lots imported.", num));
            Console.Read();
        }

        public static string GetUrlDeCode(string str, Encoding encoding)
        {
            if (encoding == null)
            {
                Encoding utf8 = Encoding.UTF8;
                //首先用utf-8进行解码                    
                string code = HttpUtility.UrlDecode(str.ToUpper(), utf8);
                //将已经解码的字符再次进行编码.
                string encode = HttpUtility.UrlEncode(code, utf8).ToUpper();
                if (str == encode)
                    encoding = Encoding.UTF8;
                else
                    encoding = Encoding.GetEncoding("gb2312");
            }
            return HttpUtility.UrlDecode(str, encoding);
        }
    }
}
