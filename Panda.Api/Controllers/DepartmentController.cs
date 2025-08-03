using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Panda.Library.Class.Department;
using Panda.Services.Department.AddDepartment;

namespace Panda.Api.Controllers;

[ApiController]
[Route("department")]
public class DepartmentController : ControllerBase
{
    private readonly IAddDepartmentService _addDepartmentService;

    public DepartmentController(IAddDepartmentService addClinitionService)
    {
        _addDepartmentService = addClinitionService;
    }

    /// <summary>
    /// Adds a new department to the database.
    /// </summary>
    /// <param name="request">The request containing the department details.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The Id of the new department.</returns>
    [HttpPost]
    public async Task<Results<BadRequest, UnauthorizedHttpResult, Ok<Guid>>> AddDepartment([FromBody] AddDepartmentDto request, CancellationToken cancellationToken)
    {
        var ClinitionId = await _addDepartmentService.AddDepartmentAsync(request, cancellationToken).ConfigureAwait(false);
        return TypedResults.Ok(ClinitionId);
    }
}
