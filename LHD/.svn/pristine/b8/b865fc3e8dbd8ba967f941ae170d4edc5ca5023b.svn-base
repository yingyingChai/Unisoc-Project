namespace Spreadtrum.LHD.DAL.Users
{
    using KaYi.Database;
    using KaYi.Utilities;
    using Spreadtrum.LHD.Entity.Users;
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;

    public class UserGateway
    {
        private DALGateway<User> dbGateway = new DALGateway<User>();

        public UserGateway()
        {
            this.dbGateway.LoadSchema("USERS");
        }

        public void AddUser(User newUser)
        {
            this.dbGateway.AddNew(newUser);
        }

        public void DeleteUserByID(string userID)
        {
            this.dbGateway.DeleteByFieldValue("UserID", userID);
        }

        public IList<User> GetUserByBUNameAndRole(string BUName, UserRoles role, int pageIndex, int pageSize, out int recordCount)
        {
            Conditions conditions = new Conditions();
            switch (role)
            {
                case UserRoles.OSAT:
                case UserRoles.OSATAdmin:
                    conditions.ConditionExpressions.Add(new Condition("Role", Operator.EqualTo, UserRoles.OSAT));
                    conditions.ConditionExpressions.Add(new Condition("Role", Operator.EqualTo, UserRoles.OSATAdmin));
                    break;

                case UserRoles.PC:
                case UserRoles.PCAdmin:
                    conditions.ConditionExpressions.Add(new Condition("Role", Operator.EqualTo, UserRoles.PC));
                    conditions.ConditionExpressions.Add(new Condition("Role", Operator.EqualTo, UserRoles.PCAdmin));
                    break;

                case UserRoles.PE:
                case UserRoles.PEAdmin:
                    conditions.ConditionExpressions.Add(new Condition("Role", Operator.EqualTo, UserRoles.PE));
                    conditions.ConditionExpressions.Add(new Condition("Role", Operator.EqualTo, UserRoles.PEAdmin));
                    break;

                case UserRoles.QA:
                case UserRoles.QAAdmin:
                    conditions.ConditionExpressions.Add(new Condition("Role", Operator.EqualTo, UserRoles.QA));
                    conditions.ConditionExpressions.Add(new Condition("Role", Operator.EqualTo, UserRoles.QAAdmin));
                    break;
            }
            conditions.Connector = Connector.OR;
            OrderExpression orderExpression = new OrderExpression("CreateTime", true);
            return this.dbGateway.getRecords(pageIndex, pageSize, conditions, orderExpression, StringHelper.BuildStringList(" BUName='" + BUName.Trim() + "' and  AccountState=1 "), Connector.AND, out recordCount);
        }

        public User GetUserByEmail(string email)
        {
            Conditions conditions = new Conditions {
                ConditionExpressions = { new Condition("Email", Operator.EqualTo, email) }
            };
            return this.dbGateway.getRecord(conditions);
        }

        public User GetUserByID(string userID)
        {
            Conditions conditions = new Conditions {
                ConditionExpressions = { new Condition("UserID", Operator.EqualTo, userID) }
            };
            return this.dbGateway.getRecord(conditions);
        }

        public User GetUserByThirdPartyAccountID(string thirdpartyLoginProvider, string thirdpartyAccountID)
        {
            Conditions conditions = new Conditions {
                ConditionExpressions = { new Condition("ThirdpartyLoginProvider", Operator.EqualTo, thirdpartyLoginProvider), new Condition("ThirdpartyAccountID", Operator.EqualTo, thirdpartyAccountID) },
                Connector = Connector.AND
            };
            return this.dbGateway.getRecord(conditions);
        }

        public IList<User> GetUsersBy(string keyword, string email, string role,string selJobType, string accountState, int pageIndex, int pageSize, out int recordCount)
        {
            Conditions conditions = new Conditions();
            if (!StringHelper.isNullOrEmpty(keyword))
            {
                conditions.ConditionExpressions.Add(new Condition("FullName", Operator.Like, keyword));
            }
            if (!StringHelper.isNullOrEmpty(email))
            {
                conditions.ConditionExpressions.Add(new Condition("Email", Operator.Like, email));
            }
            if (!StringHelper.isNullOrEmpty(role))
            {
                conditions.ConditionExpressions.Add(new Condition("Role", Operator.Like, role));
            }
            if (!StringHelper.isNullOrEmpty(accountState))
            {
                string str = accountState.ToUpper();
                if (!(str == "ACTIVE"))
                {
                    if (str == "DISABLED")
                    {
                        conditions.ConditionExpressions.Add(new Condition("AccountState", Operator.EqualTo, 2));
                    }
                }
                else
                {
                    conditions.ConditionExpressions.Add(new Condition("AccountState", Operator.EqualTo, 1));
                }
            }
            if (!string.IsNullOrEmpty(selJobType))
            {
                conditions.ConditionExpressions.Add(new Condition("JobType", Operator.EqualTo, selJobType));
            }
            conditions.Connector = Connector.AND;
            OrderExpression orderExpression = new OrderExpression("FullName", false);
            return this.dbGateway.getRecords(pageIndex, pageSize, conditions, orderExpression, null, Connector.AND, out recordCount);
        }

        public IList<User> GetUsersByRole(UserRoles role, int pageIndex, int pageSize, out int recordCount,string userJobType="FT")
        {
            Conditions conditions = new Conditions();
            switch (role)
            {
                case UserRoles.OSAT:
                case UserRoles.OSATAdmin:
                    conditions.ConditionExpressions.Add(new Condition("Role", Operator.EqualTo, UserRoles.OSAT));
                    //conditions.ConditionExpressions.Add(new Condition("Role", Operator.EqualTo, UserRoles.OSATAdmin));
                    break;

                case UserRoles.PC:
                case UserRoles.PCAdmin:
                    conditions.ConditionExpressions.Add(new Condition("Role", Operator.Like, UserRoles.PC));
                    //conditions.ConditionExpressions.Add(new Condition("Role", Operator.EqualTo, UserRoles.PCAdmin));
                    break;

                case UserRoles.PE:
                case UserRoles.PEAdmin:
                    conditions.ConditionExpressions.Add(new Condition("Role", Operator.Like, UserRoles.PE));
                    //conditions.ConditionExpressions.Add(new Condition("Role", Operator.EqualTo, UserRoles.PEAdmin));
                    break;

                case UserRoles.QA:
                case UserRoles.QAAdmin:
                    conditions.ConditionExpressions.Add(new Condition("Role", Operator.Like, UserRoles.QA));
                    //conditions.ConditionExpressions.Add(new Condition("Role", Operator.EqualTo, UserRoles.QAAdmin));
                    break;
            }
            conditions.Connector = Connector.AND;
            conditions.ConditionExpressions.Add(new Condition("JobType", Operator.Like,userJobType.ToUpper()));
            OrderExpression orderExpression = new OrderExpression("FullName", false);
            return this.dbGateway.getRecords(pageIndex, pageSize, conditions, orderExpression, StringHelper.BuildStringList(" AccountState=1 "), Connector.AND, out recordCount);
        }

        public void UpdateUser(User user)
        {
            this.dbGateway.UpdateByFieldValue("UserID", user.UserID, user);
        }
    }
}

