namespace Spreadtrum.LHD.Entity.Lots
{
    using System;

    public class LotQuery
    {
        private string _LBNO = string.Empty;
        private string _OperateType = string.Empty;
        private int _OtherBinDispose;
        private int _PEDispose = -1;
        private int _QADispose = -1;
        private int _SPRDDecision = -1;
        private Spreadtrum.LHD.Entity.Lots.AutoJudgeResult autoJudgeResult;
        private string bin1 = string.Empty;
        private string bin10 = string.Empty;
        private string bin2 = string.Empty;
        private string bin3 = string.Empty;
        private string bin4 = string.Empty;
        private string bin5 = string.Empty;
        private string bin6 = string.Empty;
        private string bin7 = string.Empty;
        private string bin8 = string.Empty;
        private string bin9 = string.Empty;
        private string completionDate = string.Empty;
        private string currentUser = string.Empty;
        private string damage = string.Empty;
        private string deviceCode = string.Empty;
        private string deviceName = string.Empty;
        private string die1CP = string.Empty;
        private string die1LotNo = string.Empty;
        private string die2CP = string.Empty;
        private string die2LotNo = string.Empty;
        private string die3CP = string.Empty;
        private string die3LotNo = string.Empty;
        private string die4CP = string.Empty;
        private string die4LotNo = string.Empty;
        private bool eqaLotsOnly;
        private string holdReason = string.Empty;
        private string loss = string.Empty;
        private string lotNO = string.Empty;
        private string lotType;
        private int manualHold = -1;
        private bool newCommentOnly;
        private string osat;
        private string osRate = string.Empty;
        private string platform = string.Empty;
        private int qtyIn = -1;
        private int retestTimes = -1;
        private string stage = string.Empty;
        private string status = string.Empty;
        private string subconLot = string.Empty;
        private string testerID = string.Empty;
        private string testProgram = string.Empty;
        private bool waitForConfirmOnly;
        private bool waitForDisposeOnly;
        private bool waitForOtherBinDispose;
        private string yield = string.Empty;

        public Spreadtrum.LHD.Entity.Lots.AutoJudgeResult AutoJudgeResult
        {
            get
            {
                return this.autoJudgeResult;
            }
            set
            {
                this.autoJudgeResult = value;
            }
        }

        public string Bin1
        {
            get
            {
                return this.bin1;
            }
            set
            {
                this.bin1 = value;
            }
        }

        public string Bin10
        {
            get
            {
                return this.bin10;
            }
            set
            {
                this.bin10 = value;
            }
        }

        public string Bin2
        {
            get
            {
                return this.bin2;
            }
            set
            {
                this.bin2 = value;
            }
        }

        public string Bin3
        {
            get
            {
                return this.bin3;
            }
            set
            {
                this.bin3 = value;
            }
        }

        public string Bin4
        {
            get
            {
                return this.bin4;
            }
            set
            {
                this.bin4 = value;
            }
        }

        public string Bin5
        {
            get
            {
                return this.bin5;
            }
            set
            {
                this.bin5 = value;
            }
        }

        public string Bin6
        {
            get
            {
                return this.bin6;
            }
            set
            {
                this.bin6 = value;
            }
        }

        public string Bin7
        {
            get
            {
                return this.bin7;
            }
            set
            {
                this.bin7 = value;
            }
        }

        public string Bin8
        {
            get
            {
                return this.bin8;
            }
            set
            {
                this.bin8 = value;
            }
        }

        public string Bin9
        {
            get
            {
                return this.bin9;
            }
            set
            {
                this.bin9 = value;
            }
        }

        public string CompletionDate
        {
            get
            {
                return this.completionDate;
            }
            set
            {
                this.completionDate = value;
            }
        }

        public string CurrentUser
        {
            get
            {
                return this.currentUser;
            }
            set
            {
                this.currentUser = value;
            }
        }

        public string Damage
        {
            get
            {
                return this.damage;
            }
            set
            {
                this.damage = value;
            }
        }

        public string DeviceCode
        {
            get
            {
                return this.deviceCode;
            }
            set
            {
                this.deviceCode = value;
            }
        }

        public string DeviceName
        {
            get
            {
                return this.deviceName;
            }
            set
            {
                this.deviceName = value;
            }
        }

