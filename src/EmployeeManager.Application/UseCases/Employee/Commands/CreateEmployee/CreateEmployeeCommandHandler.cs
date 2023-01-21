using EmployeeManager.Application.Interfaces;
using EmployeeManager.Domain.Entities;
using MediatR;

namespace EmployeeManager.Application.UseCases.Employee.Commands.CreateEmployee;

public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, Guid>
{
    private readonly IAppDbContext _dbContext;

    public CreateEmployeeCommandHandler(IAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Guid> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
    {
        var employeeId = Guid.NewGuid();
        var departments = request.DepartmentsId.Select(departmentId => new EmployeeDepartment
        {
            Id = Guid.NewGuid(),
            EmployeeId = employeeId,
            DepartmentId = departmentId
        }).ToList();
        var employee = new Domain.Entities.Employee()
        {
            Id = employeeId,
            LastName = request.LastName,
            FirstName = request.FirstName,
            Patronymic = request.Patronymic,
            Email = request.Email,
            Salary = request.Salary,
            EmployeeDepartments = departments
        };
        
        await _dbContext.Employees.AddAsync(employee, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return employee.Id;
    }
}