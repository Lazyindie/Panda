using Microsoft.EntityFrameworkCore;
using Panda.EntityFramework;
using Panda.Library.Class.Patient;

namespace Panda.Services.Members.Patients.EditPatient;

public class EditPatientService(IDatabaseContext databaseContext) : IEditPatientService
{
    public async Task EditPatient(EditPatientDto request, CancellationToken cancellationToken)
    {
        var patient = await databaseContext.Patients.FirstOrDefaultAsync((patient) => patient.Id == request.id && patient.DeletedAt == null, cancellationToken);

        if (patient is null)
        {
            throw new KeyNotFoundException($"Patient with ID {request.id} not found.");
        }

        // Update patient properties with the provided values, if they are not null
        patient.Name = request.name ?? patient.Name;
        patient.DateOfBirth = request.dateOfBirth ?? patient.DateOfBirth;
        patient.Postcode = request.postcode ?? patient.Postcode;

        await databaseContext.SaveChangesAsync(cancellationToken);
    }
}