        public string Die1CP
        {
            get
            {
                return this.die1CP;
            }
            set
            {
                this.die1CP = value;
            }
        }

        public string Die1LotNo
        {
            get
            {
                return this.die1LotNo;
            }
            set
            {
                this.die1LotNo = value;
            }
        }

        public string Die2CP
        {
            get
            {
                return this.die2CP;
            }
            set
            {
                this.die2CP = value;
            }
        }

        public string Die2LotNo
        {
            get
            {
                return this.die2LotNo;
            }
            set
            {
                this.die2LotNo = value;
            }
        }

        public string Die3CP
        {
            get
            {
                return this.die3CP;
            }
            set
            {
                this.die3CP = value;
            }
        }

        public string Die3LotNo
        {
            get
            {
                return this.die3LotNo;
            }
            set
            {
                this.die3LotNo = value;
            }
        }

        public string Die4CP
        {
            get
            {
                return this.die4CP;
            }
            set
            {
                this.die4CP = value;
            }
        }

        public string Die4LotNo
        {
            get
            {
                return this.die4LotNo;
            }
            set
            {
                this.die4LotNo = value;
            }
        }

        public bool EqaLotsOnly
        {
            get
            {
                return this.eqaLotsOnly;
            }
            set
            {
                this.eqaLotsOnly = value;
            }
        }

        public string HoldReason
        {
            get
            {
                return this.holdReason;
            }
            set
            {
                this.holdReason = value;
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

        public string Loss
        {
            get
            {
                return this.loss;
            }
            set
            {
                this.loss = value;
            }
        }

        public string LotNO
        {
            get
            {
                return this.lotNO;
            }
            set
            {
                this.lotNO = value;
            }
        }

        public string LotType
        {
            get
            {
                return this.lotType;
            }
            set
            {
                this.lotType = value;
            }
        }

        public int ManualHold
        {
            get
            {
                return this.manualHold;
            }
            set
            {
                this.manualHold = value;
            }
        }

        public bool NewCommentOnly
        {
            get
            {
                return this.newCommentOnly;
            }
            set
            {
                this.newCommentOnly = value;
            }
        }

        public string OperateType
        {
            get
            {
                return this._OperateType;
            }
            set
            {
                this._OperateType = value;
            }
        }

        public string Osat
        {
            get
            {
                return this.osat;
            }
            set
            {
                this.osat = value;
            }
        }

        public string OsRate
        {
            get
            {
                return this.osRate;
            }
            set
            {
                this.osRate = value;
            }
        }

        public int OtherBinDispose
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

        public string Platform
        {
            get
            {
                return this.platform;
            }
            set
            {
                this.platform = value;
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

        public int QtyIn
        {
            get
            {
                return this.qtyIn;
            }
            set
            {
                this.qtyIn = value;
            }
        }

        public int RetestTimes
        {
            get
            {
                return this.retestTimes;
            }
            set
            {
                this.retestTimes = value;
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

        public string Stage
        {
            get
            {
                return this.stage;
            }
            set
            {
                this.stage = value;
            }
        }

        public string Status
        {
            get
            {
                return this.status;
            }
            set
            {
                this.status = value;
            }
        }

        public string SubconLot
        {
            get
            {
                return this.subconLot;
            }
            set
            {
                this.subconLot = value;
            }
        }

        public string TesterID
        {
            get
            {
                return this.testerID;
            }
            set
            {
                this.testerID = value;
            }
        }

        public string TestProgram
        {
            get
            {
                return this.testProgram;
            }
            set
            {
                this.testProgram = value;
            }
        }

        public bool WaitForConfirmOnly
        {
            get
            {
                return this.waitForConfirmOnly;
            }
            set
            {
                this.waitForConfirmOnly = value;
            }
        }

        public bool WaitForDisposeOnly
        {
            get
            {
                return this.waitForDisposeOnly;
            }
            set
            {
                this.waitForDisposeOnly = value;
            }
        }

        public bool WaitForOtherBinDispose
        {
            get
            {
                return this.waitForOtherBinDispose;
            }
            set
            {
                this.waitForOtherBinDispose = value;
            }
        }

        public string Yield
        {
            get
            {
                return this.yield;
            }
            set
            {
                this.yield = value;
            }
        }
    }
}

