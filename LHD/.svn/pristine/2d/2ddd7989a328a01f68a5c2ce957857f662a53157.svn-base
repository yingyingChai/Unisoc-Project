namespace Spreadtrum.LHD.Entity.Users
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.CompilerServices;

    public class User
    {
        private string _BUName = string.Empty;
        private string _ChineseName = string.Empty;
        private int _EQALots;
        private int _WaitForOtherBinDispose;

        public int AccountState { get; set; }

        public byte AccountType { get; set; }

        public string BUName
        {
            get
            {
                return this._BUName;
            }
            set
            {
                this._BUName = value;
            }
        }

        public string ChineseName
        {
            get
            {
                return this._ChineseName;
            }
            set
            {
                this._ChineseName = value;
            }
        }

        public DateTime CreateTime { get; set; }

        [Required(ErrorMessage="邮箱不能为空"), RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage="邮箱格式有误")]
        public string Email { get; set; }

        public int EQALots
        {
            get
            {
                return this._EQALots;
            }
            set
            {
                this._EQALots = value;
            }
        }

        [StringLength(0xff, ErrorMessage="姓名最长为255个字符")]
        public string FullName { get; set; }

        public string LastOperator { get; set; }

        [Required(ErrorMessage="登录密码不能为空"), StringLength(0x10, MinimumLength=8, ErrorMessage="密码长度为8-16"), Display(Name="Password")]
        public string LoginPassword { get; set; }

        public int NewComments { get; set; }

        public byte RecordState { get; set; }

        public string Remarks { get; set; }

        public UserRoles Role { get; set; }

        public string RoleText
        {
            get
            {
                string str = string.Empty;
                switch (this.Role)
                {
                    case UserRoles.OSAT:
                    case UserRoles.PC:
                    case UserRoles.PE:
                    case UserRoles.QA:
                        return this.Role.ToString();

                    case UserRoles.OSATAdmin:
                        return "OSAT Admin";

                    case UserRoles.PCAdmin:
                        return "PC Admin";

                    case UserRoles.PEAdmin:
                        return "PE Admin";

                    case UserRoles.QAAdmin:
                        return "QA Admin";
                }
                return str;
            }
        }

        public string ThirdpartyAccountID { get; set; }

        public string ThirdpartyLoginProvider { get; set; }

        public DateTime UpdateTime { get; set; }

        public string UserID { get; set; }

        public int WaitForConfirm { get; set; }

        public int WaitForDispose { get; set; }

        public int WaitForOtherBinDispose
        {
            get
            {
                return this._WaitForOtherBinDispose;
            }
            set
            {
                this._WaitForOtherBinDispose = value;
            }
        }
    }
}

