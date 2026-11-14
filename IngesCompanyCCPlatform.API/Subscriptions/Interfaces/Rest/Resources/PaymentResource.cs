namespace IngesCompanyCCPlatform.API.Subscriptions.Interfaces.Rest.Resources;

/// <summary>
///     A payment of a subscription, shown as an invoice in the Web App.
/// </summary>
public record PaymentResource(
    int Id,
    string InvoiceNumber,
    decimal Amount,
    string Currency,
    string? GatewayTransactionId,
    string Status,
    DateTime? ProcessedAt);
