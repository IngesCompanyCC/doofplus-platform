using IngesCompanyCCPlatform.API.Subscriptions.Domain.Model.ValueObjects;

namespace IngesCompanyCCPlatform.API.Subscriptions.Domain.Model.Entities;

/// <summary>
///     A charge of a subscription, shown as an invoice in the Web App.
/// </summary>
public class Payment
{
    private const string InvoiceNumberNotBlankMessageKey = "payment.error.invoiceNumber.notBlank";
    private const string TransactionIdNotBlankMessageKey = "payment.error.transactionId.notBlank";
    private const string NotPendingMessageKey = "payment.error.notPending";

    /// <summary>
    ///     Initializes a new instance of the <see cref="Payment" /> class. Used by Entity Framework Core.
    /// </summary>
    private Payment()
    {
    }

    /// <summary>
    ///     Initializes a pending payment.
    /// </summary>
    /// <param name="invoiceNumber">The invoice number, for example <c>INV-1-1</c>.</param>
    /// <param name="amount">The amount charged.</param>
    /// <exception cref="ArgumentException">Thrown when the invoice number is blank.</exception>
    public Payment(string invoiceNumber, Money amount)
    {
        if (string.IsNullOrWhiteSpace(invoiceNumber)) throw new ArgumentException(InvoiceNumberNotBlankMessageKey);
        ArgumentNullException.ThrowIfNull(amount);
        InvoiceNumber = invoiceNumber;
        Amount = amount.Amount;
        Currency = amount.Currency;
        Status = EPaymentStatus.Pending;
    }

    /// <summary>Gets the identifier of the payment.</summary>
    public int Id { get; private set; }

    /// <summary>Gets the subscription that was charged.</summary>
    public SubscriptionId SubscriptionId { get; private set; }

    /// <summary>Gets the invoice number.</summary>
    public string InvoiceNumber { get; private set; } = string.Empty;

    /// <summary>Gets the amount charged.</summary>
    public decimal Amount { get; private set; }

    /// <summary>Gets the ISO 4217 currency of the amount.</summary>
    public string Currency { get; private set; } = string.Empty;

    /// <summary>Gets the transaction identifier returned by Niubiz, or <c>null</c> when it was not approved.</summary>
    public string? GatewayTransactionId { get; private set; }

    /// <summary>Gets the status of the payment.</summary>
    public EPaymentStatus Status { get; private set; }

    /// <summary>Gets when the payment gateway processed it, in UTC.</summary>
    public DateTime? ProcessedAt { get; private set; }

    /// <summary>Gets whether the payment was approved.</summary>
    public bool IsApproved => Status == EPaymentStatus.Approved;

    /// <summary>
    ///     Marks the payment as approved by the payment gateway.
    /// </summary>
    /// <param name="transactionId">The transaction identifier returned by Niubiz.</param>
    public void Approve(string transactionId)
    {
        if (string.IsNullOrWhiteSpace(transactionId)) throw new ArgumentException(TransactionIdNotBlankMessageKey);
        EnsurePending();
        GatewayTransactionId = transactionId;
        Status = EPaymentStatus.Approved;
        ProcessedAt = DateTime.UtcNow;
    }

    /// <summary>
    ///     Marks the payment as rejected by the payment gateway.
    /// </summary>
    public void Reject()
    {
        EnsurePending();
        Status = EPaymentStatus.Rejected;
        ProcessedAt = DateTime.UtcNow;
    }

    private void EnsurePending()
    {
        if (Status != EPaymentStatus.Pending) throw new InvalidOperationException(NotPendingMessageKey);
    }
}
