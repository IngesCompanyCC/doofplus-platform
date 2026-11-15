using System.Net.Mime;
using IngesCompanyCCPlatform.API.Iam.Application.CommandServices;
using IngesCompanyCCPlatform.API.Iam.Application.QueryServices;
using IngesCompanyCCPlatform.API.Iam.Domain.Model;
using IngesCompanyCCPlatform.API.Iam.Domain.Model.Queries;
using IngesCompanyCCPlatform.API.Iam.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Iam.Infrastructure.Pipeline.Middleware.Attributes;
using IngesCompanyCCPlatform.API.Iam.Interfaces.Rest.Resources;
using IngesCompanyCCPlatform.API.Iam.Interfaces.Rest.Transform;
using IngesCompanyCCPlatform.API.Shared.Interfaces.Rest.ProblemDetails;
using Microsoft.AspNetCore.Mvc;

namespace IngesCompanyCCPlatform.API.Iam.Interfaces.Rest;

/// <summary>
///     Invitations of new members of the laboratory. Only administrators send and read them.
/// </summary>
[Authorize(Roles.RoleAdministrator)]
[ApiController]
[Route("api/v1/invitations")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Invitations")]
public class InvitationsController(
    IInvitationCommandService invitationCommandService,
    IInvitationQueryService invitationQueryService,
    ProblemDetailsFactory problemDetailsFactory) : ControllerBase
{
    [HttpPost]
    [EndpointSummary("Send an invitation")]
    [EndpointDescription("Invites a new member with a role and a facility; the invitation starts Pending. " +
                         "An email of a user who is no longer invited, or of a pending invitation, answers 409.")]
    [ProducesResponseType(typeof(InvitationResource), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> SendInvitation([FromBody] SendInvitationResource resource, CancellationToken cancellationToken)
    {
        var result = await invitationCommandService.Handle(
            SendInvitationCommandFromResourceAssembler.ToCommandFromResource(resource), cancellationToken);
        return IamActionResultAssembler.ToActionResult(this, result, problemDetailsFactory,
            invitation => CreatedAtAction(nameof(GetInvitationById), new { invitationId = invitation.Id },
                InvitationResourceFromEntityAssembler.ToResourceFromEntity(invitation)));
    }

    [HttpGet]
    [EndpointSummary("Get the invitations")]
    [EndpointDescription("Gets the invitations; with organizationId, only the ones of that organization.")]
    [ProducesResponseType(typeof(IEnumerable<InvitationResource>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> GetInvitations([FromQuery] int? organizationId, CancellationToken cancellationToken)
    {
        var invitations = await invitationQueryService.Handle(new GetAllInvitationsQuery(organizationId), cancellationToken);
        return Ok(invitations.Select(InvitationResourceFromEntityAssembler.ToResourceFromEntity));
    }

    [HttpGet("{invitationId:int}")]
    [EndpointSummary("Get an invitation by its id")]
    [ProducesResponseType(typeof(InvitationResource), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetInvitationById([FromRoute] int invitationId, CancellationToken cancellationToken)
    {
        var invitation = await invitationQueryService.Handle(new GetInvitationByIdQuery(invitationId), cancellationToken);
        return invitation is null
            ? problemDetailsFactory.CreateProblemDetails(this, StatusCodes.Status404NotFound, IamError.InvitationNotFound,
                $"Invitation not found: {invitationId}")
            : Ok(InvitationResourceFromEntityAssembler.ToResourceFromEntity(invitation));
    }
}
