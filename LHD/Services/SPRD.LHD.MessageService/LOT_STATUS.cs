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
    
    public partial class LOT_STATUS
    {
        public byte ID { get; set; }
        public string StatusDescription { get; set; }
        public System.DateTime CreateTime { get; set; }
        public Nullable<System.DateTime> UpdateTime { get; set; }
        public string LastOperator { get; set; }
        public Nullable<byte> Sequence { get; set; }
    }
}
