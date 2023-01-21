using System.Reflection;
using EmployeeManager.Application.Interfaces;
using EmployeeManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManager.Persistence;

public class AppDbContext : DbContext, IAppDbContext
{
    public DbSet<Department> Departments { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<EmployeeDepartment> EmployeeDepartments { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}