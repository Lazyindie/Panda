namespace Panda.Services.Members.Patients.RemovePatient;

public interface IRemovePatientService
{
    public Task RemovePatientAsync(Guid patientId, CancellationToken cancellationToken = default);
}
