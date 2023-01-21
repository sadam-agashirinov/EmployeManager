﻿namespace EmployeeManager.WepApi.Dto;

public class CreateEmployeeDto
{
    public string LastName { get; set; }
    public string FirstName { get; set; }
    public string Patronymic { get; set; }
    public string Email { get; set; }
    public decimal Salary { get; set; }
    public List<Guid> DepartmentsId { get; set; }
}