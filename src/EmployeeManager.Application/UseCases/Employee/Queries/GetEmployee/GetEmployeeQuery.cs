using MediatR;

namespace EmployeeManager.Application.UseCases.Employee.Queries.GetEmployee;

public class GetEmployeeQuery : IRequest<Domain.Entities.Employee>
{
    public Guid Id { get; set; }
}