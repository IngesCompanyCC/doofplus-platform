using IngesCompanyCCPlatform.API.Shared.Application.Model;
using IngesCompanyCCPlatform.API.Subscriptions.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Subscriptions.Domain.Model.Commands;

namespace IngesCompanyCCPlatform.API.Subscriptions.Application.CommandServices;

/// <summary>
///     Represents the subscription command service of the Subscriptions bounded context.
/// </summary>
public interface ISubscriptionCommandService
{
    /// <summary>
    ///     Handles the selection of a plan by an organization (US51).
    /// </summary>
    Task<Result<Subscription>> Handle(CreateSubscriptionCommand command, CancellationToken cancellationToken);

    /// <summary>
    ///     Handles the activation of a pending subscription (US52).
    /// </summary>
    Task<Result<Subscription>> Handle(ActivateSubscriptionCommand command, CancellationToken cancellationToken);

    /// <summary>
    ///     Handles the cancellation of an active subscription (US53).
    /// </summary>
    Task<Result<Subscription>> Handle(CancelSubscriptionCommand command, CancellationToken cancellationToken);

    Task<Result<Subscription>> Handle(RenewSubscriptionCommand command, CancellationToken cancellationToken);

    Task<Result<Subscription>> Handle(PaySubscriptionCommand command, CancellationToken cancellationToken);
}
