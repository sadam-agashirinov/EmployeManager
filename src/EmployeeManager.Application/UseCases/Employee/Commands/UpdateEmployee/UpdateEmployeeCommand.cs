using MediatR;

namespace EmployeeManager.Application.UseCases.Employee.Commands.UpdateEmployee;

public class UpdateEmployeeCommand : IRequest<Guid>
{
    public Guid Id { get; set; }
    public string LastName { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string Patronymic { get; set; } = null!;
    public string Email { get; set; } = null!;
    public decimal Salary { get; set; }
    public List<Guid> DepartmentsId { get; set; }
}