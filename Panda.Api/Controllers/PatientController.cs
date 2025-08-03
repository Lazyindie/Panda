using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Panda.Domain;
using Panda.Library.Class.Patient;
using Panda.Services.Members.Patients.AddPatient;
using Panda.Services.Members.Patients.EditPatient;
using Panda.Services.Members.Patients.GetPatient;

namespace Panda.Api.Controllers;

[ApiController]
[Route("patient")]
public class PatientController : ControllerBase
{
    private readonly IGetPatientService _getPatientService;
    private readonly IAddPatientService _addPatientService;
    private readonly IEditPatientService _editPatientService;

    public PatientController(IGetPatientService patientRepository, IAddPatientService addPatientService, IEditPatientService editPatientService)
    {
        _getPatientService = patientRepository;
        _addPatientService = addPatientService;
        _editPatientService = editPatientService;
    }

    /// <summary>
    /// Retrieves a patient by their unique identifier.
    /// </summary>
    /// <param name="Id">The id of the patient.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    [HttpGet]
    public async Task<Results<NotFound, UnauthorizedHttpResult, BadRequest, Ok<PatientDto>>> Get([FromQuery] Guid Id, CancellationToken cancellationToken)
    {
        return TypedResults.Ok(await _getPatientService.GetPatientAsync(Id, cancellationToken).ConfigureAwait(false));
    }

    /// <summary>
    /// Adds a new patient to the database.
    /// </summary>
    /// <param name="request">The request containing the patient details.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    [HttpPost]
    public async Task<Results<BadRequest, UnauthorizedHttpResult, Ok<Guid>>> AddPatient([FromBody] AddPatientDto request, CancellationToken cancellationToken)
    {
        var patientId = await _addPatientService.AddPatientAsync(request, cancellationToken).ConfigureAwait(false);
        return TypedResults.Ok(patientId);
    }

    /// <summary>
    /// Updates an existing patient's details in the database.
    /// </summary>
    /// <param name="request">The request containing the new patient details.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    [HttpPut]
    public async Task<Results<BadRequest, UnauthorizedHttpResult, Ok>> UpdatePatient([FromBody] EditPatientDto request, CancellationToken cancellationToken)
    {
        await _editPatientService.EditPatient(request, cancellationToken).ConfigureAwait(false);
        return TypedResults.Ok();
    }
}
