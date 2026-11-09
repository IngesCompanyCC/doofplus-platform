using IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Resources;

namespace IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Transform;

/// <summary>
///     Converts a <see cref="Deviation" /> aggregate into a <see cref="DeviationResource" />.
/// </summary>
public static class DeviationResourceFromEntityAssembler
{
    public static DeviationResource ToResourceFromEntity(Deviation entity)
    {
        var today = DateOnly.FromDateTime(DateTime.UtcNow);
        return new DeviationResource(
            entity.Id,
            entity.Code,
            entity.ProductionBatchId,
            entity.Severity.ToString(),
            entity.Status.ToString(),
            entity.Description,
            entity.RootCause?.Method,
            entity.RootCause?.Description,
            entity.DetectedAt,
            entity.ClosedAt,
            entity.Actions.OrderBy(action => action.Id).Select(action => new CapaActionResource(
                action.Id, action.Type.ToString(), action.Description, action.ResponsibleId, action.DueDate,
                action.Progress, action.Status.ToString(), action.IsOverdue(today))));
    }
}

/// <summary>
///     Converts the request resources of the deviations into commands.
/// </summary>
public static class DeviationCommandFromResourceAssembler
{
    public static RegisterDeviationCommand ToCommandFromResource(RegisterDeviationResource resource)
    {
        return new RegisterDeviationCommand(resource.Code, new ProductionBatchId(resource.ProductionBatchId),
            QualityEnumParser.Parse<EDeviationSeverity>(resource.Severity, "severity"), resource.Description);
    }

    public static ClassifyDeviationCommand ToCommandFromResource(int deviationId, ClassifyDeviationResource resource)
    {
        return new ClassifyDeviationCommand(deviationId,
            QualityEnumParser.Parse<EDeviationSeverity>(resource.Severity, "severity"));
    }

    public static RegisterRootCauseCommand ToCommandFromResource(int deviationId, RegisterRootCauseResource resource)
    {
        return new RegisterRootCauseCommand(deviationId, new RootCause(resource.Method, resource.Description));
    }

    public static AddCapaActionCommand ToCommandFromResource(int deviationId, AddCapaActionResource resource)
    {
        return new AddCapaActionCommand(deviationId, QualityEnumParser.Parse<ECapaType>(resource.Type, "CAPA type"),
            resource.Description, new UserId(resource.ResponsibleId), resource.DueDate);
    }

    public static UpdateCapaActionProgressCommand ToCommandFromResource(int deviationId, int capaActionId,
        UpdateCapaActionProgressResource resource)
    {
        return new UpdateCapaActionProgressCommand(deviationId, capaActionId, resource.Progress);
    }
}
