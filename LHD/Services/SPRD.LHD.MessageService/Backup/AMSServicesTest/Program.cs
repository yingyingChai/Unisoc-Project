using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XJP.BPM.AMSService;

namespace AMSServicesTest
{
    class Program
    {
        static void Main(string[] args)
        {
            K2ExecuteAMS k2 = new K2ExecuteAMS();
            k2.Execute(null);
 
        }
    }
}
