namespace Spreadtrum.LHD.Mvc
{
    using KaYi.Services.EventServices;
    using Spreadtrum.LHD.Business;
    using Spreadtrum.LHD.Entity.Users;
    using System;
    using System.Runtime.InteropServices;

    public static class AdLoginHelper
    {
        public static User TryAdLogin(string windowsUserName, out string nextUrl, string endPoint)
        {
            string str = windowsUserName;
            EventService.AppendToLogFileToAbsFile(@"C:\LHD_APPLICATION\Logs.txt", string.Format("Windows Account login:{0}:{1}\r\n", endPoint, str));
            User userByThirdPartyAccountID = UserService.GetUserByThirdPartyAccountID("SPRDUser", str);
            if (userByThirdPartyAccountID != null)
            {
                EventService.AppendToLogFileToAbsFile(@"C:\LHD_APPLICATION\UserInfo.txt", string.Format("Windows Account login:{0}:{1}\r\n", endPoint, str));
            }
            if (userByThirdPartyAccountID != null)
            {
                userByThirdPartyAccountID.ChineseName = SPRDInterface.GetSPRDUserByEmail(userByThirdPartyAccountID.Email).ChineseName;
                switch (userByThirdPartyAccountID.Role)
                {
                    case UserRoles.PC:
                    case UserRoles.PCAdmin:
                        if (userByThirdPartyAccountID.JobType.ToUpper().IndexOf("FT") > -1)
                        {
                            nextUrl = "/Lots/Query/NewComment";
                        }
                        else {
                            nextUrl = "/Lots/Transform";
                        }
                        return userByThirdPartyAccountID;

                    case UserRoles.PE:
                    case UserRoles.PEAdmin:
                    case UserRoles.QA:
                    case UserRoles.QAAdmin:
                        if (userByThirdPartyAccountID.JobType.ToUpper().IndexOf("FT") > -1)
                        {
                            nextUrl = "/Lots/Query/LotDispose";
                        }
                        else {
                            nextUrl = "/Lots/Transform";
                        }
                           
                        return userByThirdPartyAccountID;
                }
                nextUrl = "/Accounts/Login";
                return userByThirdPartyAccountID;
            }
            nextUrl = "/Accounts/Login";
            return null;
        }
    }
}

