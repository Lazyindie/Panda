using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Panda.Domain;
using Panda.Library.Class.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Panda.EntityFramework.ModelConfiguration;

public class ClinicianConfiguration : IEntityTypeConfiguration<Clinician>
{
    /// <summary>
    /// Configures the <see cref="Clinician"/> entity.
    /// </summary>
    /// <param name="builder">The model configuration builder.</param>
    public void Configure(EntityTypeBuilder<Clinician> builder)
    {
        builder.ToTable($"{nameof(Clinician)}s", "Panda");

        builder.HasKey((clinician) => clinician.Id);

        builder.Property((clinician) => clinician.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property((clinician) => clinician.DateOfBirth)
            .IsRequired();

        builder.HasMany((clinician) => clinician.Appointments)
            .WithOne((appointment) => appointment.Clinician)
            .HasForeignKey((appointment) => appointment.ClinicianId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
