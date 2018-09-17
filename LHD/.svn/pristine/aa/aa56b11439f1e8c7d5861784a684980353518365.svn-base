using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KaYi.Database;
using Spreadtrum.LHD.Entity.Lots;
namespace Spreadtrum.LHD.DAL.Lots
{
  public  class VendorsGateway
    {
        private DALGateway<Vendors> dbGateway = new DALGateway<Vendors>();
        public VendorsGateway()
        {
            this.dbGateway.LoadSchema("VENDORS");
        }
        public IList<Vendors> VendorList(string vendorType)
        {
            Conditions condition = new Conditions();
            condition.ConditionExpressions.Add(new Condition("VendorType", Operator.EqualTo, vendorType));
            OrderExpression orderExpression = new OrderExpression("VendorID", false);
            return this.dbGateway.getRecords(999, condition, orderExpression);
        }
    }
}
