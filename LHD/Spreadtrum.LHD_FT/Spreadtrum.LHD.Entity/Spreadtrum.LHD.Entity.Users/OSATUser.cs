namespace Spreadtrum.LHD.Entity.Users
{
    using System;

    public class OSATUser
    {
        private int _BDWorkflow;
        private int _BomWorkflow;
        private int _CID;
        private int _ComplainWorkflow;
        private int _EQALots;
        private int _IsUse;
        private int _LotWorkflow;
        private int _MappingFileWorkflow;
        private int _MarkingWorkflow;
        private string _MD5CID = string.Empty;
        private int _NewComments;
        private string _SubconSite = string.Empty;
        private int _SubstrateWorkflow;
        private string _SupID = string.Empty;
        private string _SupMail = string.Empty;
        private string _SupRealUser = string.Empty;
        private string _SupUserName = string.Empty;
        private int _TestWorkflow;
        private int _WaitForConfirm;

        public int BDWorkflow
        {
            get
            {
                return this._BDWorkflow;
            }
            set
            {
                this._BDWorkflow = value;
            }
        }

        public int BomWorkflow
        {
            get
            {
                return this._BomWorkflow;
            }
            set
            {
                this._BomWorkflow = value;
            }
        }

        public int CID
        {
            get
            {
                return this._CID;
            }
            set
            {
                this._CID = value;
            }
        }

        public int ComplainWorkflow
        {
            get
            {
                return this._ComplainWorkflow;
            }
            set
            {
                this._ComplainWorkflow = value;
            }
        }

        public int EQALots
        {
            get
            {
                return this._EQALots;
            }
            set
            {
                this._EQALots = value;
            }
        }

        public int IsUse
        {
            get
            {
                return this._IsUse;
            }
            set
            {
                this._IsUse = value;
            }
        }

        public int LotWorkflow
        {
            get
            {
                return this._LotWorkflow;
            }
            set
            {
                this._LotWorkflow = value;
            }
        }

        public int MappingFileWorkflow
        {
            get
            {
                return this._MappingFileWorkflow;
            }
            set
            {
                this._MappingFileWorkflow = value;
            }
        }

        public int MarkingWorkflow
        {
            get
            {
                return this._MarkingWorkflow;
            }
            set
            {
                this._MarkingWorkflow = value;
            }
        }

        public string MD5CID
        {
            get
            {
                return this._MD5CID;
            }
            set
            {
                this._MD5CID = value;
            }
        }

        public int NewComments
        {
            get
            {
                return this._NewComments;
            }
            set
            {
                this._NewComments = value;
            }
        }

        public string SubconSite
        {
            get
            {
                return this._SubconSite;
            }
            set
            {
                this._SubconSite = value;
            }
        }

        public int SubstrateWorkflow
        {
            get
            {
                return this._SubstrateWorkflow;
            }
            set
            {
                this._SubstrateWorkflow = value;
            }
        }

        public string SupID
        {
            get
            {
                return this._SupID;
            }
            set
            {
                this._SupID = value;
            }
        }

        public string SupMail
        {
            get
            {
                return this._SupMail;
            }
            set
            {
                this._SupMail = value;
            }
        }

        public string SupRealUser
        {
            get
            {
                return this._SupRealUser;
            }
            set
            {
                this._SupRealUser = value;
            }
        }

        public string SupUserName
        {
            get
            {
                return this._SupUserName;
            }
            set
            {
                this._SupUserName = value;
            }
        }

        public int TestWorkflow
        {
            get
            {
                return this._TestWorkflow;
            }
            set
            {
                this._TestWorkflow = value;
            }
        }

        public int WaitForConfirm
        {
            get
            {
                return this._WaitForConfirm;
            }
            set
            {
                this._WaitForConfirm = value;
            }
        }
    }
}

