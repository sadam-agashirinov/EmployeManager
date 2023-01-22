namespace EmployeeManager.WepApi.Controllers.Common
{
    /// <summary>
    /// Маршруты для доступа к метод сервиса
    /// </summary>
    public static class ApiRouters
    {
        public const string Root = "api/market";

        public static class Employee
        {
            public const string Base = Root + "/[controller]";

            public const string Create = Base;
            public const string Delete = Base + "/{id:Guid}";
            public const string Update = Base + "/{id:Guid}";
                
        }
    }
}