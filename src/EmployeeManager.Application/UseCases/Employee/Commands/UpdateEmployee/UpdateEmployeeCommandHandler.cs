using EmployeeManager.Application.Common.Exceptions;
using EmployeeManager.Application.Interfaces;
using EmployeeManager.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManager.Application.UseCases.Employee.Commands.UpdateEmployee;

public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, Guid>
{
    private readonly IAppDbContext _dbContext;

    public UpdateEmployeeCommandHandler(IAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Guid> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
    {
        var employee = await _dbContext
            .Employees
            .Include(x => x.EmployeeDepartments)
            .SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);

        if (employee is null) throw new NotFoundException(nameof(Domain.Entities.Employee), request.Id);
        
        employee.LastName = request.LastName;
        employee.FirstName = request.FirstName;
        employee.Patronymic = request.Patronymic;
        employee.Email = request.Email;
        employee.Salary = request.Salary;

        employee.EmployeeDepartments.ToList()
            .RemoveAll(x => request.DepartmentsId.Contains(x.DepartmentId) == false);

        request.DepartmentsId.ForEach(departmentId =>
        {
            if (employee.EmployeeDepartments.ToList().Select(x => x.DepartmentId).Contains(departmentId) == false)
                employee.EmployeeDepartments.Add(new EmployeeDepartment
                {
                    Id = Guid.NewGuid(),
                    EmployeeId = employee.Id,
                    DepartmentId = departmentId
                });
        });

        _dbContext.Employees.Update(employee);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return employee.Id;
    }
}