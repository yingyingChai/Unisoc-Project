namespace Spreadtrum.LHD.Mvc.Areas.Lots.Controllers
{
    using KaYi.Utilities;
    using Spreadtrum.LHD.Entity.Lots;
    using System;
    using System.Web;

    public static class QueryUtility
    {
        public static LotQuery GetQueryFromRequest(HttpRequestBase Request, string userID)
        {
            LotQuery query = new LotQuery {
                CurrentUser = userID,
                AutoJudgeResult = StringHelper.isNullOrEmpty(Request.QueryString["cmbAutoJudgeResult"]) ? AutoJudgeResult.Empty : ((AutoJudgeResult) Enum.Parse(typeof(AutoJudgeResult), Request.QueryString["cmbAutoJudgeResult"])),
                Bin1 = Request.QueryString["txtbin1"],
                Bin2 = Request.QueryString["txtbin2"],
                Bin3 = Request.QueryString["txtbin3"],
                Bin4 = Request.QueryString["txtbin4"],
                Bin5 = Request.QueryString["txtbin5"],
                Bin6 = Request.QueryString["txtbin6"],
                Bin7 = Request.QueryString["txtbin7"],
                Bin8 = Request.QueryString["txtbin8"],
                Bin9 = Request.QueryString["txtbin9"],
                Bin10 = Request.QueryString["txtbin10"],
                CompletionDate = Request.QueryString["txtCompletionDate"],
                Damage = Request.QueryString["txtDamage"],
                DeviceCode = Request.QueryString["txtDeviceCode"],
                DeviceName = Request.QueryString["txtDeviceName"],
                Die1CP = Request.QueryString["txtDie1CP"],
                Die1LotNo = Request.QueryString["txtDie1LotNo"],
                Die2CP = Request.QueryString["txtDie2CP"],
                Die2LotNo = Request.QueryString["txtDie2LotNo"],
                Die3CP = Request.QueryString["txtDie3CP"],
                Die3LotNo = Request.QueryString["txtDie3LotNo"],
                Die4CP = Request.QueryString["txtDie4CP"],
                Die4LotNo = Request.QueryString["txtDie4LotNo"],
                HoldReason = Request.QueryString["HoldReason"],
                LBNO = Request.QueryString["txtLBNo"],
                Loss = Request.QueryString["txtLoss"],
                LotNO = Request.QueryString["txtLotNo"],
                Status = Request.QueryString["txtStatus"],
                LotType = Request.QueryString["cmbLotType"],
                ManualHold = StringHelper.isNullOrEmpty(Request.QueryString["cmbManualHold"]) ? -1 : Convert.ToInt32(Request.QueryString["cmbManualHold"]),
                Osat = Request.QueryString["txtOSAT"],
                OsRate = Request.QueryString["txtOSRate"],
                PEDispose = StringHelper.isNullOrEmpty(Request.QueryString["cmbPEDispose"]) ? -1 : Convert.ToInt32(Request.QueryString["cmbPEDispose"]),
                Platform = Request.QueryString["txtPlatform"],
                QADispose = StringHelper.isNullOrEmpty(Request.QueryString["cmbQADispose"]) ? -1 : Convert.ToInt32(Request.QueryString["cmbQADispose"]),
                QtyIn = StringHelper.isNullOrEmpty(Request.QueryString["txtQtyIn"]) ? -1 : Convert.ToInt32(Request.QueryString["txtQtyIn"]),
                RetestTimes = StringHelper.isNullOrEmpty(Request.QueryString["txtRetestTimes"]) ? -1 : Convert.ToInt32(Request.QueryString["txtRetestTimes"]),
                SPRDDecision = StringHelper.isNullOrEmpty(Request.QueryString["cmbSPRDDecision"]) ? -1 : Convert.ToInt32(Request.QueryString["cmbSPRDDecision"]),
                SubconLot = Request.QueryString["txtSubconLot"],
                TesterID = Request.QueryString["txtTesterID"],
                TestProgram = Request.QueryString["txtTestProgram"],
                Stage = Request.QueryString["txtStage"],
                Yield = Request.QueryString["txtYield"],
                OtherBinDispose = StringHelper.isNullOrEmpty(Request.QueryString["cmbOtherBinDispose"]) ? -1 : Convert.ToInt32(Request.QueryString["cmbOtherBinDispose"])
            };
            string s = Request.QueryString["action"];
            uint num = ComputeStringHash(s);
            if (num <= 0x3cfd7330)
            {
                switch (num)
                {
                    case 0x302c992:
                        if (s == "NewComment")
                        {
                            query.NewCommentOnly = true;
                            return query;
                        }
                        return query;

                    case 0x289beb49:
                        if (s == "LotDispose")
                        {
                            query.WaitForDisposeOnly = true;
                            return query;
                        }
                        return query;

                    case 0x3cfd7330:
                        if (s == "WaitConfirm")
                        {
                            query.WaitForConfirmOnly = true;
                            return query;
                        }
                        return query;
                }
                return query;
            }
            if (num <= 0x9e89e783)
            {
                switch (num)
                {
                    case 0x811c9dc5:
                        if ((s != null) && (s.Length != 0))
                        {
                        }
                        return query;

                    case 0x9e89e783:
                        if (!(s == "Query"))
                        {
                        }
                        return query;
                }
                return query;
            }
            switch (num)
            {
                case 0xc837a832:
                    if (s == "EQALots")
                    {
                        query.EqaLotsOnly = true;
                        return query;
                    }
                    return query;

                case 0xe9f83059:
                    if (s == "WaitForOtherBinDispose")
                    {
                        query.WaitForOtherBinDispose = true;
                        return query;
                    }
                    return query;
            }
            return query;
        }

        internal static uint ComputeStringHash(string text)
        {
            uint hashCode = 0;
            if (text != null)
            {
                hashCode = unchecked((uint)2166136261);

                int i = 0;
                goto start;

            again:
                hashCode = unchecked((text[i] ^ hashCode) * 16777619);
                i = i + 1;

            start:
                if (i < text.Length)
                    goto again;
            }
            return hashCode;
        }
    }
}

