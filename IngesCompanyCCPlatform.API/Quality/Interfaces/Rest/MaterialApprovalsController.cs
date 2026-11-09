using System.Net.Mime;
using IngesCompanyCCPlatform.API.Quality.Application.CommandServices;
using IngesCompanyCCPlatform.API.Quality.Application.QueryServices;
using IngesCompanyCCPlatform.API.Quality.Domain.Model;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Errors;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Queries;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Filters;
using IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Resources;
using IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Transform;
using IngesCompanyCCPlatform.API.Shared.Application.Model;
using IngesCompanyCCPlatform.API.Shared.Interfaces.Rest.ProblemDetails;
using Microsoft.AspNetCore.Mvc;

namespace IngesCompanyCCPlatform.API.Quality.Interfaces.Rest;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Material Approvals")]
[QualityInvalidDataFilter]
public class MaterialApprovalsController(
    IMaterialApprovalCommandService materialApprovalCommandService,
    IMaterialApprovalQueryService materialApprovalQueryService,
    ProblemDetailsFactory problemDetailsFactory) : ControllerBase
{
    [HttpGet]
    [EndpointSummary("Get the material approvals")]
    [EndpointDescription("Gets all the material approvals, or the one of a raw material lot with ?rawMaterialLotId=.")]
    [ProducesResponseType(typeof(IEnumerable<MaterialApprovalResource>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetMaterialApprovals([FromQuery] int? rawMaterialLotId,
        CancellationToken cancellationToken)
    {
        if (rawMaterialLotId is not null)
        {
            var approval = await materialApprovalQueryService.Handle(
                new GetMaterialApprovalByRawMaterialLotIdQuery(new RawMaterialLotId(rawMaterialLotId.Value)),
                cancellationToken);
            var approvals = approval is null ? [] : new[] { approval };
            return Ok(approvals.Select(MaterialApprovalResourceFromEntityAssembler.ToResourceFromEntity));
        }

        var all = await materialApprovalQueryService.Handle(new GetAllMaterialApprovalsQuery(), cancellationToken);
        return Ok(all.Select(MaterialApprovalResourceFromEntityAssembler.ToResourceFromEntity));
    }

    [HttpGet("{materialApprovalId:int}")]
    [EndpointSummary("Get a material approval by id")]
    [ProducesResponseType(typeof(MaterialApprovalResource), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetMaterialApprovalById(int materialApprovalId,
        CancellationToken cancellationToken)
    {
        var approval = await materialApprovalQueryService.Handle(new GetMaterialApprovalByIdQuery(materialApprovalId),
            cancellationToken);
        return QualityActionResultAssembler.ToActionResultFromQuery(this, approval, problemDetailsFactory,
            QualityError.MaterialApprovalNotFound, QualityErrors.MaterialApprovalNotFound(materialApprovalId).Message,
            found => Ok(MaterialApprovalResourceFromEntityAssembler.ToResourceFromEntity(found)));
    }

    [HttpPost]
    [EndpointSummary("Request the approval of a raw material lot")]
    [ProducesResponseType(typeof(MaterialApprovalResource), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> RequestMaterialApproval([FromBody] RequestMaterialApprovalResource resource,
        CancellationToken cancellationToken)
    {
        var command = MaterialApprovalCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await materialApprovalCommandService.Handle(command, cancellationToken);
        return QualityActionResultAssembler.ToActionResultFromResult(this, result, problemDetailsFactory,
            created => CreatedAtAction(nameof(GetMaterialApprovalById), new { materialApprovalId = created.Id.Value },
                MaterialApprovalResourceFromEntityAssembler.ToResourceFromEntity(created)));
    }

    [HttpPost("{materialApprovalId:int}/samples")]
    [EndpointSummary("Register the sample of a raw material lot")]
    [ProducesResponseType(typeof(MaterialApprovalResource), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> RegisterMaterialSample(int materialApprovalId,
        [FromBody] RegisterMaterialSampleResource resource, CancellationToken cancellationToken)
    {
        var command = MaterialApprovalCommandFromResourceAssembler.ToCommandFromResource(materialApprovalId, resource);
        return ToOk(await materialApprovalCommandService.Handle(command, cancellationToken));
    }

    [HttpPost("{materialApprovalId:int}/approval")]
    [EndpointSummary("Approve a sampled raw material lot")]
    [ProducesResponseType(typeof(MaterialApprovalResource), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> ApproveMaterial(int materialApprovalId,
        [FromBody] ApproveMaterialResource resource, CancellationToken cancellationToken)
    {
        var command = MaterialApprovalCommandFromResourceAssembler.ToCommandFromResource(materialApprovalId, resource);
        return ToOk(await materialApprovalCommandService.Handle(command, cancellationToken));
    }

    [HttpPost("{materialApprovalId:int}/rejection")]
    [EndpointSummary("Reject a raw material lot")]
    [ProducesResponseType(typeof(MaterialApprovalResource), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> RejectMaterial(int materialApprovalId,
        [FromBody] RejectMaterialResource resource, CancellationToken cancellationToken)
    {
        var command = MaterialApprovalCommandFromResourceAssembler.ToCommandFromResource(materialApprovalId, resource);
        return ToOk(await materialApprovalCommandService.Handle(command, cancellationToken));
    }

    private IActionResult ToOk(Result<MaterialApproval> result)
    {
        return QualityActionResultAssembler.ToActionResultFromResult(this, result, problemDetailsFactory,
            approval => Ok(MaterialApprovalResourceFromEntityAssembler.ToResourceFromEntity(approval)));
    }
}
