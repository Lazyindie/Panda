using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Panda.Library.Class.Appointment;
using Panda.Services.Appointments.AddAppointment;
using Panda.Services.Appointments.UpdateAppointmentStatus;

namespace Panda.Api.Controllers;

[ApiController]
[Route("appointment")]
public class AppointmentController : ControllerBase
{
    private readonly IAddAppointmentService _addClinitionService;
    private readonly IEditAppointmentService _updateAppointmentService;

    public AppointmentController(IAddAppointmentService addClinitionService, IEditAppointmentService updateAppointmentService)
    {
        _addClinitionService = addClinitionService;
        _updateAppointmentService = updateAppointmentService;
    }

    /// <summary>
    /// Adds a new appointment to the database.
    /// </summary>
    /// <param name="request">The request containing the appointment details.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The Id of the new appointment.</returns>
    [HttpPost]
    public async Task<Results<BadRequest, UnauthorizedHttpResult, Ok<Guid>>> AddAppointment([FromBody] AddAppointmentDto request, CancellationToken cancellationToken)
    {
        var ClinitionId = await _addClinitionService.AddAppointmentAsync(request, cancellationToken).ConfigureAwait(false);
        return TypedResults.Ok(ClinitionId);
    }

    /// <summary>
    /// Updates an existing appointment in the database.
    /// </summary>
    /// <param name="request">The request containing the new appointment details.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    [HttpPut]
    public async Task<Results<BadRequest, UnauthorizedHttpResult, Ok>> UpdateAppointment([FromBody] EditAppointmentDto request, CancellationToken cancellationToken)
    {
        await _updateAppointmentService.EditAppointmentAsync(request, cancellationToken).ConfigureAwait(false);
        return TypedResults.Ok();
    }
}
