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
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);

        if (employee is null) throw new NotFoundException(nameof(Domain.Entities.Employee), request.Id);
        
        employee.LastName = request.LastName;
        employee.FirstName = request.FirstName;
        employee.Patronymic = request.Patronymic;
        employee.Email = request.Email;
        employee.Salary = request.Salary;

        await ModifyEmployeeDepartments(employee.Id, request.DepartmentsId);
        
        _dbContext.Employees.Update(employee);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return employee.Id;
    }

    private async Task ModifyEmployeeDepartments(Guid employeeId, IEnumerable<Guid> departmentsIdFromRequest)
    {
        var employeeDepartmentsFromDb =
            await _dbContext.EmployeeDepartments.Where(x => x.EmployeeId == employeeId).ToListAsync();

        var willRemoveEmployeeDepartment = employeeDepartmentsFromDb
            .Where(employeeDepartment => departmentsIdFromRequest.Contains(employeeDepartment.DepartmentId) == false)
            .ToList();
        
        foreach (var employeeDepartment in willRemoveEmployeeDepartment)
        {
            _dbContext.EmployeeDepartments.Remove(employeeDepartment);
        }

        foreach (var departmentId in departmentsIdFromRequest)
        {
            if (employeeDepartmentsFromDb.FirstOrDefault(x => x.DepartmentId == departmentId) is null)
            {
                var employeeDepartment = new EmployeeDepartment()
                {
                    Id = Guid.NewGuid(),
                    EmployeeId = employeeId,
                    DepartmentId = departmentId
                };
                await _dbContext.EmployeeDepartments.AddAsync(employeeDepartment);
            }
        }
    }
}