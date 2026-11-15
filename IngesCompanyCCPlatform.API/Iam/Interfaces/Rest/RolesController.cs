using System.Net.Mime;
using IngesCompanyCCPlatform.API.Iam.Application.CommandServices;
using IngesCompanyCCPlatform.API.Iam.Application.QueryServices;
using IngesCompanyCCPlatform.API.Iam.Domain.Model.Queries;
using IngesCompanyCCPlatform.API.Iam.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Iam.Infrastructure.Pipeline.Middleware.Attributes;
using IngesCompanyCCPlatform.API.Iam.Interfaces.Rest.Resources;
using IngesCompanyCCPlatform.API.Iam.Interfaces.Rest.Transform;
using IngesCompanyCCPlatform.API.Shared.Interfaces.Rest.ProblemDetails;
using Microsoft.AspNetCore.Mvc;

namespace IngesCompanyCCPlatform.API.Iam.Interfaces.Rest;

/// <summary>
///     The five roles of the platform.
/// </summary>
[Authorize]
[ApiController]
[Route("api/v1/roles")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Roles")]
public class RolesController : ControllerBase
{
    [HttpGet]
    [EndpointSummary("Get all roles")]
    [ProducesResponseType(typeof(IEnumerable<RoleResource>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public IActionResult GetAllRoles()
    {
        return Ok(Enum.GetValues<Roles>().Select((role, index) => new RoleResource(index + 1, role.ToName())));
    }
}
