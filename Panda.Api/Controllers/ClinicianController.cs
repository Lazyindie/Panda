using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Panda.Library.Class.Clinician;
using Panda.Services.Members.Clinicians.AddClinician;
using Panda.Services.Members.Clinicians.GetClinicians;

namespace Panda.Api.Controllers;

[ApiController]
[Route("clinician")]
public class ClinicianController : ControllerBase
{
    private readonly IAddClinicianService _addClinicionService;
    private readonly IGetCliniciansService _getCliniciansService;

    public ClinicianController(IAddClinicianService addClinicionService, IGetCliniciansService getCliniciansService)
    {
        _addClinicionService = addClinicionService;
        _getCliniciansService = getCliniciansService;
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

    [HttpGet]
    public async Task<Results<BadRequest, UnauthorizedHttpResult, Ok<IEnumerable<ClinicianDto>>>> GetClinicians(CancellationToken cancellationToken)
    {
        var clinicians = await _getCliniciansService.GetCliniciansAsync(cancellationToken).ConfigureAwait(false);
        return TypedResults.Ok(clinicians);
    }
}
