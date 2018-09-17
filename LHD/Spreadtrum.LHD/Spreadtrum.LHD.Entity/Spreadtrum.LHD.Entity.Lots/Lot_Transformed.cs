using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spreadtrum.LHD.Entity.Lots
{
   public class Lot_Transformed
    {
        private string _ID;
        private string _ProductName;
        private string _WaferCode;
        private string _Vendor;
        private string _LotId;
        private int _Status;
        private string _StatusText;
        private string _HoldReason;
        private int _WfCount;
        private decimal _Yield;
        private DateTime _CompletionDate;
        private DateTime _UploadDate;
        private int _WaitingTime;
        private string _AutoJudeResult;
        private string _Stage;
        private string _TestProgram;
        private string _TesterID;
        private string _Platform;
        private string _LBNO;
        private string _OSFailRate;
        private string _Url;
        private int _OperatorStatus;
        private DateTime _CreateDate;
        private string _RecordType;
        private int _VersionID;
        public int FileCount { get; set; }
        public int dayCount { get; set; }
        public string  DiesposRemark { get; set; }
        public List<string> WaferList { get; set; }
        public int WaferidCount { get; set; }
        public List<string> AllBinList { get; set; }
        public List<string> AllBinLimtList { get; set; }
        public List<string> AllBinPercentList { get; set; }
        public string Program { get; set; }
        public DateTime? StartTime { get; set; }
        public string TestStr { get; set; }
      

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

        public int WfCount
        {
            get
            {
                return _WfCount;
            }

            set
            {
                _WfCount = value;
            }
        }

        public decimal Yield
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

        public DateTime UploadDate
        {
            get
            {
                return _UploadDate;
            }

            set
            {
               _UploadDate = value;
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

        public string Stage
        {
            get
            {
                return _Stage;
            }

            set
            {
                _Stage = value;
            }
        }

        public string TestProgram
        {
            get
            {
                return _TestProgram;
            }

            set
            {
                _TestProgram = value;
            }
        }

        public string TesterID
        {
            get
            {
                return _TesterID;
            }

            set
            {
                _TesterID = value;
            }
        }

        public string Platform
        {
            get
            {
                return _Platform;
            }

            set
            {
                _Platform = value;
            }
        }

        public string LBNO
        {
            get
            {
                return _LBNO;
            }

            set
            {
                _LBNO = value;
            }
        }

        public string OSFailRate
        {
            get
            {
                return _OSFailRate;
            }

            set
            {
                _OSFailRate = value;
            }
        }

        public string Url
        {
            get
            {
                return _Url;
            }

            set
            {
                _Url = value;
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

        public int OperatorStatus
        {
            get
            {
                return _OperatorStatus;
            }

            set
            {
                _OperatorStatus = value;
            }
        }

        public int WaitingTime
        {
            get
            {
                return _WaitingTime;
            }

            set
            {
                _WaitingTime = value;
            }
        }


        public string RecordType
        {
            get
            {
                return _RecordType;
            }

            set
            {
                _RecordType = value;
            }
        }

        public int VersionID
        {
            get
            {
                return _VersionID;
            }

            set
            {
                _VersionID = value;
            }
        }
    }
   public class LotDetailModel
    {
        public Lot_Transformed LotTransformed { get; set; }
        public IList<Wafer_Sbin> ListSbin { get; set; }
    }
   public class LotMeteModel
    {
        public string key { get; set; }
        public List<string> values { get; set; }
    }
   
    public enum EnumLotMeta
    {
        allBin,
        allBinLimit,
        allBinPercent,
        allBinType,
        allItem,
        deciderRole,
        decisionAttachement,
        decisionComment,
        decisionMaker,
        decisionTime,
        osat,
        osatLotId,
    }
}
