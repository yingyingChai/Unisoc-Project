namespace Spreadtrum.LHD.DAL.Users
{
    using KaYi.Database;
    using KaYi.Utilities;
    using Spreadtrum.LHD.Entity.Users;
    using System;
    using System.Collections.Generic;

    public class OSATUserGateway
    {
        private DALGateway<OSATUser> dbGateway = new DALGateway<OSATUser>();

        public OSATUserGateway()
        {
            this.dbGateway.LoadSchema("OSAT_USERS");
        }

        public OSATUser GetOSATUserByID(string OSATUserID)
        {
            Conditions conditions = new Conditions {
                ConditionExpressions = { new Condition("CID", Operator.EqualTo, OSATUserID) }
            };
            return this.dbGateway.getRecord(conditions);
        }

        public OSATUser GetOSATUserByMD5CID(string hashedCid)
        {
            Conditions conditions = new Conditions {
                ConditionExpressions = { new Condition("MD5CID", Operator.EqualTo, hashedCid) }
            };
            return this.dbGateway.getRecord(conditions);
        }

        public OSATUser GetOSATUsersByEmail(string email)
        {
            if (StringHelper.isNullOrEmpty(email))
            {
                return null;
            }
            Conditions conditions = new Conditions();
            conditions.ConditionExpressions.Add(new Condition("SupMail", Operator.EqualTo, email));
            return this.dbGateway.getRecord(conditions);
        }

        public IList<OSATUser> GetOSATUsersByOSATID(string osatID)
        {
            Conditions conditions = new Conditions {
                ConditionExpressions = { new Condition("SupID", Operator.EqualTo, osatID) }
            };
            return this.dbGateway.getRecords(0, 0x270f, conditions, null);
        }
    }
}

