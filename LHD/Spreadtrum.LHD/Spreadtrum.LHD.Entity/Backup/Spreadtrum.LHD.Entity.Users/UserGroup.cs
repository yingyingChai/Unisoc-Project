namespace Spreadtrum.LHD.Entity.Users
{
    using System;

    public class UserGroup
    {
        private string _AccountID = string.Empty;
        private DateTime _CreateTime;
        private string _GroupID = string.Empty;
        private DateTime _JoinTime;
        private string _LastOperator = string.Empty;
        private DateTime _QuitTime;
        private int _RecordState;
        private DateTime _UpdateTime;
        private string _UserGroupID = string.Empty;

        public string AccountID
        {
            get
            {
                return this._AccountID;
            }
            set
            {
                this._AccountID = value;
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

        public string GroupID
        {
            get
            {
                return this._GroupID;
            }
            set
            {
                this._GroupID = value;
            }
        }

        public DateTime JoinTime
        {
            get
            {
                return this._JoinTime;
            }
            set
            {
                this._JoinTime = value;
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

        public DateTime QuitTime
        {
            get
            {
                return this._QuitTime;
            }
            set
            {
                this._QuitTime = value;
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

        public string UserGroupID
        {
            get
            {
                return this._UserGroupID;
            }
            set
            {
                this._UserGroupID = value;
            }
        }
    }
}

