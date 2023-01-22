using MediatR;

namespace EmployeeManager.Application.UseCases.Employee.Commands.DeleteEmployee;

public class DeleteEmployeeCommand : IRequest<Guid>
{
    public Guid Id { get; set; }
}