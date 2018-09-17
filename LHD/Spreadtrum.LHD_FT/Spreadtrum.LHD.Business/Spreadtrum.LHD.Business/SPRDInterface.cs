namespace Spreadtrum.LHD.Business
{
    using Spreadtrum.LHD.DAL.Users;
    using Spreadtrum.LHD.Entity.Users;
    using System;
    using System.Collections.Generic;

    public static class SPRDInterface
    {
        private static SPRDUserGateway sprdUserGateway = new SPRDUserGateway();

        public static SPRDUser GetSPRDUserByEmail(string email)
        {
            return sprdUserGateway.GetSPRDUsersByEmail(email);
        }

        public static SPRDUser GetSPRDUserByID(string id)
        {
            return sprdUserGateway.GetSPRDUserByID(id);
        }

        public static IList<SPRDUser> GetSPRDUserByKeyword(string keyword)
        {
            return sprdUserGateway.GetSPRDUsersByKeyword(keyword);
        }
    }
}

