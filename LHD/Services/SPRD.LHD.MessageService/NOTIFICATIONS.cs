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
    
    public partial class NOTIFICATIONS
    {
        public string MessageID { get; set; }
        public string RecipientID { get; set; }
        public string NotificationType { get; set; }
        public string Message { get; set; }
        public string LotID { get; set; }
        public string EmailID { get; set; }
        public Nullable<bool> Opened { get; set; }
        public Nullable<System.DateTime> ReadTime { get; set; }
        public Nullable<byte> RecordState { get; set; }
        public Nullable<System.DateTime> CreateTime { get; set; }
        public Nullable<System.DateTime> UpdateTime { get; set; }
        public string LastOperator { get; set; }
    }
}
