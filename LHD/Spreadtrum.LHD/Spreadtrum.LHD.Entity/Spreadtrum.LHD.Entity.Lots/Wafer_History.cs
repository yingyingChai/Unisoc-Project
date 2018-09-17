using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spreadtrum.LHD.Entity.Lots
{
    public class Wafer_History
    {
        private string _ID;
        private string _TransformID;
        private string _WaferID;
        private int _Dispose;
        private string _UserID;
        private string _Comment;
        private DateTime _CreateTime;
        private bool _IsVendor;

        public string ID
        {
            get
            {
                return _ID;
            }

            set
            {
                _ID = value;
            }
        }

        public string TransformID
        {
            get
            {
                return _TransformID;
            }

            set
            {
                _TransformID = value;
            }
        }

        public string WaferID
        {
            get
            {
                return _WaferID;
            }

            set
            {
                _WaferID = value;
            }
        }

        public string UserID
        {
            get
            {
                return _UserID;
            }

            set
            {
                _UserID = value;
            }
        }

        public string Comment
        {
            get
            {
                return _Comment;
            }

            set
            {
                _Comment = value;
            }
        }

        public DateTime CreateTime
        {
            get
            {
                return _CreateTime;
            }

            set
            {
                _CreateTime = value;
            }
        }

        public bool IsVendor
        {
            get
            {
                return _IsVendor;
            }

            set
            {
                _IsVendor = value;
            }
        }

        public int Dispose
        {
            get
            {
                return _Dispose;
            }

            set
            {
                _Dispose = value;
            }
        }
    }

    public class VwWafer_History
    {
        private string _ID;
        private string _ProductName;
        private string _WaferCode;
        private string _LotId;
        private string _WaferID;
        private int _Dispose;
        private string _DisposeText;
        private string _UserID;
        private string _Comment;
        private DateTime _CreateTime;
        private bool _IsVendor;

        public string ID
        {
            get
            {
                return _ID;
            }

            set
            {
                _ID = value;
            }
        }

        public string ProductName
        {
            get
            {
                return _ProductName;
            }

            set
            {
                _ProductName = value;
            }
        }

        public string WaferCode
        {
            get
            {
                return _WaferCode;
            }

            set
            {
                _WaferCode = value;
            }
        }

        public string LotId
        {
            get
            {
                return _LotId;
            }

            set
            {
                _LotId = value;
            }
        }

        public string WaferID
        {
            get
            {
                return _WaferID;
            }

            set
            {
                _WaferID = value;
            }
        }

        public int Dispose
        {
            get
            {
                return _Dispose;
            }

            set
            {
                _Dispose = value;
            }
        }

        public string DisposeText
        {
            get
            {
                return _DisposeText;
            }

            set
            {
                _DisposeText = value;
            }
        }

        public string UserID
        {
            get
            {
                return _UserID;
            }

            set
            {
                _UserID = value;
            }
        }

        public string Comment
        {
            get
            {
                return _Comment;
            }

            set
            {
                _Comment = value;
            }
        }

        public DateTime CreateTime
        {
            get
            {
                return _CreateTime;
            }

            set
            {
                _CreateTime = value;
            }
        }

        public bool IsVendor
        {
            get
            {
                return _IsVendor;
            }

            set
            {
                _IsVendor = value;
            }
        }
    }
    public class HistoryQuery
    {
        private string productName = string.Empty;
        private string waferCode = string.Empty;
        private string lotId = string.Empty;
        private string waferID = string.Empty;
        private int dispose = 0;
        private string disposeText = string.Empty;
        private string userID = string.Empty;
        private string comment = string.Empty;
        private string createTime = string.Empty;
        private string orderBy = string.Empty;
        private bool orderDesc = false;
        public string ProductName
        {
            get
            {
                return productName;
            }

            set
            {
                productName = value;
            }
        }

        public string WaferCode
        {
            get
            {
                return waferCode;
            }

            set
            {
                waferCode = value;
            }
        }

        public string LotId
        {
            get
            {
                return lotId;
            }

            set
            {
                lotId = value;
            }
        }

        public string WaferID
        {
            get
            {
                return waferID;
            }

            set
            {
                waferID = value;
            }
        }

        public int Dispose
        {
            get
            {
                return dispose;
            }

            set
            {
                dispose = value;
            }
        }

        public string UserID
        {
            get
            {
                return userID;
            }

            set
            {
                userID = value;
            }
        }

        public string Comment
        {
            get
            {
                return comment;
            }

            set
            {
                comment = value;
            }
        }

        public string CreateTime
        {
            get
            {
                return createTime;
            }

            set
            {
                createTime = value;
            }
        }

        public string DisposeText
        {
            get
            {
                return disposeText;
            }

            set
            {
                disposeText = value;
            }
        }

        public string OrderBy
        {
            get
            {
                return orderBy;
            }

            set
            {
                orderBy = value;
            }
        }

        public bool OrderDesc
        {
            get
            {
                return orderDesc;
            }

            set
            {
                orderDesc = value;
            }
        }
    }
}
