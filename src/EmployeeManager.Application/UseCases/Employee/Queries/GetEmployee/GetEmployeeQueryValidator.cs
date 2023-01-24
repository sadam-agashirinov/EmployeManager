using FluentValidation;

namespace EmployeeManager.Application.UseCases.Employee.Queries.GetEmployee;

public class GetEmployeeQueryValidator : AbstractValidator<GetEmployeeQuery>
{
    public GetEmployeeQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Идентификатор пользователя имеет не верное значение.");
    }
}