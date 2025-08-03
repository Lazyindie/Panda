using Panda.Library.Class.Patient;

namespace Panda.Services.Members.Patients.EditPatient;

public interface IEditPatientService
{
    public Task EditPatient(EditPatientDto request, CancellationToken cancellationToken);
}
