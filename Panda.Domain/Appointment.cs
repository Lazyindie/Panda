using Panda.Domain.Common;
using Panda.Library.Class.Common;

namespace Panda.Domain;

public class Appointment : Auditable
{
    public Guid ClinicianId { get; set; }

    public Guid PatientId { get; set; }

    public Guid DepartmentId { get; set; }

    public AppointmentStatus Status { get; set; } = AppointmentStatus.Active;

    public DateTimeOffset Time { get; set; } = DateTimeOffset.Now;

    public TimeOnly Duration { get; set; } = TimeOnly.FromTimeSpan(TimeSpan.FromMinutes(15));

    public virtual Clinician Clinician { get; set; } = default!;

    public virtual Patient Patient { get; set; } = default!;

    public virtual Department Department { get; set; } = default!;
}