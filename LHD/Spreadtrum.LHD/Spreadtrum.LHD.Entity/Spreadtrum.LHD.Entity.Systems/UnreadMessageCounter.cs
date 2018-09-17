namespace Spreadtrum.LHD.Entity.Systems
{
    using System;

    public class UnreadMessageCounter
    {
        private string _LotID = string.Empty;
        private string _NotificationType = string.Empty;
        private string _RecipientID = string.Empty;
        private int _UnreadMessage;

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

        public int UnreadMessage
        {
            get
            {
                return this._UnreadMessage;
            }
            set
            {
                this._UnreadMessage = value;
            }
        }
    }
}

