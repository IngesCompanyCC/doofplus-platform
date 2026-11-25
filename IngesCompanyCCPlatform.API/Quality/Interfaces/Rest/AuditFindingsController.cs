using System.Net.Mime;
using IngesCompanyCCPlatform.API.Quality.Application.CommandServices;
using IngesCompanyCCPlatform.API.Quality.Application.QueryServices;
using IngesCompanyCCPlatform.API.Quality.Domain.Model;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Errors;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Queries;
using IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Resources;
using IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Transform;
using IngesCompanyCCPlatform.API.Quality.Resources;
using IngesCompanyCCPlatform.API.Shared.Interfaces.Rest.ProblemDetails;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using IngesCompanyCCPlatform.API.Iam.Infrastructure.Pipeline.Middleware.Attributes;

namespace IngesCompanyCCPlatform.API.Quality.Interfaces.Rest;

/// <summary>
///     REST controller of the audit findings of Quality &amp; Compliance.
/// </summary>
[ApiController]
[Authorize]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Audit Findings")]
public class AuditFindingsController(
    IAuditFindingCommandService auditFindingCommandService,
    IAuditFindingQueryService auditFindingQueryService,
    IStringLocalizer<QualityMessages> messages,
    ProblemDetailsFactory problemDetailsFactory)
    : ControllerBase
{
    [HttpPost]
    [EndpointSummary("Create a audit finding")]
    [EndpointDescription("Creates a audit finding.")]
    [ProducesResponseType(typeof(AuditFindingResource), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateAuditFinding([FromBody] CreateAuditFindingResource resource, CancellationToken cancellationToken)
    {
        var command = AuditFindingCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await auditFindingCommandService.Handle(command, cancellationToken);
        return QualityActionResultAssembler.ToActionResultFromCommandResult(this, result, messages, problemDetailsFactory,
            created => CreatedAtAction(nameof(GetAuditFindingById), new { auditFindingId = created.Id.Value },
                AuditFindingResourceFromEntityAssembler.ToResourceFromEntity(created)));
    }

    [HttpGet]
    [EndpointSummary("Get the audit findings")]
    [EndpointDescription("Gets the audit findings; with auditCode, only the ones with that audit code.")]
    [ProducesResponseType(typeof(IEnumerable<AuditFindingResource>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAuditFindings([FromQuery] string? auditCode, CancellationToken cancellationToken)
    {
        var items = await auditFindingQueryService.Handle(new GetAllAuditFindingsQuery(auditCode), cancellationToken);
        return Ok(items.Select(AuditFindingResourceFromEntityAssembler.ToResourceFromEntity));
    }

    [HttpGet("{auditFindingId:int}")]
    [EndpointSummary("Get a audit finding by its id")]
    [EndpointDescription("Gets a audit finding by the provided id.")]
    [ProducesResponseType(typeof(AuditFindingResource), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAuditFindingById([FromRoute] int auditFindingId, CancellationToken cancellationToken)
    {
        var item = await auditFindingQueryService.Handle(new GetAuditFindingByIdQuery(auditFindingId), cancellationToken);
        return QualityActionResultAssembler.ToActionResultFromQueryResult(this, item, AuditFindingErrors.NotFoundById(auditFindingId),
            QualityError.AuditFindingNotFound, messages, problemDetailsFactory,
            found => Ok(AuditFindingResourceFromEntityAssembler.ToResourceFromEntity(found)), auditFindingId);
    }
}
