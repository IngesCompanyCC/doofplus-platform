using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.Events;
using IngesCompanyCCPlatform.API.Quality.Application.Internal.OutboundServices.Acl;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Quality.Domain.Repositories;
using IngesCompanyCCPlatform.API.Shared.Application.Internal.EventHandlers;

namespace IngesCompanyCCPlatform.API.Quality.Application.Internal.EventHandlers;

/// <summary>
///     Opens a deviation when an incident is registered on a production batch, so nobody has to copy its data.
/// </summary>
/// <remarks>
///     The event is published while Manufacturing saves the incident, so the deviation is only added here and
///     is saved in that same transaction: either both are stored or neither is.
/// </remarks>
public class IncidentRegisteredEventHandler(
    IDeviationRepository deviationRepository,
    ExternalManufacturingService externalManufacturingService,
    ILogger<IncidentRegisteredEventHandler> logger) : IEventHandler<IncidentRegisteredEvent>
{
    /// <summary>
    ///     The name shown as the reporter of the deviations opened from an incident.
    /// </summary>
    public const string Reporter = "Manufacturing incident";

    /// <inheritdoc />
    public async Task Handle(IncidentRegisteredEvent notification, CancellationToken cancellationToken)
    {
        var classification = Enum.TryParse<EClassification>(notification.Severity, true, out var parsed)
            ? parsed
            : EClassification.Minor;
        var orderCode = await externalManufacturingService.FetchProductionOrderCodeByBatchNumber(
            notification.BatchNumber, cancellationToken);

        var deviation = new Deviation(new CreateDeviationCommand(
            Code: await NextCodeAsync(notification.RecordedAt, cancellationToken),
            Title: $"Incident in batch {notification.BatchNumber}",
            BatchCode: notification.BatchNumber,
            OrderCode: orderCode,
            Summary: string.IsNullOrWhiteSpace(notification.Line)
                ? notification.Description
                : $"{notification.Description} (line: {notification.Line})",
            Classification: classification,
            Containment: classification == EClassification.Critical ? "Batch put on hold by the critical incident." : string.Empty,
            Severity: SeverityScore(classification),
            Likelihood: 1,
            Detectability: 1,
            Status: EDeviationStatus.Open,
            RootCause: string.Empty,
            CapaCode: string.Empty,
            ReportedBy: Reporter,
            ReportedAt: notification.RecordedAt,
            Owner: string.Empty,
            DueAt: null));

        await deviationRepository.AddAsync(deviation, cancellationToken);
        logger.LogInformation("Deviation {Code} opened from an incident of batch {BatchNumber}",
            deviation.Code, notification.BatchNumber);
    }

    /// <summary>
    ///     Gives the severity score, from 1 to 5, that the Web App shows for each classification.
    /// </summary>
    private static int SeverityScore(EClassification classification) => classification switch
    {
        EClassification.Critical => 5,
        EClassification.Major => 3,
        _ => 2
    };

    /// <summary>
    ///     Builds a code such as DEV-INC-261127153012 (up to 30 characters), adding a suffix if it is taken.
    /// </summary>
    private async Task<string> NextCodeAsync(DateTime recordedAt, CancellationToken cancellationToken)
    {
        var baseCode = $"DEV-INC-{recordedAt:yyMMddHHmmss}";
        var code = baseCode;
        for (var suffix = 2; await deviationRepository.ExistsByCodeAsync(code, cancellationToken); suffix++)
            code = $"{baseCode}-{suffix}";
        return code;
    }
}
