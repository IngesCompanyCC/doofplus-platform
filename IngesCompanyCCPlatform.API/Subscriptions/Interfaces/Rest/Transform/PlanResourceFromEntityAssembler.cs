using IngesCompanyCCPlatform.API.Subscriptions.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Subscriptions.Interfaces.Rest.Resources;

namespace IngesCompanyCCPlatform.API.Subscriptions.Interfaces.Rest.Transform;

/// <summary>
///     Converts a <see cref="Plan" /> aggregate into a <see cref="PlanResource" />.
/// </summary>
public static class PlanResourceFromEntityAssembler
{
    public static PlanResource ToResourceFromEntity(Plan entity)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity),
                "Plan entity cannot be null when converting to resource.");
        return new PlanResource(
            entity.Id,
            entity.PlanKey.Value,
            entity.Name,
            entity.MonthlyPrice.Amount,
            entity.AnnualPrice.Amount,
            entity.MonthlyPrice.Currency,
            entity.UserLimit,
            entity.IotDeviceLimit);
    }
}
