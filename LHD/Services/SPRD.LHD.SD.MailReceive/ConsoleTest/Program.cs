using SPRD.LHD.SD.MailReceive;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //SDMailReceive ea = new SDMailReceive();
                //ea.MailPop3Client();

                Console.WriteLine(Convert.ToDateTime("2017-06-26 21:59:45").ToString("yyyy-MM-dd HH:mm:ss"));

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
            Console.Read();
        }
    }
}
