using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EmployeeManager.Application.UseCases.Employee.Commands.UpdateEmployee;
using EmployeeManager.Domain.Entities;
using EmployeeManager.IntegrationTests.Application.Common;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace EmployeeManager.IntegrationTests.Application.Employee.Commands;

public class UpdateEmployeeCommandHandlerTest : BaseTest
{
    [Test]
    public async Task ItShould_Update_Employee_And_Return_EmployeeId()
    {
        //arrange
        var employeeId = await CreateEmployee();
        var updateEmployeeCommand = new UpdateEmployeeCommand()
        {
            Id = employeeId,
            LastName = "NewLastname",
            FirstName = "NewFirstname",
            Patronymic = "NewPatronymic",
            Email = "new_email@mail.com",
            Salary = 12345,
            DepartmentsId = new List<Guid>()
            {
                Guid.Parse("0fb37d5b-04bd-478a-841d-79f954be6528"),
                Guid.Parse("223a57cf-4650-4ec7-b39f-0970837d6769"),
                Guid.Parse("ede94df8-b101-452e-bddc-fe68431622ee")
            }
        };
        var sut = new UpdateEmployeeCommandHandler(AppDbContext);

        //act
        var updateEmployeeId = await sut.Handle(updateEmployeeCommand, CancellationToken.None);
        
        //assert
        var employeeFromDb = await AppDbContext.Employees.SingleOrDefaultAsync(x => x.Id == updateEmployeeId);
        employeeFromDb.Should().NotBeNull();
        employeeFromDb.LastName.Should().Be(updateEmployeeCommand.LastName);
        employeeFromDb.FirstName.Should().Be(updateEmployeeCommand.FirstName);
        employeeFromDb.Patronymic.Should().Be(updateEmployeeCommand.Patronymic);
        employeeFromDb.Email.Should().Be(updateEmployeeCommand.Email);
        employeeFromDb.Salary.Should().Be(updateEmployeeCommand.Salary);
        employeeFromDb.EmployeeDepartments.Count.Should().Be(updateEmployeeCommand.DepartmentsId.Count);
        employeeFromDb.EmployeeDepartments.All(x => updateEmployeeCommand.DepartmentsId.Contains(x.DepartmentId));
    }
    
    private async Task<Guid> CreateEmployee()
    {
        var employeeId = Guid.NewGuid();
        var employee = new Domain.Entities.Employee
        {
            Id = employeeId,
            LastName = "Lastname",
            FirstName = "Firstname",
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
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    EmployeeId = employeeId,
                    DepartmentId = Guid.Parse("0fb37d5b-04bd-478a-841d-79f954be6528")
                }
            }
        };
        await AppDbContext.Employees.AddAsync(employee);
        await AppDbContext.SaveChangesAsync();

        return employee.Id;
    }
}