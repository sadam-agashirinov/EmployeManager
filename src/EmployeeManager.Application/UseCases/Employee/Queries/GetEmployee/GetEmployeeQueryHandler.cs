using EmployeeManager.Application.Common.Exceptions;
using EmployeeManager.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManager.Application.UseCases.Employee.Queries.GetEmployee;

public class GetEmployeeQueryHandler : IRequestHandler<GetEmployeeQuery, Domain.Entities.Employee>
{
    private readonly IAppDbContext _dbContext;

    public GetEmployeeQueryHandler(IAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Domain.Entities.Employee> Handle(GetEmployeeQuery request, CancellationToken cancellationToken)
    {
        var employee = await _dbContext.Employees
            .Include(x => x.EmployeeDepartments)
            .ThenInclude(x => x.Department)
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);

        if (employee is null) throw new NotFoundException(nameof(Domain.Entities.Employee), request.Id);

        return employee;
    }
}