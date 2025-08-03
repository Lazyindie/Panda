using Panda.Library.Class.Clinician;

namespace Panda.Services.Members.Clinicians.GetClinicians;

public interface IGetCliniciansService
{
    public Task<IEnumerable<ClinicianDto>> GetCliniciansAsync(CancellationToken cancellationToken = default);
}
