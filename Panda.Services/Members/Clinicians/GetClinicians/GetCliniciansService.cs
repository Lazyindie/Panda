using Microsoft.EntityFrameworkCore;
using Panda.EntityFramework;
using Panda.Library.Class.Clinician;

namespace Panda.Services.Members.Clinicians.GetClinicians;

public class GetCliniciansService(IDatabaseContext databaseContext) : IGetCliniciansService
{
    public async Task<IEnumerable<ClinicianDto>> GetCliniciansAsync(CancellationToken cancellationToken = default)
    {
        return await databaseContext.Clinicians
            .Include((clinician) => clinician.Department)
            .AsNoTracking()
            .Where((clinician) => clinician.DeletedAt == null && clinician.Department.DeletedAt == null)
            .Select(clinician => new ClinicianDto(clinician.Id, clinician.Name, clinician.DateOfBirth, clinician.Department.Name))
            .ToListAsync();
    }
}
