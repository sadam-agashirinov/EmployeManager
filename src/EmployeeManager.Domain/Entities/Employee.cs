﻿using EmployeeManager.Domain.Common;

namespace EmployeeManager.Domain.Entities;

public class Employee : Entity
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Patronymic { get; set; } = null!;
    public string Email { get; set; } = null!;
    public decimal Salary { get; set; }
    public ICollection<Guid> EmployeeDepartmentIds { get; set; }

    public virtual ICollection<EmployeeDepartment> EmployeeDepartments { get; set; } = null!;
}