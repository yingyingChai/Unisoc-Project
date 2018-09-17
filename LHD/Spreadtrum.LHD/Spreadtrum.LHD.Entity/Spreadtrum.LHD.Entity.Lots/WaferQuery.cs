using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spreadtrum.LHD.Entity.Lots
{
    public  class WaferQuery
    {
        private string searchType = string.Empty;
        private string productName=string.Empty;
        private string waferCode = string.Empty;
        private string lotId = string.Empty;
        private string waferID = string.Empty;
        private string transformID = string.Empty;
        private int pEDispose=-1;
        private string pEComment = string.Empty;
        private int qADispose=-1;
        private string qAComment= string.Empty;
        private int sPRDDecision=-1;
        private string vendorComment = string.Empty;
        private string osat = string.Empty;
        private int status = -1;
        private string holdReason = string.Empty;
        private string yield = string.Empty;
        private string completionDate = string.Empty;
        private string orderBy = string.Empty;
        private bool orderDesc = false;
        private int lastDays = 1;
        public string SearchType
        {
            get
            {
                return searchType;
            }

            set
            {
                searchType = value;
            }
        }

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

        public string TransformID
        {
            get
            {
                return transformID;
            }

            set
            {
                transformID = value;
            }
        }

        public int PEDispose
        {
            get
            {
                return pEDispose;
            }

            set
            {
                pEDispose = value;
            }
        }

        public string PEComment
        {
            get
            {
                return pEComment;
            }

            set
            {
                pEComment = value;
            }
        }

        public int QADispose
        {
            get
            {
                return qADispose;
            }

            set
            {
                qADispose = value;
            }
        }

        public string QAComment
        {
            get
            {
                return qAComment;
            }

            set
            {
                qAComment = value;
            }
        }

        public int SPRDDecision
        {
            get
            {
                return sPRDDecision;
            }

            set
            {
                sPRDDecision = value;
            }
        }

       

        public string Osat
        {
            get
            {
                return osat;
            }

            set
            {
                osat = value;
            }
        }

        public int Status
        {
            get
            {
                return status;
            }

            set
            {
                status = value;
            }
        }

        public string HoldReason
        {
            get
            {
                return holdReason;
            }

            set
            {
                holdReason = value;
            }
        }

        public string Yield
        {
            get
            {
                return yield;
            }

            set
            {
                yield = value;
            }
        }

        public string CompletionDate
        {
            get
            {
                return completionDate;
            }

            set
            {
                completionDate = value;
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

        public int LastDays
        {
            get
            {
                return lastDays;
            }

            set
            {
                lastDays = value;
            }
        }


        public string VendorComment
        {
            get
            {
                return vendorComment;
            }

            set
            {
                vendorComment = value;
            }
        }
    }

    public class DetailWaferQuery
    {
        private string transformID = string.Empty;
        private string waferID = string.Empty;
        private string program = string.Empty;
        private string startTime = string.Empty;
        private int totalDieCount = -1;
        private string yield = string.Empty;
        private string orderBy = string.Empty;
        private bool orderDesc = false;

        public string TransformID
        {
            get
            {
                return transformID;
            }

            set
            {
                transformID = value;
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

        public string Program
        {
            get
            {
                return program;
            }

            set
            {
                program = value;
            }
        }

        public string StartTime
        {
            get
            {
                return startTime;
            }

            set
            {
                startTime = value;
            }
        }

        public int TotalDieCount
        {
            get
            {
                return totalDieCount;
            }

            set
            {
                totalDieCount = value;
            }
        }

        public string Yield
        {
            get
            {
                return yield;
            }

            set
            {
                yield = value;
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

    public enum WaferSelection
    {
        Hold = 1,
        Release = 2,
        Ink = 3,
        Split = 4,
        RMA=5,
        Scrap = 6,
        Others = 7,
    }

    public enum WaferStatus
    {
        WaitQA = 1,
        WaitPE = 2,
        WaitQAPE = 3,
        WaitVendor = 4,
        WaitPEVendor = 5,
        Close = 6
    }

    public enum OperationStatus
    {
        Hold = 1,
        Release = 2
    }
    public class WaferHelper
    {
        public static string WaferSelectionDes(int selection)
        {
            string str = "";
            switch (selection)
            {
                case (int)WaferSelection.Hold:
                    str = "Hold";
                    break;
                case (int)WaferSelection.Release:
                    str = "Release";
                    break;
                case (int)WaferSelection.Ink:
                    str = "Ink&rls";
                    break;
                case (int)WaferSelection.Split:
                    str = "Split&rls";
                    break;
                case (int)WaferSelection.RMA:
                    str = "RMA";
                    break;
                case (int)WaferSelection.Scrap:
                    str = "Scrap";
                    break;
                case (int)WaferSelection.Others:
                    str = "Others";
                    break;
                default:
                    str = "";
                    break;
            }
            return str;
        }
        public static string waferStatusDes(int status)
        {
            string des = "";
            switch (status)
            {
                case (int)WaferStatus.Close:
                    des = "Close";
                    break;
                case (int)WaferStatus.WaitPE:
                    des = "Wait PE";
                    break;
                case (int)WaferStatus.WaitQA:
                    des = "Wait QA";
                    break;
                case (int)WaferStatus.WaitQAPE:
                    des = "Wait QA&PE";
                    break;
                case (int)WaferStatus.WaitVendor:
                    des = "Wait Vendor";
                    break;
                case (int)WaferStatus.WaitPEVendor:
                    des = "Wait PE&Vendor";
                    break;
            }
            return des;
        }
    }
}
