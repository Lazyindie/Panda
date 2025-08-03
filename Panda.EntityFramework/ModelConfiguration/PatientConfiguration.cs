using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Panda.Domain;

namespace Panda.EntityFramework.ModelConfiguration;

public class PatientConfiguration : IEntityTypeConfiguration<Patient>
{
    /// <summary>
    /// Configures the <see cref="Patient"/> entity.
    /// </summary>
    /// <param name="builder">The model configuration builder.</param>
    public void Configure(EntityTypeBuilder<Patient> builder)
    {
        builder.ToTable($"{nameof(Patient)}s", "Panda");

        builder.HasKey(patient => patient.Id);

        builder.Property((patient => patient.Name))
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(patient => patient.DateOfBirth)
            .IsRequired();

        builder.Property((patient) => patient.Postcode)
            .IsRequired()
            .HasMaxLength(8);

        builder.HasMany((patient) => patient.Appointments)
            .WithOne((appointment) => appointment.Patient)
            .HasForeignKey((appointment) => appointment.PatientId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
