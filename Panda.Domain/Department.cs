using Panda.Domain.Common;

namespace Panda.Domain; 

public class Department : Auditable
{
    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public virtual ICollection<Clinician> Clinicians { get; set; } = new List<Clinician>();

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
}