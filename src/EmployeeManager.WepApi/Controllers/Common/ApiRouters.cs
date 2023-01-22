namespace EmployeeManager.WepApi.Controllers.Common
{
    /// <summary>
    /// Маршруты для доступа к метод сервиса
    /// </summary>
    public static class ApiRouters
    {
        public const string Root = "api/market";
        public const string Version = "v{version:apiVersion}";

        public static class V1
        {
            public const string VersionName = "1";
            public const string Base = Root + "/[controller]";

            public static class Employee
            {
                public const string Create = Base;
                public const string Delete = Base + "/{id:Guid}";
            }
        }
    }
}