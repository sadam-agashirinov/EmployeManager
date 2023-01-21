using System;
using EmployeeManager.Persistence;

namespace EmployeeManager.IntegrationTests.Application.Common;

public class BaseTest : IDisposable
{
    protected readonly AppDbContext AppDbContext;

    public BaseTest()
    {
        AppDbContext = EmployeeManagerDbContextFactory.Create();
    }
    
    public void Dispose()
    {
        EmployeeManagerDbContextFactory.Destroy(AppDbContext);
    }
}