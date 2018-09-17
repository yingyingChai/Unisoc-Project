namespace Spreadtrum.LHD.Business
{
    using Spreadtrum.LHD.DAL.Systems;
    using Spreadtrum.LHD.Entity.Systems;
    using System;
    using System.Collections.Generic;

    public class SilicondashService
    {
        private static SlicondashOSATConfigurationGateway silicondashGateway = new SlicondashOSATConfigurationGateway();

        public static IList<SilicondashOSATConfiguration> GetSilicondashOSATConfigurations()
        {
            return silicondashGateway.GetAllOSATConfigurations();
        }
    }
}

