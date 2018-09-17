namespace Spreadtrum.LHD.Entity.Lots
{
    using System;

    public class NotificationswithLot
    {
        private string _AutoJudgeResult = string.Empty;
        private int _Bin1;
        private int _Bin10;
        private int _Bin2;
        private int _Bin3;
        private int _Bin4;
        private int _Bin5;
        private int _Bin6;
        private int _Bin7;
        private int _Bin8;
        private int _Bin9;
        private DateTime _CompletionDate;
        private DateTime _CreateTime;
        private int _Damage;
        private string _DeviceCode = string.Empty;
        private string _DeviceName = string.Empty;
        private string _Die1CP = string.Empty;
        private string _Die1LotNO = string.Empty;
        private string _Die2CP = string.Empty;
        private string _Die2LotNO = string.Empty;
        private string _Die3CP = string.Empty;
        private string _Die3LotNO = string.Empty;
        private string _Die4CP = string.Empty;
        private string _Die4LotNO = string.Empty;
        private string _HoldReason = string.Empty;
        private string _ID = string.Empty;
        private string _LastOperator = string.Empty;
        private string _LBNO = string.Empty;
        private int _Loss;
        private string _LotID = string.Empty;
        private string _LotNO = string.Empty;
        private string _LotType = string.Empty;
        private bool _ManualHold = true;
        private string _NotificationType = string.Empty;
        private bool _Opened = true;
        private DateTime _OperateTime;
        private string _OperatorID = string.Empty;
        private string _OperatorName = string.Empty;
        private string _OSRate = string.Empty;
        private bool _OtherBinDispose = true;
        private int _PEDispose;
        private string _PEDisposeText = string.Empty;
        private string _Platforms = string.Empty;
        private string _PreviousRecordID = string.Empty;
        private int _QADispose;
        private string _QADisposeText = string.Empty;
        private int _QtyIn;
        private string _RecipientID = string.Empty;
        private int _RecordState;
        private string _RecordType = string.Empty;
        private int _RetestTimes;
        private int _SPRDDecision;
        private string _SPRDDecisionText = string.Empty;
        private string _Status = string.Empty;
        private string _SubconLot = string.Empty;
        private string _TesterID = string.Empty;
        private string _TestProgram = string.Empty;
        private DateTime _UpdateTime;
        private bool _VenderConfirmed = true;
        private string _VenderJudgeResult = string.Empty;
        private string _VendorID = string.Empty;
        private string _VendorName = string.Empty;
        private int _Version;
        private int _VersionID;
        private double _Yield;
        private float roundOSRate;
        private float roundYield;
        private int swBinCount;

        public string AutoJudgeResult
        {
            get
            {
                return this._AutoJudgeResult;
            }
            set
            {
                this._AutoJudgeResult = value;
            }
        }

        public int Bin1
        {
            get
            {
                return this._Bin1;
            }
            set
            {
                this._Bin1 = value;
            }
        }

        public int Bin10
        {
            get
            {
                return this._Bin10;
            }
            set
            {
                this._Bin10 = value;
            }
        }

        public int Bin2
        {
            get
            {
                return this._Bin2;
            }
            set
            {
                this._Bin2 = value;
            }
        }

        public int Bin3
        {
            get
            {
                return this._Bin3;
            }
            set
            {
                this._Bin3 = value;
            }
        }

        public int Bin4
        {
            get
            {
                return this._Bin4;
            }
            set
            {
                this._Bin4 = value;
            }
        }

        public int Bin5
        {
            get
            {
                return this._Bin5;
            }
            set
            {
                this._Bin5 = value;
            }
        }

        public int Bin6
        {
            get
            {
                return this._Bin6;
            }
            set
            {
                this._Bin6 = value;
            }
        }

        public int Bin7
        {
            get
            {
                return this._Bin7;
            }
            set
            {
                this._Bin7 = value;
            }
        }

        public int Bin8
        {
            get
            {
                return this._Bin8;
            }
            set
            {
                this._Bin8 = value;
            }
        }

        public int Bin9
        {
            get
            {
                return this._Bin9;
            }
            set
            {
                this._Bin9 = value;
            }
        }

        public DateTime CompletionDate
        {
            get
            {
                return this._CompletionDate;
            }
            set
            {
                this._CompletionDate = value;
            }
        }

        public DateTime CreateTime
        {
            get
            {
                return this._CreateTime;
            }
            set
            {
                this._CreateTime = value;
            }
        }

        public int Damage
        {
            get
            {
                return this._Damage;
            }
            set
            {
                this._Damage = value;
            }
        }

        public string DeviceCode
        {
            get
            {
                return this._DeviceCode;
            }
            set
            {
                this._DeviceCode = value;
            }
        }

        public string DeviceName
        {
            get
            {
                return this._DeviceName;
            }
            set
            {
                this._DeviceName = value;
            }
        }

        public string Die1CP
        {
            get
            {
                return this._Die1CP;
            }
            set
            {
                this._Die1CP = value;
            }
        }

        public string Die1LotNO
        {
            get
            {
                return this._Die1LotNO;
            }
            set
            {
                this._Die1LotNO = value;
            }
        }

        public string Die2CP
        {
            get
            {
                return this._Die2CP;
            }
            set
            {
                this._Die2CP = value;
            }
        }

        public string Die2LotNO
        {
            get
            {
                return this._Die2LotNO;
            }
            set
            {
                this._Die2LotNO = value;
            }
        }

        public string Die3CP
        {
            get
            {
                return this._Die3CP;
            }
            set
            {
                this._Die3CP = value;
            }
        }

        public string Die3LotNO
        {
            get
            {
                return this._Die3LotNO;
            }
            set
            {
                this._Die3LotNO = value;
            }
        }

        public string Die4CP
        {
            get
            {
                return this._Die4CP;
            }
            set
            {
                this._Die4CP = value;
            }
        }

        public string Die4LotNO
        {
            get
            {
                return this._Die4LotNO;
            }
            set
            {
                this._Die4LotNO = value;
            }
        }

        public string HoldReason
        {
            get
            {
                return this._HoldReason;
            }
            set
            {
                this._HoldReason = value;
            }
        }

        public string ID
        {
            get
            {
                return this._ID;
            }
            set
            {
                this._ID = value;
            }
        }

        public string LastOperator
        {
            get
            {
                return this._LastOperator;
            }
            set
            {
                this._LastOperator = value;
            }
        }

        public string LBNO
        {
            get
            {
                return this._LBNO;
            }
            set
            {
                this._LBNO = value;
            }
        }

        public int Loss
        {
            get
            {
                return this._Loss;
            }
            set
            {
                this._Loss = value;
            }
        }

        public string LotID
        {
            get
            {
                return this._LotID;
            }
            set
            {
                this._LotID = value;
            }
        }

        public string LotNO
        {
            get
            {
                return this._LotNO;
            }
            set
            {
                this._LotNO = value;
            }
        }

        public string LotType
        {
            get
            {
                return this._LotType;
            }
            set
            {
                this._LotType = value;
            }
        }

        public bool ManualHold
        {
            get
            {
                return this._ManualHold;
            }
            set
            {
                this._ManualHold = value;
            }
        }

        public string NotificationType
        {
            get
            {
                return this._NotificationType;
            }
            set
            {
                this._NotificationType = value;
            }
        }

        public bool Opened
        {
            get
            {
                return this._Opened;
            }
            set
            {
                this._Opened = value;
            }
        }

        public DateTime OperateTime
        {
            get
            {
                return this._OperateTime;
            }
            set
            {
                this._OperateTime = value;
            }
        }

        public string OperatorID
        {
            get
            {
                return this._OperatorID;
            }
            set
            {
                this._OperatorID = value;
            }
        }

        public string OperatorName
        {
            get
            {
                return this._OperatorName;
            }
            set
            {
                this._OperatorName = value;
            }
        }

        public string OSRate
        {
            get
            {
                return this._OSRate;
            }
            set
            {
                this._OSRate = value;
            }
        }

        public bool OtherBinDispose
        {
            get
            {
                return this._OtherBinDispose;
            }
            set
            {
                this._OtherBinDispose = value;
            }
        }

        public int PEDispose
        {
            get
            {
                return this._PEDispose;
            }
            set
            {
                this._PEDispose = value;
            }
        }

        public string PEDisposeText
        {
            get
            {
                return this._PEDisposeText;
            }
            set
            {
                this._PEDisposeText = value;
            }
        }

        public string Platforms
        {
            get
            {
                return this._Platforms;
            }
            set
            {
                this._Platforms = value;
            }
        }

        public string PreviousRecordID
        {
            get
            {
                return this._PreviousRecordID;
            }
            set
            {
                this._PreviousRecordID = value;
            }
        }

        public int QADispose
        {
            get
            {
                return this._QADispose;
            }
            set
            {
                this._QADispose = value;
            }
        }

        public string QADisposeText
        {
            get
            {
                return this._QADisposeText;
            }
            set
            {
                this._QADisposeText = value;
            }
        }

        public int QtyIn
        {
            get
            {
                return this._QtyIn;
            }
            set
            {
                this._QtyIn = value;
            }
        }

        public string RecipientID
        {
            get
            {
                return this._RecipientID;
            }
            set
            {
                this._RecipientID = value;
            }
        }

        public int RecordState
        {
            get
            {
                return this._RecordState;
            }
            set
            {
                this._RecordState = value;
            }
        }

        public string RecordType
        {
            get
            {
                return this._RecordType;
            }
            set
            {
                this._RecordType = value;
            }
        }

        public int RetestTimes
        {
            get
            {
                return this._RetestTimes;
            }
            set
            {
                this._RetestTimes = value;
            }
        }

        public float RoundOSRate
        {
            get
            {
                return this.roundOSRate;
            }
            set
            {
                this.roundOSRate = value;
            }
        }

        public float RoundYield
        {
            get
            {
                return this.roundYield;
            }
            set
            {
                this.roundYield = value;
            }
        }

        public int SPRDDecision
        {
            get
            {
                return this._SPRDDecision;
            }
            set
            {
                this._SPRDDecision = value;
            }
        }

        public string SPRDDecisionText
        {
            get
            {
                return this._SPRDDecisionText;
            }
            set
            {
                this._SPRDDecisionText = value;
            }
        }

        public string Status
        {
            get
            {
                return this._Status;
            }
            set
            {
                this._Status = value;
            }
        }

        public string SubconLot
        {
            get
            {
                return this._SubconLot;
            }
            set
            {
                this._SubconLot = value;
            }
        }

        public int SwBinCount
        {
            get
            {
                return this.swBinCount;
            }
            set
            {
                this.swBinCount = value;
            }
        }

        public string TesterID
        {
            get
            {
                return this._TesterID;
            }
            set
            {
                this._TesterID = value;
            }
        }

        public string TestProgram
        {
            get
            {
                return this._TestProgram;
            }
            set
            {
                this._TestProgram = value;
            }
        }

        public DateTime UpdateTime
        {
            get
            {
                return this._UpdateTime;
            }
            set
            {
                this._UpdateTime = value;
            }
        }

        public bool VenderConfirmed
        {
            get
            {
                return this._VenderConfirmed;
            }
            set
            {
                this._VenderConfirmed = value;
            }
        }

        public string VenderJudgeResult
        {
            get
            {
                return this._VenderJudgeResult;
            }
            set
            {
                this._VenderJudgeResult = value;
            }
        }

        public string VendorID
        {
            get
            {
                return this._VendorID;
            }
            set
            {
                this._VendorID = value;
            }
        }

        public string VendorName
        {
            get
            {
                return this._VendorName;
            }
            set
            {
                this._VendorName = value;
            }
        }

        public int Version
        {
            get
            {
                return this._Version;
            }
            set
            {
                this._Version = value;
            }
        }

        public int VersionID
        {
            get
            {
                return this._VersionID;
            }
            set
            {
                this._VersionID = value;
            }
        }

        public double Yield
        {
            get
            {
                return this._Yield;
            }
            set
            {
                this._Yield = value;
            }
        }
    }
}

