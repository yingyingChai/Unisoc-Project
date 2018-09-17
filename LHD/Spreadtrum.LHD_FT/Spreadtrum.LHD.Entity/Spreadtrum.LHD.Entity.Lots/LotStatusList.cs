namespace Spreadtrum.LHD.Entity.Lots
{
    using System;

    public class LotStatusList
    {
        private bool _OtherBinDispose = true;
        private bool _OtherBinDisposeConfirmed = true;
        private int _PEDispose;
        private int _QADispose;
        private int _SPRDDecision;
        private string _Status = string.Empty;
        private string _StatusID = string.Empty;
        private bool _VenderConfirmed = true;

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

        public bool OtherBinDisposeConfirmed
        {
            get
            {
                return this._OtherBinDisposeConfirmed;
            }
            set
            {
                this._OtherBinDisposeConfirmed = value;
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

        public string StatusID
        {
            get
            {
                return this._StatusID;
            }
            set
            {
                this._StatusID = value;
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
    }
}

