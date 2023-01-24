using System;
using EmployeeManager.Persistence;
using NUnit.Framework;

namespace EmployeeManager.IntegrationTests.Application.Common;

[TestFixture]
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