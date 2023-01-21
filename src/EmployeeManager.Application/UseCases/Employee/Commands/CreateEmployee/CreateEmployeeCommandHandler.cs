using EmployeeManager.Application.Interfaces;
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
        var employee = new Domain.Entities.Employee()
        {
            Id = Guid.NewGuid(),
            LastName = request.LastName,
            FirstName = request.FirstName,
            Patronymic = request.Patronymic,
            Email = request.Email,
            Salary = request.Salary,
            EmployeeDepartmentIds = request.DepartmentsId
        };
        
        await _dbContext.Employees.AddAsync(employee, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return employee.Id;
    }
}