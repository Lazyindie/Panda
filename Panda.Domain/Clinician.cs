using Panda.Domain.Common;
using Panda.Library.Class;

namespace Panda.Domain;

/// <summary>
/// Represents a clinician in the healthcare system.
/// </summary>
public class Clinician : Auditable
{
    public Guid DepartmentId { get; set; } 

    public string Name { get; set; } = string.Empty;

    public DateOnly DateOfBirth { get; set; }

    public virtual Department Department { get; set; } = default!;

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
}