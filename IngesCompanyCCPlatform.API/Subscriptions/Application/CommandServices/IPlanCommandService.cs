using IngesCompanyCCPlatform.API.Shared.Application.Model;
using IngesCompanyCCPlatform.API.Subscriptions.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Subscriptions.Domain.Model.Commands;

namespace IngesCompanyCCPlatform.API.Subscriptions.Application.CommandServices;

/// <summary>
///     Represents the plan command service of the Subscriptions bounded context.
/// </summary>
public interface IPlanCommandService
{
    /// <summary>
    ///     Handles the creation of a plan.
    /// </summary>
    /// <param name="command">The <see cref="CreatePlanCommand" /> with the plan data.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The created plan, or a failure when the plan key is already used.</returns>
    Task<Result<Plan>> Handle(CreatePlanCommand command, CancellationToken cancellationToken);
}
