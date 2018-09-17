namespace Spreadtrum.LHD.Entity.Lots
{
    using KaYi.FileSystem.Model;
    using System;
    using System.Collections.Generic;

    public class Comment
    {
        private string _CommentID = string.Empty;
        private string _CommentText = string.Empty;
        private CommentTypes _CommentType = CommentTypes.NotSpecified;
        private DateTime _CreateTime;
        private string _CreateTimeStr = string.Empty;
        private int _Dispose;
        private string _DisposeText = string.Empty;
        private bool _Internal;
        private string _LastOperator = string.Empty;
        private string _LotID = string.Empty;
        private string _Operator = string.Empty;
        private string _OperatorBUName = string.Empty;
        private string _OperatorChineseName = string.Empty;
        private string _OperatorEmail = string.Empty;
        private string _OperatorName = string.Empty;
        private string _OperatorRole = string.Empty;
        private string _OperatorRoleText = string.Empty;
        private bool _OtherBinDispose;
        private int _RecordState;
        private DateTime _UpdateTime;
        private IList<File> attachments;
        private string disposeStyle = string.Empty;

        public IList<File> Attachments
        {
            get
            {
                return this.attachments;
            }
            set
            {
                this.attachments = value;
            }
        }

        public string CommentID
        {
            get
            {
                return this._CommentID;
            }
            set
            {
                this._CommentID = value;
            }
        }

        public string CommentText
        {
            get
            {
                return this._CommentText;
            }
            set
            {
                this._CommentText = value;
            }
        }

        public CommentTypes CommentType
        {
            get
            {
                return this._CommentType;
            }
            set
            {
                this._CommentType = value;
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

        public string CreateTimeStr
        {
            get
            {
                return this._CreateTime.ToString("yyyy-MM-dd HH:mm:ss");
            }
        }

        public int Dispose
        {
            get
            {
                return this._Dispose;
            }
            set
            {
                this._Dispose = value;
            }
        }

        public string DisposeStyle
        {
            get
            {
                bool flag1 = this.DisposeText.ToUpper() == "RELEASE";
                return "";
            }
        }

        public string DisposeText
        {
            get
            {
                return this._DisposeText;
            }
            set
            {
                this._DisposeText = value;
            }
        }

        public bool Internal
        {
            get
            {
                return this._Internal;
            }
            set
            {
                this._Internal = value;
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

        public string Operator
        {
            get
            {
                return this._Operator;
            }
            set
            {
                this._Operator = value;
            }
        }

        public string OperatorBUName
        {
            get
            {
                return this._OperatorBUName;
            }
            set
            {
                this._OperatorBUName = value;
            }
        }

        public string OperatorChineseName
        {
            get
            {
                return this._OperatorChineseName;
            }
            set
            {
                this._OperatorChineseName = value;
            }
        }

        public string OperatorEmail
        {
            get
            {
                return this._OperatorEmail;
            }
            set
            {
                this._OperatorEmail = value;
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

        public string OperatorRole
        {
            get
            {
                return this._OperatorRole;
            }
            set
            {
                this._OperatorRole = value;
            }
        }

        public string OperatorRoleText
        {
            get
            {
                return this._OperatorRoleText;
            }
            set
            {
                this._OperatorRoleText = value;
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
    }
}

