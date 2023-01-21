using System;
using System.Threading.Tasks;
using EmployeeManager.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManager.IntegrationTests.Application.Common;

public class EmployeeManagerDbContextFactory
{
    public static AppDbContext Create()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        var dbContext = new AppDbContext(options);
        dbContext.Database.EnsureCreated();
        
        return dbContext;
    }

    public static void Destroy(AppDbContext dbContext)
    {
        dbContext.Database.EnsureDeleted();
        dbContext.Dispose();
    }
}