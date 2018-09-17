namespace Spreadtrum.LHD.DAL.Users
{
    using KaYi.Database;
    using Spreadtrum.LHD.Entity.Users;
    using System;
    using System.Collections.Generic;

    public class UserGroupGateway
    {
        private DALGateway<UserGroup> dbGateway = new DALGateway<UserGroup>();

        public UserGroupGateway()
        {
            this.dbGateway.LoadSchema("USER_GROUPS");
        }

        public void AddNew(UserGroup newUserGroup)
        {
            this.dbGateway.AddNew(newUserGroup);
        }

        public void DeleteByUserGroupID(string UserGroupID)
        {
            this.dbGateway.DeleteByFieldValue("UserGroupID", UserGroupID);
        }

        public UserGroup GetUserGroupByID(string UserGroupID)
        {
            Conditions conditions = new Conditions {
                ConditionExpressions = { new Condition("UserGroupID", Operator.EqualTo, UserGroupID) }
            };
            return this.dbGateway.getRecord(conditions);
        }

        public IList<UserGroup> GetUserGroupsBy()
        {
            return null;
        }

        public void UpdateByPK(UserGroup objUserGroup)
        {
            this.dbGateway.UpdateByFieldValue("UserGroupID", objUserGroup.UserGroupID, objUserGroup);
        }
    }
}

