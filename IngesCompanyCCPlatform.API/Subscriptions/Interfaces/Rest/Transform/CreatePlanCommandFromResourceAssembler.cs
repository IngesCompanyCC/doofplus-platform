using IngesCompanyCCPlatform.API.Subscriptions.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Subscriptions.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Subscriptions.Interfaces.Rest.Resources;

namespace IngesCompanyCCPlatform.API.Subscriptions.Interfaces.Rest.Transform;

/// <summary>
///     Converts a <see cref="CreatePlanResource" /> into a <see cref="CreatePlanCommand" />.
/// </summary>
public static class CreatePlanCommandFromResourceAssembler
{
    public static CreatePlanCommand ToCommandFromResource(CreatePlanResource resource)
    {
        if (resource == null)
            throw new ArgumentNullException(nameof(resource),
                "CreatePlanResource cannot be null when converting to command.");
        return new CreatePlanCommand(
            new PlanKey(resource.PlanKey),
            resource.Name,
            new Money(resource.MonthlyPrice, resource.Currency),
            new Money(resource.AnnualPrice, resource.Currency),
            resource.UserLimit,
            resource.IotDeviceLimit);
    }
}
