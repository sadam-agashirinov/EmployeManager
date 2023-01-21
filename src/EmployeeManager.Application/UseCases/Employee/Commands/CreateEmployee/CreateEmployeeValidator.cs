using FluentValidation;

namespace EmployeeManager.Application.UseCases.Employee.Commands.CreateEmployee;

public class CreateEmployeeValidator : AbstractValidator<CreateEmployeeCommand>
{
    public CreateEmployeeValidator()
    {
        RuleFor(x => x.LastName)
            .NotNull()
            .NotEmpty()
            .Length(1, 100)
            .WithMessage("Фамилия сотрдуника обязательна для заполнения.");
        RuleFor(x => x.FirstName)
            .NotNull()
            .NotEmpty()
            .Length(1, 100)
            .WithMessage("Имя сотрдуника обязательно для заполнения.");
        RuleFor(x => x.Patronymic)
            .NotNull()
            .NotEmpty()
            .Length(1, 100)
            .WithMessage("Отчество сотрдуника обязательно для заполнения.");
        RuleFor(x => x.Email)
            .NotNull()
            .NotEmpty()
            .EmailAddress()
            .WithMessage("Адрес электронной почты имеет неверный формат.");
        RuleFor(x => x.Salary)
            .GreaterThan(0)
            .WithMessage("Зарплата сотрудника имеет не верное значение.");
        RuleFor(x => x.DepartmentsId)
            .NotNull()
            .NotEmpty()
            .WithMessage("Сотрудник должен принадлежать хотя бы одному отделу");
        RuleForEach(x => x.DepartmentsId)
            .NotEqual(Guid.Empty)
            .WithMessage("Идентификатор отдела имеет неверное значение.");
    }
}