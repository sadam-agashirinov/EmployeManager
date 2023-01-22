using MediatR;

namespace EmployeeManager.Application.UseCases.Employee.Commands.DeleteEmployeeQuery;

public class DeleteEmployeeQuery : IRequest<Guid>
{
    public Guid Id { get; set; }
}