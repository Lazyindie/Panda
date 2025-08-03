using Panda.Library.Class.Patient;

namespace Panda.Services.Members.Patients.GetPatient;

public interface IGetPatientService
{
    Task<PatientDto> GetPatientAsync(Guid patientId, CancellationToken cancellationToken);
}
