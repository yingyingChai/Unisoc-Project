namespace Spreadtrum.LHD.Business
{
    using Spreadtrum.LHD.DAL.Users;
    using Spreadtrum.LHD.Entity.Users;
    using System;

    public static class OSATPortalInterface
    {
        private static OSATUserGateway osatUserGateway = new OSATUserGateway();

        public static OSATUser GetOSATUserByEmail(string email)
        {
            return osatUserGateway.GetOSATUsersByEmail(email);
        }
    }
}

