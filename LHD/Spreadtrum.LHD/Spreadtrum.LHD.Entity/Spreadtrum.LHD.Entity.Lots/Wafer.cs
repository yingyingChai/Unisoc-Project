﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spreadtrum.LHD.Entity.Lots
{
    public class Wafer
    {
        private string _ID;
        private string _ProductName;
        private string _WaferCode;
        private string _Vendor;
        private string _LotId;
        private string _AutoJudeResult;
        private string _WaferID;
        private string _TransformID;
        private int _Status;
        private string _StatusText;
        private string _HoldReason;
        private double _Yield;
        private DateTime _CompletionDate;
        private int _PEDispose;
        private string _PEDisposeText;
        private string _PEComment;
        private int _QADispose;
        private string _QADisposeText;
        private string _QAComment;
        private int _SPRDDecision;
        private string _SPRDDecisionText;
        private DateTime _CreateDate;
        private int _VendorConfirm;
        private string _VendorComment;
        private string _Program;
        private DateTime _StartTime;
        private int _TotalDieCount;
        private string _ImgUrl;
        private IList<Wafer_Sbin> _ListSbin;
        private Boolean _IsTriggered;
        public string StrStartTime { get; set; }
        public string ID
        {
            get
            {
                return _ID;
            }

            set
            {
                _ID = value;
            }
        }

        public string ProductName
        {
            get
            {
                return _ProductName;
            }

            set
            {
                _ProductName = value;
            }
        }

        public string WaferCode
        {
            get
            {
                return _WaferCode;
            }

            set
            {
                _WaferCode = value;
            }
        }

        public string Vendor
        {
            get
            {
                return _Vendor;
            }

            set
            {
                _Vendor = value;
            }
        }

        public string LotId
        {
            get
            {
                return _LotId;
            }

            set
            {
                _LotId = value;
            }
        }

        public string AutoJudeResult
        {
            get
            {
                return _AutoJudeResult;
            }

            set
            {
                _AutoJudeResult = value;
            }
        }

        public string WaferID
        {
            get
            {
                return _WaferID;
            }

            set
            {
                _WaferID = value;
            }
        }

        public string TransformID
        {
            get
            {
                return _TransformID;
            }

            set
            {
                _TransformID = value;
            }
        }

        public int Status
        {
            get
            {
                return _Status;
            }

            set
            {
                _Status = value;
            }
        }

        public string StatusText
        {
            get
            {
                return _StatusText;
            }

            set
            {
                _StatusText = value;
            }
        }

        public string HoldReason
        {
            get
            {
                return _HoldReason;
            }

            set
            {
                _HoldReason = value;
            }
        }

        public double Yield
        {
            get
            {
                return _Yield;
            }

            set
            {
                _Yield = value;
            }
        }

        public DateTime CompletionDate
        {
            get
            {
                return _CompletionDate;
            }

            set
            {
                _CompletionDate = value;
            }
        }

        public int PEDispose
        {
            get
            {
                return _PEDispose;
            }

            set
            {
                _PEDispose = value;
            }
        }

        public string PEDisposeText
        {
            get
            {
                return _PEDisposeText;
            }

            set
            {
                _PEDisposeText = value;
            }
        }

        public string PEComment
        {
            get
            {
                return _PEComment;
            }

            set
            {
                _PEComment = value;
            }
        }

        public int QADispose
        {
            get
            {
                return _QADispose;
            }

            set
            {
                _QADispose = value;
            }
        }

        public string QADisposeText
        {
            get
            {
                return _QADisposeText;
            }

            set
            {
                _QADisposeText = value;
            }
        }

        public string QAComment
        {
            get
            {
                return _QAComment;
            }

            set
            {
                _QAComment = value;
            }
        }

        public int SPRDDecision
        {
            get
            {
                return _SPRDDecision;
            }

            set
            {
                _SPRDDecision = value;
            }
        }

        public string SPRDDecisionText
        {
            get
            {
                return _SPRDDecisionText;
            }

            set
            {
                _SPRDDecisionText = value;
            }
        }

        public DateTime CreateDate
        {
            get
            {
                return _CreateDate;
            }

            set
            {
                _CreateDate = value;
            }
        }

        public int VendorConfirm
        {
            get
            {
                return _VendorConfirm;
            }

            set
            {
                _VendorConfirm = value;
            }
        }

        public string VendorComment
        {
            get
            {
                return _VendorComment;
            }

            set
            {
                _VendorComment = value;
            }
        }

        public string Program
        {
            get
            {
                return _Program;
            }

            set
            {
                _Program = value;
            }
        }

        public DateTime StartTime
        {
            get
            {
                return _StartTime;
            }

            set
            {
                _StartTime = value;
            }
        }

        public int TotalDieCount
        {
            get
            {
                return _TotalDieCount;
            }

            set
            {
                _TotalDieCount = value;
            }
        }

        public string ImgUrl
        {
            get
            {
                return _ImgUrl;
            }

            set
            {
                _ImgUrl = value;
            }
        }

        public IList<Wafer_Sbin> ListSbin
        {
            get
            {
                return _ListSbin;
            }

            set
            {
                _ListSbin = value;
            }
        }
        public Boolean IsTriggered
        {
            get
            {
                return _IsTriggered;
            }
            set
            {
                _IsTriggered = value;
            }
        }
    }
    public class SqlWafer
    {
        public string ID { get; set; }
        public string TransformID { get; set; }
        public string WaferID { get; set; }
        public int Status { get; set; }
        public string HoldReason { get; set; }
        public decimal Yield { get; set; }
        public DateTime CompletionDate { get; set; }
        public int PEDispose { get; set; }
        public int QADispose { get; set; }
        public int SPRDDecision { get; set; }
        public DateTime CreateDate { get; set; }
        public int VendorConfirm { get; set; }
        public string Program { get; set; }
        public DateTime? StartTime { get; set; }
        public int TotalDieCount { get; set; }
        public string ImgUrl { get; set; }

    }
}
