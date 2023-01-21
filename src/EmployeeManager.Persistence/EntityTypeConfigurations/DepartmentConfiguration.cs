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

        builder.HasMany(x => x.EmployeeDepartments)
            .WithOne(ed => ed.Department)
            .HasForeignKey(x => x.DepartmentId);
        builder.HasOne(x => x.ParentDepartment)
            .WithOne(d => d.ParentDepartment);
        
        SeedData(builder);
    }

    private void SeedData(EntityTypeBuilder<Department> builder)
    {
        var financeDepartment = new Department
        {
            Id = Guid.NewGuid(),
            Name = "Финансовый",
            ParentDepartmentId = null,
            ParentDepartment = null
        };

        var departments = new[]
        {
            new Department
            {
                Id = Guid.NewGuid(),
                Name = "Логистики",
                ParentDepartmentId = financeDepartment.Id,
                ParentDepartment = financeDepartment,
            },
            new Department
            {
                Id = Guid.NewGuid(),
                Name = "Закупок",
                ParentDepartmentId = financeDepartment.Id,
                ParentDepartment = financeDepartment
            },
            new Department
            {
                Id = Guid.NewGuid(),
                Name = "Кадров",
                ParentDepartmentId = financeDepartment.Id,
                ParentDepartment = financeDepartment
            },
            new Department
            {
                Id = Guid.NewGuid(),
                Name = "Развлечений",
                ParentDepartmentId = null,
                ParentDepartment = null
            }
        };
        
        builder.HasData(departments);
    }
}