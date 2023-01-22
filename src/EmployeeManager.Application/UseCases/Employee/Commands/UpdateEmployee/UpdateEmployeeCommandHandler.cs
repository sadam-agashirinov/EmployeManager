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
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);

        if (employee is null) throw new NotFoundException(nameof(Domain.Entities.Employee), request.Id);
        
        employee.LastName = request.LastName;
        employee.FirstName = request.FirstName;
        employee.Patronymic = request.Patronymic;
        employee.Email = request.Email;
        employee.Salary = request.Salary;

        foreach (var employeeDepartment in employee.EmployeeDepartments)
        {
            if (request.DepartmentsId.Contains(employeeDepartment.DepartmentId) == false)
                _dbContext.EmployeeDepartments.Remove(employeeDepartment);
        }
        
        foreach (var departmentId in request.DepartmentsId)
        {
            if (employee.EmployeeDepartments.FirstOrDefault(x => x.DepartmentId == departmentId) is null)
            {
                var employeeDepartment = new EmployeeDepartment()
                {
                    Id = Guid.NewGuid(),
                    EmployeeId = employee.Id,
                    DepartmentId = departmentId
                };
                await _dbContext.EmployeeDepartments.AddAsync(employeeDepartment, cancellationToken);
            }
        }
        
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