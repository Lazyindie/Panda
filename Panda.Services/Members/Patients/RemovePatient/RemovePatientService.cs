using Panda.EntityFramework;

namespace Panda.Services.Members.Patients.RemovePatient;

public class RemovePatientService(IDatabaseContext databaseContext) : IRemovePatientService
{
    public Task RemovePatientAsync(Guid patientId, CancellationToken cancellationToken = default)
    {
        var patient = databaseContext.Patients.FirstOrDefault(p => p.Id == patientId);
        if (patient == null)
        {
            throw new KeyNotFoundException($"Patient with ID {patientId} not found.");
        }

        if (patient.DeletedAt == null)
        {
            return Task.CompletedTask;
        }

        patient.DeletedAt = DateTime.UtcNow;

        return databaseContext.SaveChangesAsync(cancellationToken);
    }
}
