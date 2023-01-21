using EmployeeManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManager.Application.Interfaces;

public interface IAppDbContext
{
    public DbSet<Department> Departments { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<EmployeeDepartment> EmployeeDepartments { get; set; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}