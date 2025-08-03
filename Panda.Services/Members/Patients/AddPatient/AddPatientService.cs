using Panda.Domain;
using Panda.EntityFramework;
using Panda.Library.Class.Patient;

namespace Panda.Services.Members.Patients.AddPatient;

public class AddPatientService(IDatabaseContext databaseContext) : IAddPatientService
{
    public async Task<Guid> AddPatientAsync(AddPatientDto request, CancellationToken cancellationToken)
    {
        var patient = new Patient
        {
            Name = request.name,
            DateOfBirth = request.dateOfBirth,
            Postcode = request.postcode
        };

        await databaseContext.Patients.AddAsync(patient);
        await databaseContext.SaveChangesAsync(cancellationToken);

        return patient.Id;
    }
}
