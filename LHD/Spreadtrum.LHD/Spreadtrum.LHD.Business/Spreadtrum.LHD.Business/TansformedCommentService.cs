using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spreadtrum.LHD.DAL.Lots;
using Spreadtrum.LHD.Entity.Lots;
using Spreadtrum.LHD.Entity.Users;

namespace Spreadtrum.LHD.Business
{
  public  class TansformedCommentService
    {
        private TransformedCommentGateway way = new TransformedCommentGateway();
       
        public void AddComment(TranformedComment comment)
        {
            this.way.AddComment(comment);
        }

        public TranformedComment GenerateTranformedComment(string commentid,string lotId,string commentText, bool internalOnly, User currentUser)
        {
            TranformedComment comment = new TranformedComment {
                CommentID = commentid,
                CommentText = commentText,
                Operator = currentUser.UserID,
                LotID = lotId,
                RecordState = 0,
                CreateTime = DateTime.Now,
                UpdateTime = DateTime.Now,
                LastOperator = currentUser.UserID,
                CommentType = CommentTypes.CommentOnly,
                OperatorName = currentUser.FullName,
                OperatorRole=currentUser.Role.ToString(),
                OperatorBUName=currentUser.BUName,
                OperatorEmail=currentUser.Email,
                Internal=internalOnly,
            };
           
            return comment;
        }
    }
}
