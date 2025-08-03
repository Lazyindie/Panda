using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Panda.Domain;

namespace Panda.EntityFramework.ModelConfiguration;

public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
{
    /// <summary>
    /// Configures the <see cref="Department"/> entity.
    /// </summary>
    /// <param name="builder">The model configuration builder.</param>
    public void Configure(EntityTypeBuilder<Department> builder)
    {
        builder.ToTable($"{nameof(Department)}s", "Panda");

        builder.HasKey(d => d.Id);

        builder.Property((department) => department.Name)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property((department) => department.Description)
            .IsRequired()
            .HasMaxLength(500);

        builder.HasMany(d => d.Clinicians)
            .WithOne(c => c.Department)
            .HasForeignKey(c => c.DepartmentId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(d => d.Appointments)
            .WithOne(a => a.Department)
            .HasForeignKey(a => a.DepartmentId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
