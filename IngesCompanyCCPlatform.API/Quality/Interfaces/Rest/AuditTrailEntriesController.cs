using System.Net.Mime;
using IngesCompanyCCPlatform.API.Quality.Application.CommandServices;
using IngesCompanyCCPlatform.API.Quality.Application.QueryServices;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Queries;
using IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Filters;
using IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Resources;
using IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Transform;
using IngesCompanyCCPlatform.API.Shared.Interfaces.Rest.ProblemDetails;
using Microsoft.AspNetCore.Mvc;

namespace IngesCompanyCCPlatform.API.Quality.Interfaces.Rest;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Audit Trail")]
[QualityInvalidDataFilter]
public class AuditTrailEntriesController(
    IAuditTrailEntryCommandService auditTrailEntryCommandService,
    IAuditTrailEntryQueryService auditTrailEntryQueryService,
    ProblemDetailsFactory problemDetailsFactory) : ControllerBase
{
    [HttpGet]
    [EndpointSummary("Get the audit trail")]
    [EndpointDescription(
        "Gets the whole audit trail, or the trail of one record with ?entityType=&entityId=. Newest first.")]
    [ProducesResponseType(typeof(IEnumerable<AuditTrailEntryResource>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAuditTrailEntries([FromQuery] string? entityType, [FromQuery] string? entityId,
        CancellationToken cancellationToken)
    {
        var entries = string.IsNullOrWhiteSpace(entityType) || string.IsNullOrWhiteSpace(entityId)
            ? await auditTrailEntryQueryService.Handle(new GetAllAuditTrailEntriesQuery(), cancellationToken)
            : await auditTrailEntryQueryService.Handle(new GetAuditTrailEntriesByEntityQuery(entityType, entityId),
                cancellationToken);
        return Ok(entries.Select(AuditTrailEntryResourceFromEntityAssembler.ToResourceFromEntity));
    }

    [HttpPost]
    [EndpointSummary("Record a change in the audit trail")]
    [EndpointDescription("The audit trail is insert only: its entries cannot be changed or deleted.")]
    [ProducesResponseType(typeof(AuditTrailEntryResource), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RecordAuditTrailEntry([FromBody] RecordAuditTrailEntryResource resource,
        CancellationToken cancellationToken)
    {
        var command = RecordAuditTrailEntryCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await auditTrailEntryCommandService.Handle(command, cancellationToken);
        return QualityActionResultAssembler.ToActionResultFromResult(this, result, problemDetailsFactory,
            created => StatusCode(StatusCodes.Status201Created,
                AuditTrailEntryResourceFromEntityAssembler.ToResourceFromEntity(created)));
    }
}
