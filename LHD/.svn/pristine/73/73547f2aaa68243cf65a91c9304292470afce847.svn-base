using Spreadtrum.LHD.DAL.Lots;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spreadtrum.LHD.Entity.Lots;
namespace Spreadtrum.LHD.Business
{
   public class Wafer_HistoryService
    {
        private VWWaferHistoryGateway way = new VWWaferHistoryGateway();
        public IList<VwWafer_History> HistoryList(HistoryQuery query, int pageSize, int pageIndex, out int recordCount)
        {
            return this.way.HistoryList(query, pageSize, pageIndex, out recordCount);
        }
    }
}
