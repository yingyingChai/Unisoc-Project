namespace Spreadtrum.LHD.DAL.Users
{
    using KaYi.Database;
    using KaYi.Utilities;
    using Spreadtrum.LHD.Entity.Users;
    using System;
    using System.Collections.Generic;

    public class SPRDUserGateway
    {
        private DALGateway<SPRDUser> dbGateway = new DALGateway<SPRDUser>();

        public SPRDUserGateway()
        {
            this.dbGateway.LoadSchema("SPRD_USERS");
            //this.dbGateway.LoadSchema("USERS");
        }

        public void AddNew(SPRDUser newSPRDUser)
        {
            this.dbGateway.AddNew(newSPRDUser);
        }

        public void DeleteBySPRDUserID(string SPRDUserID)
        {
            this.dbGateway.DeleteByFieldValue("Id", SPRDUserID);
        }

        public SPRDUser GetSPRDUserByID(string SPRDUserID)
        {
            Conditions conditions = new Conditions {
                ConditionExpressions = { new Condition("Id", Operator.EqualTo, SPRDUserID) }
            };
            return this.dbGateway.getRecord(conditions);
        }

        public SPRDUser GetSPRDUserBySN(string SPRDUserSN)
        {
            Conditions conditions = new Conditions
            {
                ConditionExpressions = { new Condition("SN", Operator.EqualTo, SPRDUserSN) }
            };
            return this.dbGateway.getRecord(conditions);
        }

        public SPRDUser GetSPRDUsersByEmail(string keyword)
        {
            if (StringHelper.isNullOrEmpty(keyword))
            {
                return null;
            }
            Conditions conditions = new Conditions();
            conditions.ConditionExpressions.Add(new Condition("Email", Operator.EqualTo, keyword));
            return this.dbGateway.getRecord(conditions);
        }

        public IList<SPRDUser> GetSPRDUsersByKeyword(string keyword)
        {
            if (StringHelper.isNullOrEmpty(keyword))
            {
                return null;
            }
            Conditions conditions = new Conditions();
            conditions.ConditionExpressions.Add(new Condition("Email", Operator.Like, keyword));
            conditions.ConditionExpressions.Add(new Condition("ChineseName", Operator.Like, keyword));
            conditions.ConditionExpressions.Add(new Condition("EnglishName", Operator.Like, keyword));
            conditions.ConditionExpressions.Add(new Condition("Account", Operator.Like, keyword));
            conditions.Connector = Connector.OR;
            OrderExpression orderExpression = new OrderExpression("EnglishName", false);
            int recordCount = 0;
            return this.dbGateway.getRecords(0, 30, conditions, orderExpression, null, Connector.AND, out recordCount);
        }
    }
}

