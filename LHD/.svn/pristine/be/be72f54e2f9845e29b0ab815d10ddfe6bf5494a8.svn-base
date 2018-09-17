using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spreadtrum.LHD.Entity.Lots;
using KaYi.Database;
using KaYi.Utilities;
using System.Data;
namespace Spreadtrum.LHD.DAL.Lots
{
   public class TransformedCommentGateway
    {

        DALGateway<TranformedComment> dbGateway = new DALGateway<TranformedComment>();
        public TransformedCommentGateway()
        {
            this.dbGateway.LoadSchema("TRANSFORMEDCOMMENT");
        }
      
        public void AddComment(TranformedComment comment)
        {
            this.dbGateway.AddNew(comment);
        }

        public  IList<TranformedComment> GetCommentsBy(string lotID, string commentType, bool osatUser, string orderBy, bool desc, int pageIndex, int pageSize, out int recordCount)
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
    }
}
