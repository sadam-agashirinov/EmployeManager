using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EmployeeManager.Domain.Entities;
using EmployeeManager.IntegrationTests.Application.Common;

namespace EmployeeManager.IntegrationTests.Application.Employee.Common;

public class EmployeeBaseTest : BaseTest
{
    protected async Task<Domain.Entities.Employee> CreateEmployee()
    {
        var employeeId = Guid.NewGuid();
        var employee = new Domain.Entities.Employee
        {
            Id = employeeId,
            LastName = "Lastname",
            FirstName = "Firsname",
            Patronymic = "Patronymic",
            Email = "email@mail.com",
            Salary = 1000,
            EmployeeDepartments = new List<EmployeeDepartment>()
            {
                new()
                {
                    Id = Guid.NewGuid(),
                    EmployeeId = employeeId,
                    DepartmentId = Guid.Parse("9ab46f93-6e3c-4e15-b79e-190e1105c33d")
                }
            }
        };
        await AppDbContext.Employees.AddAsync(employee);
        await AppDbContext.SaveChangesAsync();

        return employee;
    }
}