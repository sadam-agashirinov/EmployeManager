using FluentValidation;

namespace EmployeeManager.Application.UseCases.Employee.Queries.DeleteEmployeeQuery;

public class DeleteEmployeeQueryValidator : AbstractValidator<DeleteEmployeeQuery>
{
    public DeleteEmployeeQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEqual(Guid.Empty)
            .WithMessage("Идентификатор сотрудника имеет не верное значение.");
    }
}