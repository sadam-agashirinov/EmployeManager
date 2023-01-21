using EmployeeManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeeManager.Persistence.EntityTypeConfigurations;

public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
{
    public void Configure(EntityTypeBuilder<Department> builder)
    {
        builder.ToTable("department", tableBuilder =>
        {
            tableBuilder.HasComment("информация об отделе");
        });
        
        builder.HasKey(x => x.Id).HasName("первичный ключь");
        builder.HasIndex(x => x.Id, "pk_department_id")
            .IsUnique();

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .IsRequired()
            .HasComment("идентификатор");

        builder.Property(x => x.Name)
            .HasColumnName("name")
            .IsRequired()
            .HasComment("наименование");

        builder.Property(x => x.ParentDepartmentId)
            .HasColumnName("parent_department_id")
            .HasComment("идентификатор родительского отдела");

        builder.Property(x => x.EmployeeDepartmentId)
            .HasColumnName("employee_department_id");

        builder.HasMany(x => x.EmployeeDepartments)
            .WithOne(ed => ed.Department)
            .HasForeignKey(x => x.DepartmentId);

        SeedData(builder);
    }

    private void SeedData(EntityTypeBuilder<Department> builder)
    {
        var financeDepartmentId = Guid.Parse("9ab46f93-6e3c-4e15-b79e-190e1105c33d");

        var departments = new[]
        {
            new Department
            {
                Id = financeDepartmentId,
                Name = "Финансовый",
                ParentDepartmentId = null,
                ParentDepartment = null,
                EmployeeDepartmentId = null
            },
            new Department
            {
                Id = Guid.Parse("0fb37d5b-04bd-478a-841d-79f954be6528"),
                Name = "Логистики",
                ParentDepartmentId = financeDepartmentId,
                EmployeeDepartmentId = null
            },
            new Department
            {
                Id = Guid.Parse("ede94df8-b101-452e-bddc-fe68431622ee"),
                Name = "Закупок",
                ParentDepartmentId = financeDepartmentId,
                EmployeeDepartmentId = null
            },
            new Department
            {
                Id = Guid.Parse("61ce4735-4c00-414e-955d-1363d27a297f"),
                Name = "Кадров",
                ParentDepartmentId = financeDepartmentId,
                EmployeeDepartmentId = null
            },
            new Department
            {
                Id = Guid.Parse("223a57cf-4650-4ec7-b39f-0970837d6769"),
                Name = "Развлечений",
                ParentDepartmentId = null,
                ParentDepartment = null,
                EmployeeDepartmentId = null
            }
        };
        
        builder.HasData(departments);
    }
}