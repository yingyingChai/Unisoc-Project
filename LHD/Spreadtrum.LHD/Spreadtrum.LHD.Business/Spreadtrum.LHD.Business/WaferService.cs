using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spreadtrum.LHD.DAL.Lots;
using Spreadtrum.LHD.Entity.Lots;
using System.Data;
using System.Web;
using System.Collections;
using KaYi.Utilities;

namespace Spreadtrum.LHD.Business
{
    public class WaferService
    {
        private WaferGateway way = new WaferGateway();
        private WaferGeteway2 way2 = new WaferGeteway2();
        private Lot_TransformedGateway lotWay = new Lot_TransformedGateway();
        public IList<Wafer> GetAllWaferBy(WaferQuery query, int pageSize, int pageIndex, out int recoedCount)
        {
            return this.way.GetAllWaferBy(query, pageSize, pageIndex, out recoedCount);
        }

        public int UpdateWaferStatus(HttpRequestBase req, int type, string userName)
        {
            Dictionary<int,ArrayList> dic=this.way.UpdateWaferStatus(req, type, userName);
            int suc = 0;
            int dispose = StringHelper.isNullOrEmpty(req.QueryString["HidStatus"]) ? 0 : int.Parse(req.QueryString["HidStatus"]);
            if (dic != null)
            {
                foreach (KeyValuePair<int, ArrayList> kvp in dic)
                {
                    suc = kvp.Key;
                    ArrayList array = kvp.Value;
                    if (array != null) {
                        foreach (string s in array)
                        {
                            Lot_Transformed lot = this.lotWay.GetTransformById(s);
                            if (type != 3 && lot != null)
                            {
                                //发邮件
                                NotificationService.CreateCpOSATConfirmNotificationsWhilePeDispose(lot.Vendor, lot, "test", dispose);
                            }
                        }
                    }
                }
            }
            
            return suc;
        }
        public int DisposeByLot(string lotids, int type, int dispose, HttpRequestBase req, string userName)
        {
            Dictionary<int, ArrayList> dic = this.way.DisposeByLot(lotids, type, dispose, req, userName);
            int suc = 0;
            if (dic != null) {
                foreach (KeyValuePair<int, ArrayList> kvp in dic)
                {
                    suc = kvp.Key;
                    ArrayList array = kvp.Value;
                    if (array != null)
                    {
                        foreach (string s in array)
                        {
                            Lot_Transformed lot = this.lotWay.GetTransformById(s);
                            if (type != 3 && lot != null)
                            {
                                //发邮件
                                NotificationService.CreateCpOSATConfirmNotificationsWhilePeDispose(lot.Vendor, lot, "test", dispose);
                            }
                        }
                    }
                }
            }
            return suc;
        }
        public IList<Wafer> GetWaferByLotid(DetailWaferQuery query, int pageIndex, int pageSize, out int recoedCount)
        {
            return this.way.GetWaferByLotid(query,pageIndex,pageSize,out recoedCount);
        }
        public int SaveWafer(SqlWafer wafer)
        {
            return this.way2.SaveWafer(wafer);
        }
        public string LoadCountByDispose(string lotid, int status)
        {
            return this.way.LoadCountByDispose(lotid, status);
        }
    }
}
