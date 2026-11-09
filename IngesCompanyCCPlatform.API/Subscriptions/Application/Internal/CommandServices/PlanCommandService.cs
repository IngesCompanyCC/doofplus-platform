using IngesCompanyCCPlatform.API.Shared.Application.Model;
using IngesCompanyCCPlatform.API.Shared.Domain.Repositories;
using IngesCompanyCCPlatform.API.Subscriptions.Application.CommandServices;
using IngesCompanyCCPlatform.API.Subscriptions.Domain.Model;
using IngesCompanyCCPlatform.API.Subscriptions.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Subscriptions.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Subscriptions.Domain.Model.Errors;
using IngesCompanyCCPlatform.API.Subscriptions.Domain.Repositories;

namespace IngesCompanyCCPlatform.API.Subscriptions.Application.Internal.CommandServices;

/// <summary>
///     Implements the plan command service of the Subscriptions bounded context.
/// </summary>
public class PlanCommandService(IPlanRepository planRepository, IUnitOfWork unitOfWork) : IPlanCommandService
{
    /// <inheritdoc />
    public async Task<Result<Plan>> Handle(CreatePlanCommand command, CancellationToken cancellationToken)
    {
        if (await planRepository.ExistsByPlanKeyAsync(command.PlanKey, cancellationToken))
            return Result<Plan>.Failure(PlanErrors.DuplicatePlanKey, SubscriptionsError.DuplicatePlanKey);

        var plan = new Plan(command);
        await planRepository.AddAsync(plan, cancellationToken);
        await unitOfWork.CompleteAsync(cancellationToken);
        return Result<Plan>.Success(plan);
    }
}
