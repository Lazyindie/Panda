using Panda.Domain;
using Panda.EntityFramework;
using Panda.Library.Class.Clinician;

namespace Panda.Services.Members.Clinicians.AddClinician;

public class AddClinicianService(IDatabaseContext databaseContext) : IAddClinicianService
{
    public async Task<Guid> AddClinicianAsync(AddClinicianDto request, CancellationToken cancellationToken)
    {
        var department = await databaseContext.Departments.FindAsync(request.departmentId, cancellationToken);

        if (department is null)
        {
            throw new ArgumentException($"Department with ID {request.departmentId} does not exist.", nameof(request.departmentId));
        }

        var clinician = new Clinician
        {
            Name = request.name,
            DateOfBirth = request.dateOfBirth,
            DepartmentId = request.departmentId
        };

        await databaseContext.Clinicians.AddAsync(clinician, cancellationToken);
        await databaseContext.SaveChangesAsync();

        return clinician.Id;
    }
}
