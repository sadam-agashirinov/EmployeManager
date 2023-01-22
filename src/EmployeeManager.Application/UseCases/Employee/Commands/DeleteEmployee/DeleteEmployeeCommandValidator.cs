using FluentValidation;

namespace EmployeeManager.Application.UseCases.Employee.Commands.DeleteEmployee;

public class DeleteEmployeeCommandValidator : AbstractValidator<DeleteEmployeeCommand>
{
    public DeleteEmployeeCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEqual(Guid.Empty)
            .WithMessage("Идентификатор сотрудника имеет не верное значение.");
    }
}