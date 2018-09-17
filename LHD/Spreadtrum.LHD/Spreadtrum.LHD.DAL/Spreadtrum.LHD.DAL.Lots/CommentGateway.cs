namespace Spreadtrum.LHD.DAL.Lots
{
    using KaYi.Database;
    using KaYi.Utilities;
    using Spreadtrum.LHD.Entity.Lots;
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;

    public class CommentGateway
    {
        private DALGateway<Comment> dbGateway = new DALGateway<Comment>();

        public CommentGateway()
        {
            this.dbGateway.LoadSchema("COMMENTS");
        }

        public void AddNew(Comment newComment)
        {
            this.dbGateway.AddNew(newComment);
        }

        public void DeleteByCommentID(string CommentID)
        {
            this.dbGateway.DeleteByFieldValue("CommentID", CommentID);
        }

        public Comment GetCommentByID(string CommentID)
        {
            Conditions conditions = new Conditions {
                ConditionExpressions = { new Condition("CommentID", Operator.EqualTo, CommentID) }
            };
            return this.dbGateway.getRecord(conditions);
        }

        public IList<Comment> GetCommentsBy(string lotID, string commentType, bool osatUser, string orderBy, bool desc, int pageIndex, int pageSize, out int recordCount)
        {
            Conditions conditions = new Conditions();
            if (!StringHelper.isNullOrEmpty(lotID))
            {
                conditions.ConditionExpressions.Add(new Condition("LotID", Operator.EqualTo, lotID));
            }
            if (!StringHelper.isNullOrEmpty(commentType))
            {
                conditions.ConditionExpressions.Add(new Condition("CommentType", Operator.EqualTo, commentType));
            }
            if (osatUser)
            {
                conditions.ConditionExpressions.Add(new Condition("Internal", Operator.EqualTo, false));
            }
            conditions.Connector = Connector.AND;
            OrderExpression orderExpression = null;
            if (!StringHelper.isNullOrEmpty(orderBy))
            {
                orderExpression = new OrderExpression(orderBy, desc);
            }
            return this.dbGateway.getRecords(pageIndex, pageSize, conditions, orderExpression, null, Connector.AND, out recordCount);
        }

        public void UpdateByPK(Comment objComment)
        {
            this.dbGateway.UpdateByFieldValue("CommentID", objComment.CommentID, objComment);
        }
    }
}

