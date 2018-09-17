namespace Spreadtrum.LHD.Entity.Lots
{
    using System;

    public static class DecisionUtility
    {
        public static string GetDecisionText(int decision)
        {
            string str = string.Empty;
            switch (decision)
            {
                case 0:
                    return "RELEASE";

                case 1:
                    return "BIN1 RELEASE";

                case 2:
                    return "RESCREEN";

                case 3:
                    return "SCRAP";

                case 4:
                    return "PENDING";

                case 0xfd:
                    return "MANUAL HOLD";

                case 0xfe:
                    return "HOLD";

                case 0xff:
                    return "NORMAL";
            }
            return str;
        }
    }
}

