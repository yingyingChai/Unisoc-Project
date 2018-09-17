using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMailApprovalService.Entity
{

    public class CurrentEmployeeInfo
    {
        public string Domain = "";
        public string Language = "";
        public string LoginType = "";
        public string Password = "";
        public string SecurityLabelName = "";
        public string Username = "";
        public string K2UserID = "";
        public string _Domain
        {
            get { return Domain; }
            set { Domain = value; }
        }
        public string _Language
        {
            get { return Language; }
            set { Language = value; }
        }
        public string _LoginType
        {
            get { return LoginType; }
            set { LoginType = value; }
        }
        public string _Password
        {
            get { return Password; }
            set { Password = value; }
        }
        public string _SecurityLabelName
        {
            get { return SecurityLabelName; }
            set { SecurityLabelName = value; }
        }
        public string _Username
        {
            get { return Username; }
            set { Username = value; }
        }
        public string _K2UserID
        {
            get { return K2UserID; }
            set { K2UserID = value; }
        }
    }
}
