namespace IngesCompanyCCPlatform.API.Subscriptions.Application.OutboundServices.PaymentGateway;

/// <summary>
///     Outcome of a charge sent to the payment gateway.
/// </summary>
public enum EPaymentOutcome
{
    Approved,
    Rejected,
    Unavailable
}

/// <summary>
///     Answer of the payment gateway to a charge.
/// </summary>
/// <param name="Outcome">Approved, rejected or unavailable.</param>
/// <param name="TransactionId">The transaction identifier when it was approved.</param>
/// <param name="Message">The description returned by the gateway.</param>
public record PaymentResult(EPaymentOutcome Outcome, string? TransactionId, string Message)
{
    public static PaymentResult ApprovedWith(string transactionId, string message) => new(EPaymentOutcome.Approved, transactionId, message);
    public static PaymentResult RejectedWith(string message) => new(EPaymentOutcome.Rejected, null, message);
    public static PaymentResult UnavailableWith(string message) => new(EPaymentOutcome.Unavailable, null, message);
    public bool IsApproved => Outcome == EPaymentOutcome.Approved;
    public bool IsUnavailable => Outcome == EPaymentOutcome.Unavailable;
}
