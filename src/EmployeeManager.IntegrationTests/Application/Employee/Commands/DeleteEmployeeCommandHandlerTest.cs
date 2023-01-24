using System.Threading;
using System.Threading.Tasks;
using EmployeeManager.Application.UseCases.Employee.Commands.DeleteEmployee;
using EmployeeManager.IntegrationTests.Application.Employee.Common;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace EmployeeManager.IntegrationTests.Application.Employee.Commands;

public class DeleteEmployeeCommandHandlerTest : EmployeeBaseTest
{
    [Test]
    public async Task ItShould_DeleteEmployee_And_Return_EmployeeId()
    {
        //arrange
        var employee = await CreateEmployee();
        var deleteEmployeeQuery = new DeleteEmployeeCommand
        {
            Id = employee.Id
        };
        var sut = new DeleteEmployeeCommandHandler(AppDbContext);
        
        //act
        var deletedEmployeeId = await sut.Handle(deleteEmployeeQuery, CancellationToken.None);
        
        //assert
        var employeeFromDb = await AppDbContext.Employees.FirstOrDefaultAsync(x => x.Id == deletedEmployeeId);
        employeeFromDb.Should().BeNull();
    }
}