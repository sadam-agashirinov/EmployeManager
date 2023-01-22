using EmployeeManager.Application.Common.Exceptions;
using EmployeeManager.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManager.Application.UseCases.Employee.Queries.DeleteEmployeeQuery;

public class DeleteEmployeeQueryHandler : IRequestHandler<DeleteEmployeeQuery, Guid>
{
    private readonly IAppDbContext _dbContext;

    public DeleteEmployeeQueryHandler(IAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Guid> Handle(DeleteEmployeeQuery request, CancellationToken cancellationToken)
    {
        var employee =
            await _dbContext.Employees.SingleOrDefaultAsync(x => x.Id == request.Id,
                cancellationToken: cancellationToken);
        if (employee is null) throw new NotFoundException(nameof(Domain.Entities.Employee), request.Id);

        _dbContext.Employees.Remove(employee);
        await  _dbContext.SaveChangesAsync(cancellationToken);

        return request.Id;
    }
}