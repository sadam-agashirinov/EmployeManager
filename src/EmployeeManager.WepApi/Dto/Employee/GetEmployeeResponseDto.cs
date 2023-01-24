namespace EmployeeManager.WepApi.Dto.Employee;

public class GetEmployeeResponseDto
{
    public string LastName { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string Patronymic { get; set; } = null!;
    public string Email { get; set; } = null!;
    public decimal Salary { get; set; }
    public List<Guid> DepartmentsId { get; set; } = null!;
}