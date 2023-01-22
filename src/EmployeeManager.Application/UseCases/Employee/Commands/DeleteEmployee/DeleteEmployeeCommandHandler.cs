using EmployeeManager.Application.Common.Exceptions;
using EmployeeManager.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManager.Application.UseCases.Employee.Commands.DeleteEmployee;

public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand, Guid>
{
    private readonly IAppDbContext _dbContext;

    public DeleteEmployeeCommandHandler(IAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Guid> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
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