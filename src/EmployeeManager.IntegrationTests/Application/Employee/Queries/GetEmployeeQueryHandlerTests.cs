using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EmployeeManager.Application.UseCases.Employee.Queries.GetEmployee;
using EmployeeManager.Domain.Entities;
using EmployeeManager.IntegrationTests.Application.Common;
using FluentAssertions;
using NUnit.Framework;

namespace EmployeeManager.IntegrationTests.Application.Employee.Queries;

public class GetEmployeeQueryHandlerTests : BaseTest
{
    [Test]
    public async Task ItShould_Return_Employee()
    {
        //arrange
        var employee = await CreateEmployee();
        
        var getEmployeeQuery = new GetEmployeeQuery()
        {
            Id = employee.Id
        };

        var sut = new GetEmployeeQueryHandler(AppDbContext);
        
        //act
        var employeeFromDb = await sut.Handle(getEmployeeQuery, CancellationToken.None);
        
        //assert
        employeeFromDb.Should().NotBeNull();
        employeeFromDb.Id.Should().Be(employee.Id);
        employeeFromDb.LastName.Should().Be(employee.LastName);
        employeeFromDb.FirstName.Should().Be(employee.FirstName);
        employeeFromDb.Patronymic.Should().Be(employee.Patronymic);
        employeeFromDb.Email.Should().Be(employee.Email);
        employeeFromDb.Salary.Should().Be(employee.Salary);
        foreach (var department in employee.EmployeeDepartments)
        {
            employeeFromDb.EmployeeDepartments.Contains(department).Should().BeTrue();
        }
    }
    
    private async Task<Domain.Entities.Employee> CreateEmployee()
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