using EmployeeManager.Domain.Common;

namespace EmployeeManager.Domain.Entities;

public class Department : Entity
{
    public string Name { get; set; } = null!;
    public Guid? ParentDepartmentId { get; set; }
    public Guid EmployeeDepartmentId { get; set; }
    
    public virtual Department ParentDepartment { get; set; }
    public virtual EmployeeDepartment EmployeeDepartment { get; set; } = null!;
}