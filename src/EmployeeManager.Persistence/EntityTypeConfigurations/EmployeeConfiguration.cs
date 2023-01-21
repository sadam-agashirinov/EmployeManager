using EmployeeManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeeManager.Persistence.EntityTypeConfigurations;

public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.ToTable("employee", tableBuilder =>
        {
            tableBuilder.HasComment("информация о сотруднике");
        });

        builder.HasKey(x => x.Id).HasName("первичный ключь");
        builder.HasIndex(x => x.Id, "pk_employee_id")
            .IsUnique();

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .IsRequired()
            .HasComment("идентификатор");

        builder.Property(x => x.LastName)
            .HasColumnName("last_name")
            .IsRequired()
            .HasMaxLength(100)
            .HasComment("фамилия");
        
        builder.Property(x => x.FirstName)
            .HasColumnName("first_name")
            .IsRequired()
            .HasMaxLength(100)
            .HasComment("имя");

        builder.Property(x => x.Patronymic)
            .HasColumnName("patronymic")
            .IsRequired()
            .HasMaxLength(100)
            .HasComment("отчество");

        builder.Property(x => x.Email)
            .HasColumnName("email")
            .IsRequired()
            .HasMaxLength(255)
            .HasComment("адрес электронной почты");

        builder.Property(x => x.Salary)
            .HasColumnName("salary")
            .IsRequired()
            .HasComment("оклад");

        builder.HasMany(x => x.EmployeeDepartments)
            .WithOne(ed => ed.Employee)
            .HasForeignKey(x=>x.EmployeeId);
    }
}