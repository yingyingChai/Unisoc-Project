using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.Threading.Tasks;
using System.ServiceProcess;

namespace ConsoleMailApprovalService
{
    [RunInstaller(true)]
    public partial class ProjectInstaller : System.Configuration.Install.Installer
    {
        private ServiceInstaller serviceInstaller;
        private ServiceProcessInstaller ProjectInstaller1;
        private Container components1 = null;
        public ProjectInstaller()
        {
            //InitializeComponent();
            components1 = new System.ComponentModel.Container();
            this.ProjectInstaller1 = new ServiceProcessInstaller();
            this.serviceInstaller = new ServiceInstaller();
            this.ProjectInstaller1.Account = ServiceAccount.LocalSystem;
            this.serviceInstaller.StartType = ServiceStartMode.Automatic;
            this.serviceInstaller.ServiceName = "SPRD.LHD.SD.MailReceive";
            this.serviceInstaller.DisplayName = "SPRD.LHD.SD.MailReceive";
            Installers.Add(serviceInstaller);
            Installers.Add(ProjectInstaller1);
        }
    }

}
