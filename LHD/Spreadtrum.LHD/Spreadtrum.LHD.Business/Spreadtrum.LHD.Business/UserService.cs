namespace Spreadtrum.LHD.Business
{
    using KaYi.Emails.Library;
    using KaYi.Services.Emails.Library.Model;
    using Spreadtrum.LHD.DAL.Users;
    using Spreadtrum.LHD.Entity.Users;
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;

    public static class UserService
    {
        private static OSATUserGateway osatUserGateway = new OSATUserGateway();
        private static UserGateway userGateway = new UserGateway();
        private static UserGroupGateway userGroupGateway = new UserGroupGateway();
        private static SPRDUserGateway sprdUserGateway = new SPRDUserGateway();

        
        public static void AddAccount(User account)
        {
            userGateway.AddUser(account);
        }

        public static void CreateSPRDUser(UserRoles userRole, string thirdPartyLoginAccount, string BUName, string fullname, string email, bool active, string operateUser,string jobType)
        {
            User newUser = new User {
                AccountState = active ? 1 : 0,
                FullName = fullname,
                LastOperator = operateUser,
                LoginPassword = "",
                RecordState = 0,
                Remarks = "",
                Role = userRole,
                ThirdpartyAccountID = @"Spreadtrum\" + thirdPartyLoginAccount,
                ThirdpartyLoginProvider = "SPRDUser",
                CreateTime = DateTime.Now,
                UpdateTime = DateTime.Now,
                Email = email,
                BUName = BUName,
                UserID = Guid.NewGuid().ToString(),
                JobType= jobType,
            };
            userGateway.AddUser(newUser);
            SendNewSPRDUserNotificationEmail(newUser);
        }

        public static void DeleteByID(string userID)
        {
            userGateway.DeleteUserByID(userID);
        }

        public static void DisableUser(string userID)
        {
            User userByID = GetUserByID(userID);
            userByID.AccountState = 2;
            userGateway.UpdateUser(userByID);
        }

        public static List<User> GetAccounts()
        {
            return null;
        }

        public static User GetAccountsByEmail(string email)
        {
            return userGateway.GetUserByEmail(email);
        }

        public static User GetOSATUserByHashedCID(string hashedCID)
        {
            OSATUser user = osatUserGateway.GetOSATUserByMD5CID(hashedCID);
            return new User { 
                AccountState = 1, AccountType = 0, BUName = user.SupID, CreateTime = Convert.ToDateTime("2016-01-01"), Email = user.SupMail, FullName = user.SupRealUser, LastOperator = "SYSTEM", LoginPassword = "", RecordState = 0, Remarks = "", Role = user.Role, ThirdpartyAccountID = user.CID.ToString(), ThirdpartyLoginProvider = "LHD LINKED SERVER", UpdateTime = DateTime.Now, UserID = user.MD5CID, WaitForConfirm = user.WaitForConfirm, 
                WaitForDispose = 0, WaitForOtherBinDispose = 0, NewComments = user.NewComments, EQALots = user.EQALots,JobType=user.JobType
             };
        }

        public static User GetSPRDUserSN(string SN)
        {
            SPRDUser user = sprdUserGateway.GetSPRDUserBySN(SN);
            if(user!=null){
               User u= userGateway.GetUserByEmail(user.Email);
               if (u != null)
               {
                   u.ChineseName = user.ChineseName;
                   return u;
               }
            }
            return null;
            
        }
        public static IList<OSATUser> GetOSATUsersByOSATID(string osatID)
        {
            return osatUserGateway.GetOSATUsersByOSATID(osatID);
        }

        public static IList<OSATUser> GetCPOSATUsersByOSATID(string osatID)
        {
            return osatUserGateway.GetCPOSATUsersByOSATID(osatID);
        }
        public static User GetUserByID(string userID)
        {
            return userGateway.GetUserByID(userID);
        }

        public static User GetUserByThirdPartyAccountID(string thirdpartyLoginProvider, string thirdpartyAccountID)
        {
            return userGateway.GetUserByThirdPartyAccountID(thirdpartyLoginProvider, thirdpartyAccountID);
        }

        public static IList<User> GetUsersBy(string keyword, string email, string role,string selJobType, string accountState, int pageIndex, int pageSize, out int recordCount)
        {
            return userGateway.GetUsersBy(keyword, email, role, selJobType, accountState, pageIndex, pageSize, out recordCount);
        }

        public static IList<User> GetUsersByRole(UserRoles role, int pageIndex, int pageSize, out int recordCount,string userJobType="FT")
        {
            return userGateway.GetUsersByRole(role, pageIndex, pageSize, out recordCount,userJobType);
        }

        public static void ReActiveUser(string userID, UserRoles role)
        {
            User userByID = userGateway.GetUserByID(userID);
            userByID.AccountState = 1;
            userByID.Role = role;
            userGateway.UpdateUser(userByID);
        }

        private static void SendNewOSATUserNotificationEmail(User user, string password)
        {
            Email emailByID = EmailService.GetEmailByID("OSATNEWUSERNOTIFICATION");
            emailByID.AccountIDs = user.UserID;
            emailByID.Body = emailByID.Body.Replace("#FULLNAME_TAG#", user.FullName);
            emailByID.Body = emailByID.Body.Replace("#ROLE_TAG#", user.Role.ToString());
            emailByID.Body = emailByID.Body.Replace("#SYSTEM_URL#", "https://lhd.spreadtrum.com");
            emailByID.Body = emailByID.Body.Replace("#LOGIN_ID#", user.Email);
            emailByID.Body = emailByID.Body.Replace("#LOGIN_PASSWORD#", password);
            emailByID.EmailID = Guid.NewGuid().ToString();
            emailByID.NextTryTime = DateTime.Now.AddSeconds(30.0);
            emailByID.Recipients = "hexh@163.com";
            EmailService.AddEmail(emailByID);
        }

        private static void SendNewSPRDUserNotificationEmail(User user)
        {
            Email emailByID = EmailService.GetEmailByID("SPRDNEWUSERNOTIFICATION");
            emailByID.AccountIDs = user.UserID;
            emailByID.Body = emailByID.Body.Replace("#FULLNAME_TAG#", user.FullName);
            emailByID.Body = emailByID.Body.Replace("#ROLE_TAG#", user.Role.ToString());
            emailByID.Body = emailByID.Body.Replace("#SYSTEM_URL#", "https://lhd.spreadtrum.com");
            emailByID.EmailID = Guid.NewGuid().ToString();
            emailByID.NextTryTime = DateTime.Now.AddSeconds(30.0);
            emailByID.Recipients = "hexh@163.com";
            EmailService.AddEmail(emailByID);
        }

        public static void Update(User account)
        {
            userGateway.UpdateUser(account);
        }
      
    }
}

