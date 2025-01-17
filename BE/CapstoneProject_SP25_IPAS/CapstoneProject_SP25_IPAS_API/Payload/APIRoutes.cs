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
            public const string createFarm = Base + "/farms";
            public const string getFarmById = Base + "/farms/get-farm-by-id";
            public const string getAllFarmOfUser = Base + "/farms/get-farm-of-user";
            public const string getFarmWithPagination = Base + "/farms";
            public const string permanenlyDelete = Base + "/farms/delete-permanenly";
            public const string softedDeleteFarm = Base + "/farms/softed-delete-farm";
            public const string updateFarmInfo = Base + "/farms/update-farm-info";
            public const string updateFarmLogo = Base + "/farms/update-farm-logo";
            public const string updateFarmCoordination = Base + "/farms/update-farm-coordination";
        }

    }
}
