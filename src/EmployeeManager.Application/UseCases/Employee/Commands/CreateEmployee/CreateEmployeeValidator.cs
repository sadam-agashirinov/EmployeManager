using FluentValidation;

namespace EmployeeManager.Application.UseCases.Employee.Commands.CreateEmployee;

public class CreateEmployeeValidator : AbstractValidator<CreateEmployeeCommand>
{
    public CreateEmployeeValidator()
    {
        RuleFor(x => x.LastName)
            .Length(1, 100)
            .WithMessage("Фамилия сотрдуника обязательна для заполнения.");
        RuleFor(x => x.FirstName)
            .Length(1, 100)
            .WithMessage("Имя сотрдуника обязательно для заполнения.");
        RuleFor(x => x.Patronymic)
            .Length(1, 100)
            .WithMessage("Отчество сотрдуника обязательно для заполнения.");
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress()
            .WithMessage("Адрес электронной почты имеет неверный формат.");
        RuleFor(x => x.Salary)
            .GreaterThan(0)
            .WithMessage("Зарплата сотрудника имеет не верное значение.");
        RuleFor(x => x.DepartmentsId)
            .NotEmpty()
            .WithMessage("Сотрудник должен принадлежать хотя бы одному отделу");
        RuleForEach(x => x.DepartmentsId)
            .NotEqual(Guid.Empty)
            .WithMessage("Идентификатор отдела имеет неверное значение.");
    }
}