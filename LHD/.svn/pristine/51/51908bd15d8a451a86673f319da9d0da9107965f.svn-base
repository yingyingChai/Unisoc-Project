using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spreadtrum.LHD.DAL.Lots;
using Spreadtrum.LHD.Entity.Lots;
namespace Spreadtrum.LHD.Business
{
   public class Wafer_sbinService
    {
        private Wafer_SbinGateway dal = new Wafer_SbinGateway();
        public IList<Wafer_Sbin> GetSbinTextByLotId(string lotid)
        {
            return this.dal.GetSbinTextByLotId(lotid);
        }
        public IList<Wafer_Sbin> GetWaferSbin(string lotid, string waferid)
        {
            return this.dal.GetWaferSbin(lotid,waferid);
        }
        public int SaveSBin(Wafer_Sbin sbin)
        {
            return this.dal.SaveSBin(sbin);
        }
    }
}
