using IngesCompanyCCPlatform.API.Shared.Application.Model;
using IngesCompanyCCPlatform.API.Shared.Domain.Repositories;
using IngesCompanyCCPlatform.API.Subscriptions.Application.CommandServices;
using IngesCompanyCCPlatform.API.Subscriptions.Domain.Model;
using IngesCompanyCCPlatform.API.Subscriptions.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Subscriptions.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Subscriptions.Domain.Model.Errors;
using IngesCompanyCCPlatform.API.Subscriptions.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Subscriptions.Domain.Repositories;

namespace IngesCompanyCCPlatform.API.Subscriptions.Application.Internal.CommandServices;

/// <summary>
///     Implements the subscription command service of the Subscriptions bounded context.
/// </summary>
public class SubscriptionCommandService(
    ISubscriptionRepository subscriptionRepository,
    IPlanRepository planRepository,
    IUnitOfWork unitOfWork)
    : ISubscriptionCommandService
{
    private static readonly ESubscriptionStatus[] CurrentStatuses =
        [ESubscriptionStatus.Pending, ESubscriptionStatus.Active];

    /// <inheritdoc />
    public async Task<Result<Subscription>> Handle(CreateSubscriptionCommand command,
        CancellationToken cancellationToken)
    {
        if (await planRepository.FindByIdAsNoTrackingAsync(command.PlanId, cancellationToken) is null)
            return Result<Subscription>.Failure(SubscriptionErrors.PlanNotFound, SubscriptionsError.PlanNotFound);

        if (await subscriptionRepository.ExistsByOrganizationIdAndStatusInAsync(command.OrganizationId,
                CurrentStatuses, cancellationToken))
            return Result<Subscription>.Failure(SubscriptionErrors.AlreadySubscribed,
                SubscriptionsError.AlreadySubscribed);

        var subscription = new Subscription(command);
        await subscriptionRepository.AddAsync(subscription, cancellationToken);
        await unitOfWork.CompleteAsync(cancellationToken);
        return Result<Subscription>.Success(subscription);
    }

    /// <inheritdoc />
    public Task<Result<Subscription>> Handle(ActivateSubscriptionCommand command,
        CancellationToken cancellationToken)
    {
        return Transition(command.SubscriptionId, subscription => subscription.Activate(), cancellationToken);
    }

    /// <inheritdoc />
    public Task<Result<Subscription>> Handle(CancelSubscriptionCommand command, CancellationToken cancellationToken)
    {
        return Transition(command.SubscriptionId, subscription => subscription.Cancel(), cancellationToken);
    }

    /// <summary>
    ///     Applies a change of state to a subscription and saves it.
    /// </summary>
    private async Task<Result<Subscription>> Transition(SubscriptionId subscriptionId,
        Action<Subscription> transition, CancellationToken cancellationToken)
    {
        var subscription = await subscriptionRepository.FindByIdAsync(subscriptionId, cancellationToken);
        if (subscription is null)
            return Result<Subscription>.Failure(SubscriptionErrors.NotFound, SubscriptionsError.SubscriptionNotFound);

        try
        {
            transition(subscription);
        }
        catch (InvalidOperationException exception)
        {
            return Result<Subscription>.Failure(SubscriptionErrors.InvalidState(exception.Message),
                SubscriptionsError.InvalidSubscriptionState);
        }

        await unitOfWork.CompleteAsync(cancellationToken);
        return Result<Subscription>.Success(subscription);
    }
}
