using Panda.Domain.Common;

namespace Panda.Domain;

public class Patient : Auditable
{
    public string Name { get; set; } = string.Empty;

    public DateOnly DateOfBirth { get; set; }

    public string Postcode { get; set; } = string.Empty;

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
}