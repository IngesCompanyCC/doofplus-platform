namespace IngesCompanyCCPlatform.API.Subscriptions.Domain.Model.ValueObjects;

/// <summary>
///     Status of a payment of a subscription.
/// </summary>
public enum EPaymentStatus
{
    /// <summary>Sent to the payment gateway.</summary>
    Pending,
    /// <summary>Authorized by the payment gateway.</summary>
    Approved,
    /// <summary>Rejected by the payment gateway.</summary>
    Rejected
}
