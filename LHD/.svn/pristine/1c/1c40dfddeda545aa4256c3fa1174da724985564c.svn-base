using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;

namespace SPRD.LHD.MessageService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
#if DEBUG
            DebugRun();
#else
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[] 
			{ 
				new MainService() 
			};
            ServiceBase.Run(ServicesToRun);
#endif
        }

        private static void DebugRun()
        {
            MainEntry objMain = new MainEntry();
            objMain.Run();
        }
    }
}
