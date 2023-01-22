using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EmployeeManager.Application.UseCases.Employee.Commands;
using EmployeeManager.Application.UseCases.Employee.Commands.CreateEmployee;
using EmployeeManager.IntegrationTests.Application.Common;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace EmployeeManager.IntegrationTests.Application.Employee.Commands;

public class CreateEmployeeCommandHandlerTests : BaseTest
{
    [Test]
    public async Task ItShould_CreateEmployee_And_Return_EmployeeId()
    {
        //arrange
        var employee = new CreateEmployeeCommand
        {
            LastName = "LastName",
            FirstName = "FirstName",
            Patronymic = "Patronymic",
            Email = "email@test.ru",
            Salary = 1000,
            DepartmentsId = new List<Guid>()
            {
                Guid.Parse("9ab46f93-6e3c-4e15-b79e-190e1105c33d"),
                Guid.Parse("ede94df8-b101-452e-bddc-fe68431622ee")
            }
        };
        var sut = new CreateEmployeeCommandHandler(AppDbContext);
        
        //act
        var employeeId = await sut.Handle(employee, CancellationToken.None);
        
        //assert
        var employeeFromDb = await AppDbContext.Employees.FirstOrDefaultAsync(x => x.Id == employeeId);

        employeeFromDb.Should().NotBeNull();
        employeeFromDb.Id.Should().Be(employeeId);
        employeeFromDb.LastName.Should().Be(employee.LastName);
        employeeFromDb.FirstName.Should().Be(employee.FirstName);
        employeeFromDb.Patronymic.Should().Be(employee.Patronymic);
        employeeFromDb.Email.Should().Be(employee.Email);
        employeeFromDb.Salary.Should().Be(employee.Salary);
        employeeFromDb.EmployeeDepartments.Select(x => x.DepartmentId).Should().Contain(employee.DepartmentsId);
    }
}