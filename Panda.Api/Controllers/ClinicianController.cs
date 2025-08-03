using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Panda.Library.Class.Clinician;
using Panda.Services.Members.Clinicians.AddClinician;

namespace Panda.Api.Controllers;

[ApiController]
[Route("clinician")]
public class ClinicianController : ControllerBase
{
    private readonly IAddClinicianService _addClinicionService;

    public ClinicianController(IAddClinicianService addClinicionService)
    {
        _addClinicionService = addClinicionService;
    }

    /// <summary>
    /// Adds a new clinician to the database.
    /// </summary>
    /// <param name="request">The request containing the appointment details.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    [HttpPost]
    public async Task<Results<BadRequest, UnauthorizedHttpResult, Ok<Guid>>> AddClinicion([FromBody] AddClinicianDto request, CancellationToken cancellationToken)
    {
        var ClinitionId = await _addClinicionService.AddClinicianAsync(request, cancellationToken).ConfigureAwait(false);
        return TypedResults.Ok(ClinitionId);
    }
}
