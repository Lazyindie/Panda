using Panda.Library.Class.Clinician;

namespace Panda.Services.Members.Clinicians.AddClinician;

public interface IAddClinicianService
{
    public Task<Guid> AddClinicianAsync(AddClinicianDto clinician, CancellationToken cancellationToken);
}
