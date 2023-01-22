using MediatR;

namespace EmployeeManager.Application.UseCases.Employee.Commands.DeleteEmployee;

public class DeleteEmployeeQuery : IRequest<Guid>
{
    public Guid Id { get; set; }
}