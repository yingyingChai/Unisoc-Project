//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SPRD.LHD.MessageService
{
    using System;
    using System.Collections.Generic;
    
    public partial class COMMENTS
    {
        public string CommentID { get; set; }
        public string CommentText { get; set; }
        public string Operator { get; set; }
        public string LotID { get; set; }
        public Nullable<byte> RecordState { get; set; }
        public Nullable<System.DateTime> CreateTime { get; set; }
        public Nullable<System.DateTime> UpdateTime { get; set; }
        public string LastOperator { get; set; }
        public string CommentType { get; set; }
        public string OperatorName { get; set; }
        public string OperatorRole { get; set; }
        public string OperatorBUName { get; set; }
        public string OperatorEmail { get; set; }
        public Nullable<bool> Internal { get; set; }
        public Nullable<int> Dispose { get; set; }
        public string DisposeText { get; set; }
        public Nullable<int> OtherBinDispose { get; set; }
    }
}
