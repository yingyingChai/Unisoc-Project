using KaYi.FileSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spreadtrum.LHD.Entity.Lots
{
  public  class TranformedComment
    {
        private string _CommentID;
        private string _CommentText;
        private string _Operator;
        private string _LotID;
        private int _RecordState;
        private object _CreateTime;
        private object _UpdateTime;
        private string _LastOperator;
        private CommentTypes _CommentType=CommentTypes.NotSpecified;
        private string _OperatorName;
        private string _OperatorRole;
        private string _OperatorBUName;
        private string _OperatorEmail;
        private bool _Internal;
        private IList<File> attachments;

        public string CommentID
        {
            get
            {
                return _CommentID;
            }

            set
            {
                _CommentID = value;
            }
        }

        public string CommentText
        {
            get
            {
                return _CommentText;
            }

            set
            {
                _CommentText = value;
            }
        }

        public string Operator
        {
            get
            {
                return _Operator;
            }

            set
            {
                _Operator = value;
            }
        }

        public string LotID
        {
            get
            {
                return _LotID;
            }

            set
            {
                _LotID = value;
            }
        }

        public int RecordState
        {
            get
            {
                return _RecordState;
            }

            set
            {
                _RecordState = value;
            }
        }

        public object CreateTime
        {
            get
            {
                if (_CreateTime == DBNull.Value)
                {
                    return null;
                }
                    return _CreateTime;
            }

            set
            {
                if (_CreateTime == DBNull.Value)
                {
                    _CreateTime = null;
                }else
                {
                    _CreateTime = value;

                }
            }
        }

        public object UpdateTime
        {
            get
            {
                if (_UpdateTime == DBNull.Value)
                {
                    return null;
                }
                    return _UpdateTime;
            }

            set
            {
                if (_UpdateTime == DBNull.Value)
                {
                    _UpdateTime = null;
                }else
                { _UpdateTime = value; }
                
            }
        }

        public string LastOperator
        {
            get
            {
                return _LastOperator;
            }

            set
            {
                _LastOperator = value;
            }
        }

        public CommentTypes CommentType
        {
            get
            {
                return _CommentType;
            }

            set
            {
                _CommentType = value;
            }
        }

        public string OperatorName
        {
            get
            {
                return _OperatorName;
            }

            set
            {
                _OperatorName = value;
            }
        }

        public string OperatorRole
        {
            get
            {
                return _OperatorRole;
            }

            set
            {
                _OperatorRole = value;
            }
        }

        public string OperatorBUName
        {
            get
            {
                return _OperatorBUName;
            }

            set
            {
                _OperatorBUName = value;
            }
        }

        public string OperatorEmail
        {
            get
            {
                return _OperatorEmail;
            }

            set
            {
                _OperatorEmail = value;
            }
        }

        public bool Internal
        {
            get
            {
                return _Internal;
            }

            set
            {
                _Internal = value;
            }
        }

        public IList<File> Attachments
        {
            get
            {
                return attachments;
            }
            set
            {
                attachments = value;
            }
        }
    }
}
