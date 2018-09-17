using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KaYi.Database;
using Spreadtrum.LHD.Entity.Lots;
namespace Spreadtrum.LHD.DAL.Lots
{
    public class Wafer_SbinGateway
    {
        private DALGateway<Wafer_Sbin> dbGateway = new DALGateway<Wafer_Sbin>();
        public Wafer_SbinGateway()
        {
            this.dbGateway.LoadSchema("WAFER_SBIN");
        }
        public IList<Wafer_Sbin> GetSbinTextByLotId(string lotid)
        {
            string sql = "select * from WAFER_SBIN where lotid='"+lotid+"' and ID in(SELECT MIN(ID) FROM dbo.WAFER_SBIN where lotid='"+ lotid + "' GROUP BY SbinText ) order by sort";
            return this.dbGateway.getRecordsBySQLStatement(sql,0,9999);
            //Conditions conditions = new Conditions();
            //conditions.ConditionExpressions.Add(new Condition("LotID", Operator.EqualTo,lotid));
            //OrderExpression exp = new OrderExpression("Sort", false);
            //return this.dbGateway.getRecords(9999, conditions, exp);
        }
        public IList<Wafer_Sbin> GetWaferSbin(string lotid,string waferid)
        {
            Conditions conditions = new Conditions();
            conditions.ConditionExpressions.Add(new Condition("LotID", Operator.EqualTo, lotid));
            conditions.ConditionExpressions.Add(new Condition("WaferID",Operator.EqualTo, waferid));
            conditions.Connector = Connector.AND;
            OrderExpression exp = new OrderExpression("Sort", false);
            return this.dbGateway.getRecords(9999, conditions, exp);
        }
        public int SaveSBin(Wafer_Sbin sbin)
        {
            return this.dbGateway.AddNew(sbin);
        }
    }
}
