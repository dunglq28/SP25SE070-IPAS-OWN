namespace CapstoneProject_SP25_IPAS_API.Payload
{
    public static class APIRoutes
    {
        public const string Base = "/ipas";

        public static class WebSocket
        {
            public const string ws = Base + "/websocket";
        }

        public static class Farm
        {
            public const string prefix = Base + "/farms";
            public const string createFarm = prefix + "";
            public const string getFarmById = prefix + "/get-farm-by-id";
            public const string getAllFarmOfUser = prefix + "/get-farm-of-user";
            public const string getFarmWithPagination = prefix + "";
            public const string permanenlyDelete = prefix + "/delete-permanenly";
            public const string softedDeleteFarm = prefix + "/softed-delete-farm";
            public const string updateFarmInfo = prefix + "/update-farm-info";
            public const string updateFarmLogo = prefix + "/update-farm-logo";
            public const string updateFarmCoordination = prefix + "/update-farm-coordination";
        }

        public static class LandPlot
        {
            public const string prefix = Base + "/landplots";
            public const string createLandPlot = prefix + "";
            public const string getAllLandPlotNoPagin = prefix + "";
            public const string updateLandPlotCoordination = prefix + "/update-coordination";
            public const string updateLandPlotInfo = prefix + "/update-info";
            public const string deleteLandPlotOfFarm = prefix + "";
            public const string getLandPlotById = prefix + "";

        }

    }
}
