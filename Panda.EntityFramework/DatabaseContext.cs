using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Panda.Domain;
using Panda.Domain.Common;
using Panda.EntityFramework.ModelConfiguration;

namespace Panda.EntityFramework;

public class DatabaseContext : DbContext, IDatabaseContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
    {
    }

    public DbSet<Patient> Patients { get; set; } = default!;

    public DbSet<Clinician> Clinicians { get; set; } = default!;

    public DbSet<Department> Departments { get; set; } = default!;

    public DbSet<Appointment> Appointments { get; set; } = default!;


    /// <summary>
    /// Applies model configuration.
    /// </summary>
    /// <param name="modelBuilder">The model builder.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Get the assembly of (any) configuration class to get the assembly information.
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppointmentConfiguration).Assembly);
    }

    /// <summary>
    /// Save changes and update auditing information.
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var modified = DateTimeOffset.Now;
        var changeSet = ChangeTracker.Entries<IAuditable>();

        foreach (var entry in changeSet.Where(c => c.State != EntityState.Unchanged))
        {
            entry.Entity.ModifiedAt = modified;

            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedAt = modified;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}
