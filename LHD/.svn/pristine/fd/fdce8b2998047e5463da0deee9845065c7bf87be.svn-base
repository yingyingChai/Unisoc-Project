namespace Spreadtrum.LHD.Mvc.Areas.Shared
{
    using KaYi.Utilities;
    using System;

    public static class NavigatorHelper
    {
        public static string GenerateNavigatorClass(string matchIDs, string navigatorID)
        {
            if ((!StringHelper.isNullOrEmpty(navigatorID) && !StringHelper.isNullOrEmpty(matchIDs)) && (string.Copy(matchIDs).IndexOf(navigatorID) >= 0))
            {
                return " in ";
            }
            return "";
        }
    }
}

