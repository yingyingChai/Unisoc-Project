namespace Spreadtrum.LHD.Entity.Lots
{
    using System;

    public class SWBin
    {
        private int _Code;
        private DateTime _CreateTime;
        private string _Defect = string.Empty;
        private double _FailedBinPercent;
        private float _FailRate;
        private string _ID = string.Empty;
        private string _IsPassed;
        private string _IsTriggerd;
        private string _LastOperator = string.Empty;
        private float _Limited;
        private string _LotID = string.Empty;
        private int _Qty;
        private int _RecordState;

        public int Code
        {
            get
            {
                return this._Code;
            }
            set
            {
                this._Code = value;
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

        public string Defect
        {
            get
            {
                return this._Defect;
            }
            set
            {
                this._Defect = value;
            }
        }

        public double FailedBinPercent
        {
            get
            {
                return this._FailedBinPercent;
            }
            set
            {
                this._FailedBinPercent = value;
            }
        }

        public float FailRate
        {
            get
            {
                return this._FailRate;
            }
            set
            {
                this._FailRate = value;
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

        public string IsPassed
        {
            get
            {
                return this._IsPassed;
            }
            set
            {
                this._IsPassed = value;
            }
        }

        public string IsTriggerd
        {
            get
            {
                return this._IsTriggerd;
            }
            set
            {
                this._IsTriggerd = value;
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

        public float Limited
        {
            get
            {
                return this._Limited;
            }
            set
            {
                this._Limited = value;
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

        public int Qty
        {
            get
            {
                return this._Qty;
            }
            set
            {
                this._Qty = value;
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
    }
}

