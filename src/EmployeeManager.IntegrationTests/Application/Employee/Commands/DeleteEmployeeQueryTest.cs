using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EmployeeManager.Application.UseCases.Employee.Commands.DeleteEmployee;
using EmployeeManager.Domain.Entities;
using EmployeeManager.IntegrationTests.Application.Common;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace EmployeeManager.IntegrationTests.Application.Employee.Commands;

public class DeleteEmployeeQueryTest : BaseTest
{
    [Test]
    public async Task ItShould_DeleteEmployee_And_Return_EmployeeId()
    {
        //arrange
        var employeeId = await CreateEmployee();
        var deleteEmployeeQuery = new DeleteEmployeeQuery
        {
            Id = employeeId
        };
        var sut = new DeleteEmployeeQueryHandler(AppDbContext);
        
        //act
        var deletedEmployeeId = await sut.Handle(deleteEmployeeQuery, CancellationToken.None);
        
        //assert
        var employee = await AppDbContext.Employees.SingleOrDefaultAsync(x => x.Id == deletedEmployeeId);
        employee.Should().BeNull();
    }

    private async Task<Guid> CreateEmployee()
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

        return employee.Id;
    }
}