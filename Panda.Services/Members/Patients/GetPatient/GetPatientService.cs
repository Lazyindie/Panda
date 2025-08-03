using Microsoft.EntityFrameworkCore;
using Panda.EntityFramework;
using Panda.Library.Class.Patient;

namespace Panda.Services.Members.Patients.GetPatient;

public class GetPatientService(IDatabaseContext databaseContext) : IGetPatientService
{
    public async Task<PatientDto> GetPatientAsync(Guid patientId, CancellationToken cancellationToken)
    {
        var patient = await databaseContext.Patients
            .AsNoTracking()
            .FirstOrDefaultAsync((patient) => patient.Id == patientId && patient.DeletedAt == null, cancellationToken);

        if (patient is null)
        {
            throw new KeyNotFoundException($"Patient with ID {patientId} not found.");
        }

        return new PatientDto(patient.Name, patient.DateOfBirth, patient.Postcode); 
    }
}
