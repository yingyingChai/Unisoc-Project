using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spreadtrum.LHD.Entity.Lots;
using Spreadtrum.LHD.DAL.Lots;

namespace Spreadtrum.LHD.Business
{
   public class VendorsService
    {
        private VendorsGateway dal = new VendorsGateway();
        public IList<Vendors> VendorList(string vendorType)
        {
            return this.dal.VendorList(vendorType);
        }
    }
}
