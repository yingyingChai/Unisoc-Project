using SPRD.LHD.MessageService;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace AMSServicesTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //K2ExecuteAMS k2 = new K2ExecuteAMS();
            //k2.Execute(null);
            //List<string> tolist = new List<string>();
            //tolist.Add("shine.shen@spreadtrum.com");
            //List<string> cclist = new List<string>();
            //List<string> bcclist = new List<string>();

            //MailHelp.SendMail(tolist, cclist, bcclist, "test subject", "test body", System.Net.Mail.MailPriority.Normal);
            SpreadtrumLHDEntities Db = new SpreadtrumLHDEntities();
            SqlParameter[] para = new SqlParameter[] { new SqlParameter("@ID", "9d2e022f-0212-4f76-b573-f2bb367ef7c2") };
            Db.Database.ExecuteSqlCommand("exec sp_UpdateLotsReasons @ID", para);

            
        }
    }
}
