using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Panda.Domain;

namespace Panda.EntityFramework.ModelConfiguration;

public class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
{
    /// <summary>
    /// Configures the <see cref="Appointment"/> entity.
    /// </summary>
    /// <param name="builder">The model configuration builder.</param>
    public void Configure(EntityTypeBuilder<Appointment> builder)
    {
        builder.ToTable($"{nameof(Appointment)}s", "Panda");

        builder.HasKey(patient => patient.Id);

        builder.Property((appointment) => appointment.Time)
            .IsRequired();

        builder.Property((appointment) => appointment.Duration)
            .IsRequired();
    }
}
