using Panda.Library.Class.Patient;

namespace Panda.Services.Members.Patients.AddPatient;

public interface IAddPatientService
{
    /// <summary>
    /// Adds a new patient to the database.
    /// </summary>
    /// <param name="request">The details of the patient to add.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The ID of the newly added patient.</returns>
    Task<Guid> AddPatientAsync(AddPatientDto request, CancellationToken cancellationToken);
}
