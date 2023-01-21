using EmployeeManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeeManager.Persistence.EntityTypeConfigurations;

public class EmployeeDepartmentConfiguration : IEntityTypeConfiguration<EmployeeDepartment>
{
    public void Configure(EntityTypeBuilder<EmployeeDepartment> builder)
    {
        builder.ToTable("employee_department");

        builder.HasKey(x => x.Id).HasName("первичный ключь");
        builder.HasIndex(x => x.Id, "pk_employee_department_id")
            .IsUnique();

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .IsRequired()
            .HasComment("идентификатор записи");

        builder.Property(x => x.EmployeeId)
            .HasColumnName("employee_id")
            .IsRequired()
            .HasComment("идентификатор сотрудника");

        builder.Property(x => x.DepartmentId)
            .HasColumnName("department_id")
            .IsRequired()
            .HasComment("идентификатор отдела");

        builder.HasOne(x => x.Employee)
            .WithMany(e => e.EmployeeDepartments)
            .HasForeignKey(x => x.EmployeeId);
        builder.HasOne(x => x.Department)
            .WithMany(d => d.EmployeeDepartments)
            .HasForeignKey(x => x.DepartmentId);
    }
}