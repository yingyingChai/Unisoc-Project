namespace Spreadtrum.LHD.DAL.Users
{
    using KaYi.Database;
    using Spreadtrum.LHD.Entity.Users;
    using System;
    using System.Collections.Generic;

    public class GroupGateway
    {
        private DALGateway<Group> dbGateway = new DALGateway<Group>();

        public GroupGateway()
        {
            this.dbGateway.LoadSchema("GROUPS");
        }

        public void AddNew(Group newGroup)
        {
            this.dbGateway.AddNew(newGroup);
        }

        public void DeleteByGroupID(string GroupID)
        {
            this.dbGateway.DeleteByFieldValue("GroupID", GroupID);
        }

        public Group GetGroupByID(string GroupID)
        {
            Conditions conditions = new Conditions {
                ConditionExpressions = { new Condition("GroupID", Operator.EqualTo, GroupID) }
            };
            return this.dbGateway.getRecord(conditions);
        }

        public IList<Group> GetGroupsBy()
        {
            return null;
        }

        public void UpdateByPK(Group objGroup)
        {
            this.dbGateway.UpdateByFieldValue("GroupID", objGroup.GroupID, objGroup);
        }
    }
}

