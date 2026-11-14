using System.Net.Mime;
using IngesCompanyCCPlatform.API.Shared.Interfaces.Rest.ProblemDetails;
using IngesCompanyCCPlatform.API.Subscriptions.Application.CommandServices;
using IngesCompanyCCPlatform.API.Subscriptions.Application.QueryServices;
using IngesCompanyCCPlatform.API.Subscriptions.Domain.Model;
using IngesCompanyCCPlatform.API.Subscriptions.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Subscriptions.Domain.Model.Errors;
using IngesCompanyCCPlatform.API.Subscriptions.Domain.Model.Queries;
using IngesCompanyCCPlatform.API.Subscriptions.Interfaces.Rest.Resources;
using IngesCompanyCCPlatform.API.Subscriptions.Interfaces.Rest.Transform;
using IngesCompanyCCPlatform.API.Subscriptions.Resources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace IngesCompanyCCPlatform.API.Subscriptions.Interfaces.Rest;

/// <summary>
///     REST controller of the subscriptions of the organizations to the DoofPlus plans.
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Subscriptions")]
public class SubscriptionsController(
    ISubscriptionCommandService subscriptionCommandService,
    ISubscriptionQueryService subscriptionQueryService,
    IStringLocalizer<SubscriptionsMessages> messages,
    ProblemDetailsFactory problemDetailsFactory)
    : ControllerBase
{
    [HttpPost]
    [EndpointSummary("Create a subscription")]
    [EndpointDescription("Selects a plan for an organization (US51); the subscription starts as Pending.")]
    [ProducesResponseType(typeof(SubscriptionResource), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> CreateSubscription([FromBody] CreateSubscriptionResource resource,
        CancellationToken cancellationToken)
    {
        try
        {
            var createSubscriptionCommand =
                CreateSubscriptionCommandFromResourceAssembler.ToCommandFromResource(resource);
            var result = await subscriptionCommandService.Handle(createSubscriptionCommand, cancellationToken);

            return SubscriptionsActionResultAssembler.ToActionResultFromCommandResult(
                this,
                result,
                messages,
                problemDetailsFactory,
                createdSubscription => CreatedAtAction(nameof(GetSubscriptionById),
                    new { subscriptionId = createdSubscription.Id.Value },
                    SubscriptionResourceFromEntityAssembler.ToResourceFromEntity(createdSubscription)));
        }
        catch (ArgumentException exception)
        {
            return SubscriptionsActionResultAssembler.ToActionResultFromInvalidArgument(this, exception, messages,
                problemDetailsFactory);
        }
    }

    [HttpPost("{subscriptionId:int}/activation")]
    [EndpointSummary("Activate a subscription")]
    [EndpointDescription("Activates a Pending subscription once the payment is authorized (US52).")]
    [ProducesResponseType(typeof(SubscriptionResource), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> ActivateSubscription([FromRoute] int subscriptionId,
        CancellationToken cancellationToken)
    {
        var result = await subscriptionCommandService.Handle(new ActivateSubscriptionCommand(subscriptionId),
            cancellationToken);

        return SubscriptionsActionResultAssembler.ToActionResultFromCommandResult(
            this,
            result,
            messages,
            problemDetailsFactory,
            activatedSubscription =>
                Ok(SubscriptionResourceFromEntityAssembler.ToResourceFromEntity(activatedSubscription)));
    }

    [HttpPost("{subscriptionId:int}/payments")]
    [EndpointSummary("Pay a subscription")]
    [EndpointDescription("Charges the next billing period with Niubiz: activates a Pending subscription or renews an Active one. Answers 422 when Niubiz rejects the card and 503 when it is not available.")]
    [ProducesResponseType(typeof(SubscriptionResource), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
    public async Task<IActionResult> PaySubscription([FromRoute] int subscriptionId,
        [FromBody] PaySubscriptionResource resource,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(resource.CardToken)) throw new ArgumentException("payment.error.cardToken.notBlank");
        var result = await subscriptionCommandService.Handle(new PaySubscriptionCommand(new Domain.Model.ValueObjects.SubscriptionId(subscriptionId), resource.CardToken), cancellationToken);

        return SubscriptionsActionResultAssembler.ToActionResultFromCommandResult(
            this,
            result,
            messages,
            problemDetailsFactory,
            paidSubscription =>
                Ok(SubscriptionResourceFromEntityAssembler.ToResourceFromEntity(paidSubscription)));
    }

    [HttpPost("{subscriptionId:int}/renewal")]
    [EndpointSummary("Renew a subscription")]
    [EndpointDescription("Extends an Active subscription one more billing period (US53).")]
    [ProducesResponseType(typeof(SubscriptionResource), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> RenewSubscription([FromRoute] int subscriptionId,
        CancellationToken cancellationToken)
    {
        var result = await subscriptionCommandService.Handle(new RenewSubscriptionCommand(new Domain.Model.ValueObjects.SubscriptionId(subscriptionId)), cancellationToken);

        return SubscriptionsActionResultAssembler.ToActionResultFromCommandResult(
            this,
            result,
            messages,
            problemDetailsFactory,
            renewedSubscription =>
                Ok(SubscriptionResourceFromEntityAssembler.ToResourceFromEntity(renewedSubscription)));
    }

    [HttpPost("{subscriptionId:int}/cancellation")]
    [EndpointSummary("Cancel a subscription")]
    [EndpointDescription("Cancels an Active subscription; the access stays until the end of the paid period (US53).")]
    [ProducesResponseType(typeof(SubscriptionResource), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> CancelSubscription([FromRoute] int subscriptionId,
        CancellationToken cancellationToken)
    {
        var result = await subscriptionCommandService.Handle(new CancelSubscriptionCommand(subscriptionId),
            cancellationToken);

        return SubscriptionsActionResultAssembler.ToActionResultFromCommandResult(
            this,
            result,
            messages,
            problemDetailsFactory,
            cancelledSubscription =>
                Ok(SubscriptionResourceFromEntityAssembler.ToResourceFromEntity(cancelledSubscription)));
    }

    [HttpGet("{subscriptionId:int}")]
    [EndpointSummary("Get a subscription by its id")]
    [EndpointDescription("Gets a subscription by the provided id.")]
    [ProducesResponseType(typeof(SubscriptionResource), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetSubscriptionById([FromRoute] int subscriptionId,
        CancellationToken cancellationToken)
    {
        var subscription = await subscriptionQueryService.Handle(new GetSubscriptionByIdQuery(subscriptionId),
            cancellationToken);

        return SubscriptionsActionResultAssembler.ToActionResultFromQueryResult(
            this,
            subscription,
            SubscriptionErrors.NotFoundById(subscriptionId),
            SubscriptionsError.SubscriptionNotFound,
            messages,
            problemDetailsFactory,
            foundSubscription => Ok(SubscriptionResourceFromEntityAssembler.ToResourceFromEntity(foundSubscription)),
            subscriptionId);
    }

    [HttpGet]
    [EndpointSummary("Get the subscriptions of an organization")]
    [EndpointDescription("Gets the subscriptions of the provided organization.")]
    [ProducesResponseType(typeof(IEnumerable<SubscriptionResource>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetSubscriptionsByOrganizationId([FromQuery] int organizationId,
        CancellationToken cancellationToken)
    {
        try
        {
            var query = new GetAllSubscriptionsByOrganizationIdQuery(organizationId);
            var subscriptions = await subscriptionQueryService.Handle(query, cancellationToken);
            return Ok(subscriptions.Select(SubscriptionResourceFromEntityAssembler.ToResourceFromEntity));
        }
        catch (ArgumentException exception)
        {
            return SubscriptionsActionResultAssembler.ToActionResultFromInvalidArgument(this, exception, messages,
                problemDetailsFactory);
        }
    }
}
