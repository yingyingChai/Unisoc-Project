namespace Spreadtrum.LHD.Entity.Systems
{
    using System;

    public class Notification
    {
        private DateTime _CreateTime;
        private string _EmailID = string.Empty;
        private string _LastOperator = string.Empty;
        private string _LotID = string.Empty;
        private string _Message = string.Empty;
        private string _MessageID = string.Empty;
        private NotificationTypes _NotificationType;
        private bool _Opened = true;
        private DateTime _ReadTime;
        private string _RecipientID = string.Empty;
        private int _RecordState;
        private DateTime _UpdateTime;

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

        public string EmailID
        {
            get
            {
                return this._EmailID;
            }
            set
            {
                this._EmailID = value;
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

        public string Message
        {
            get
            {
                return this._Message;
            }
            set
            {
                this._Message = value;
            }
        }

        public string MessageID
        {
            get
            {
                return this._MessageID;
            }
            set
            {
                this._MessageID = value;
            }
        }

        public NotificationTypes NotificationType
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

        public DateTime ReadTime
        {
            get
            {
                return this._ReadTime;
            }
            set
            {
                this._ReadTime = value;
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

