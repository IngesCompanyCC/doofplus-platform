using System.Net.Mime;
using IngesCompanyCCPlatform.API.Quality.Application.CommandServices;
using IngesCompanyCCPlatform.API.Quality.Application.QueryServices;
using IngesCompanyCCPlatform.API.Quality.Domain.Model;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Errors;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Queries;
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
[Tags("Quality Documents")]
[QualityInvalidDataFilter]
public class QualityDocumentsController(
    IQualityDocumentCommandService qualityDocumentCommandService,
    IQualityDocumentQueryService qualityDocumentQueryService,
    ProblemDetailsFactory problemDetailsFactory) : ControllerBase
{
    [HttpGet]
    [EndpointSummary("Get all quality documents")]
    [ProducesResponseType(typeof(IEnumerable<QualityDocumentResource>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllQualityDocuments(CancellationToken cancellationToken)
    {
        var documents = await qualityDocumentQueryService.Handle(new GetAllQualityDocumentsQuery(), cancellationToken);
        return Ok(documents.Select(QualityDocumentResourceFromEntityAssembler.ToResourceFromEntity));
    }

    [HttpGet("{qualityDocumentId:int}")]
    [EndpointSummary("Get a quality document by id")]
    [ProducesResponseType(typeof(QualityDocumentResource), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetQualityDocumentById(int qualityDocumentId, CancellationToken cancellationToken)
    {
        var document = await qualityDocumentQueryService.Handle(new GetQualityDocumentByIdQuery(qualityDocumentId),
            cancellationToken);
        return QualityActionResultAssembler.ToActionResultFromQuery(this, document, problemDetailsFactory,
            QualityError.QualityDocumentNotFound, QualityErrors.QualityDocumentNotFound(qualityDocumentId).Message,
            found => Ok(QualityDocumentResourceFromEntityAssembler.ToResourceFromEntity(found)));
    }

    [HttpPost]
    [EndpointSummary("Create a quality document")]
    [EndpointDescription("Creates the document in draft with its version 1.0.")]
    [ProducesResponseType(typeof(QualityDocumentResource), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> CreateQualityDocument([FromBody] CreateQualityDocumentResource resource,
        CancellationToken cancellationToken)
    {
        var command = QualityDocumentCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await qualityDocumentCommandService.Handle(command, cancellationToken);
        return QualityActionResultAssembler.ToActionResultFromResult(this, result, problemDetailsFactory,
            created => CreatedAtAction(nameof(GetQualityDocumentById), new { qualityDocumentId = created.Id.Value },
                QualityDocumentResourceFromEntityAssembler.ToResourceFromEntity(created)));
    }

    [HttpPost("{qualityDocumentId:int}/review-submission")]
    [EndpointSummary("Submit a quality document for review")]
    [ProducesResponseType(typeof(QualityDocumentResource), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> SubmitQualityDocumentForReview(int qualityDocumentId,
        CancellationToken cancellationToken)
    {
        var result = await qualityDocumentCommandService.Handle(
            new SubmitQualityDocumentForReviewCommand(qualityDocumentId), cancellationToken);
        return ToOk(result);
    }

    [HttpPost("{qualityDocumentId:int}/approval")]
    [EndpointSummary("Approve a quality document")]
    [EndpointDescription("Approves the current version with an electronic signature. The author cannot approve it.")]
    [ProducesResponseType(typeof(QualityDocumentResource), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> ApproveQualityDocument(int qualityDocumentId,
        [FromBody] ApproveQualityDocumentResource resource, CancellationToken cancellationToken)
    {
        var command = QualityDocumentCommandFromResourceAssembler.ToCommandFromResource(qualityDocumentId, resource);
        return ToOk(await qualityDocumentCommandService.Handle(command, cancellationToken));
    }

    [HttpPost("{qualityDocumentId:int}/rejection")]
    [EndpointSummary("Reject a quality document in review")]
    [ProducesResponseType(typeof(QualityDocumentResource), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> RejectQualityDocument(int qualityDocumentId,
        [FromBody] RejectQualityDocumentResource resource, CancellationToken cancellationToken)
    {
        var command = QualityDocumentCommandFromResourceAssembler.ToCommandFromResource(qualityDocumentId, resource);
        return ToOk(await qualityDocumentCommandService.Handle(command, cancellationToken));
    }

    [HttpPost("{qualityDocumentId:int}/versions")]
    [EndpointSummary("Publish a new version of a quality document")]
    [ProducesResponseType(typeof(QualityDocumentResource), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> PublishDocumentVersion(int qualityDocumentId,
        [FromBody] PublishDocumentVersionResource resource, CancellationToken cancellationToken)
    {
        var command = QualityDocumentCommandFromResourceAssembler.ToCommandFromResource(qualityDocumentId, resource);
        return ToOk(await qualityDocumentCommandService.Handle(command, cancellationToken));
    }

    private IActionResult ToOk(Result<QualityDocument> result)
    {
        return QualityActionResultAssembler.ToActionResultFromResult(this, result, problemDetailsFactory,
            document => Ok(QualityDocumentResourceFromEntityAssembler.ToResourceFromEntity(document)));
    }
}
