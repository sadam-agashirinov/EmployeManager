using MediatR;

namespace EmployeeManager.Application.UseCases.Employee.Queries.DeleteEmployeeQuery;

public class DeleteEmployeeQuery : IRequest<Guid>
{
    public Guid Id { get; set; }
}