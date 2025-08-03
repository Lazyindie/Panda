using Microsoft.EntityFrameworkCore;
using Panda.Domain;

namespace Panda.EntityFramework;

public interface IDatabaseContext
{
    public DbSet<Patient> Patients { get; set; }

    public DbSet<Clinician> Clinicians { get; set; }

    public DbSet<Department> Departments { get; set; }

    public DbSet<Appointment> Appointments { get; set; }

    /// <summary>
    /// Saves changes to the database.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous save operation.</returns>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
