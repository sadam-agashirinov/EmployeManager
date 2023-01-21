using System.Collections.ObjectModel;
using EmployeeManager.Domain.Common;

namespace EmployeeManager.Domain.Entities;

public class Department : Entity
{
    public Department()
    {
        EmployeeDepartments = new Collection<EmployeeDepartment>();
    }

    public string Name { get; set; } = null!;
    public Guid? ParentDepartmentId { get; set; }
    public Guid EmployeeDepartmentId { get; set; }
    
    public virtual Department? ParentDepartment { get; set; }
    public virtual ICollection<EmployeeDepartment> EmployeeDepartments { get; set; }
}