using System.Net.Mime;
using IngesCompanyCCPlatform.API.Shared.Interfaces.Rest.ProblemDetails;
using IngesCompanyCCPlatform.API.Subscriptions.Application.CommandServices;
using IngesCompanyCCPlatform.API.Subscriptions.Application.QueryServices;
using IngesCompanyCCPlatform.API.Subscriptions.Domain.Model;
using IngesCompanyCCPlatform.API.Subscriptions.Domain.Model.Errors;
using IngesCompanyCCPlatform.API.Subscriptions.Domain.Model.Queries;
using IngesCompanyCCPlatform.API.Subscriptions.Interfaces.Rest.Resources;
using IngesCompanyCCPlatform.API.Subscriptions.Interfaces.Rest.Transform;
using IngesCompanyCCPlatform.API.Subscriptions.Resources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace IngesCompanyCCPlatform.API.Subscriptions.Interfaces.Rest;

/// <summary>
///     REST controller of the subscription plans offered by DoofPlus.
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Plans")]
public class PlansController(
    IPlanCommandService planCommandService,
    IPlanQueryService planQueryService,
    IStringLocalizer<SubscriptionsMessages> messages,
    ProblemDetailsFactory problemDetailsFactory)
    : ControllerBase
{
    [HttpPost]
    [EndpointSummary("Create a plan")]
    [EndpointDescription("Creates a subscription plan with its prices and limits.")]
    [ProducesResponseType(typeof(PlanResource), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> CreatePlan([FromBody] CreatePlanResource resource,
        CancellationToken cancellationToken)
    {
        try
        {
            var createPlanCommand = CreatePlanCommandFromResourceAssembler.ToCommandFromResource(resource);
            var result = await planCommandService.Handle(createPlanCommand, cancellationToken);

            return SubscriptionsActionResultAssembler.ToActionResultFromCommandResult(
                this,
                result,
                messages,
                problemDetailsFactory,
                createdPlan => CreatedAtAction(nameof(GetPlanById), new { planId = createdPlan.Id.Value },
                    PlanResourceFromEntityAssembler.ToResourceFromEntity(createdPlan)));
        }
        catch (ArgumentException exception)
        {
            return SubscriptionsActionResultAssembler.ToActionResultFromInvalidArgument(this, exception, messages,
                problemDetailsFactory);
        }
    }

    [HttpGet]
    [EndpointSummary("Get all plans")]
    [EndpointDescription("Gets the plans and prices shown to the laboratory administrator.")]
    [ProducesResponseType(typeof(IEnumerable<PlanResource>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllPlans(CancellationToken cancellationToken)
    {
        var plans = await planQueryService.Handle(new GetAllPlansQuery(), cancellationToken);
        var planResources = plans.Select(PlanResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(planResources);
    }

    [HttpGet("{planId:int}")]
    [EndpointSummary("Get a plan by its id")]
    [EndpointDescription("Gets a plan by the provided id.")]
    [ProducesResponseType(typeof(PlanResource), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetPlanById([FromRoute] int planId, CancellationToken cancellationToken)
    {
        var plan = await planQueryService.Handle(new GetPlanByIdQuery(planId), cancellationToken);

        return SubscriptionsActionResultAssembler.ToActionResultFromQueryResult(
            this,
            plan,
            PlanErrors.NotFoundById(planId),
            SubscriptionsError.PlanNotFound,
            messages,
            problemDetailsFactory,
            foundPlan => Ok(PlanResourceFromEntityAssembler.ToResourceFromEntity(foundPlan)),
            planId);
    }
}
