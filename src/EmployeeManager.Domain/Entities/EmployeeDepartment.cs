using EmployeeManager.Domain.Common;

namespace EmployeeManager.Domain.Entities;

public class EmployeeDepartment : Entity
{
    public Guid EmployeeId { get; set; }
    public Guid DepartmentId { get; set; }

    public Employee Employee { get; set; }
    public Department Department { get; set; }
}