using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spreadtrum.LHD.Entity.Lots
{
   public class Lot_TransformedQuery
    {

        private string searchType = string.Empty;
        private string productName = string.Empty;
        private string waferCode = string.Empty;
        private string osat = string.Empty;
        private string lotId = string.Empty;
        private int status=-1;
        private string holdReason=string.Empty;
        private int wfCount= -1;
        private string yield = string.Empty;
        private string completionDate = string.Empty;
        private string orderBy = string.Empty;
        private bool orderDesc=false;
        private int lastDays =1;
        private int outClose = 0;
        private int operatorStatus = -1;
        private int peVendorState = 0;
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

        public int WfCount
        {
            get
            {
                return wfCount;
            }

            set
            {
                wfCount = value;
            }
        }

        public int OutClose
        {
            get
            {
                return outClose;
            }

            set
            {
                outClose = value;
            }
        }

        public int OperatorStatus
        {
            get
            {
                return operatorStatus;
            }

            set
            {
                operatorStatus = value;
            }
        }

        public int PeVendorState
        {
            get
            {
                return peVendorState;
            }

            set
            {
                peVendorState = value;
            }
        }
    }
   public enum LotAutoJudgement
   {
        hold,
        normal,
   }
   
}
