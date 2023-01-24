using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EmployeeManager.Application.UseCases.Employee.Queries.GetEmployee;
using EmployeeManager.IntegrationTests.Application.Employee.Common;
using FluentAssertions;
using NUnit.Framework;

namespace EmployeeManager.IntegrationTests.Application.Employee.Queries;

public class GetEmployeeQueryHandlerTests : EmployeeBaseTest
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
            employeeFromDb.EmployeeDepartments.FirstOrDefault(x => x.Id == department.Id).Should().NotBeNull();
        }
    }
}