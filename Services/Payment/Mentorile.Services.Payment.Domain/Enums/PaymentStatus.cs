namespace Mentorile.Services.Payment.Domain.Enums;
public enum PaymentStatus
{
    /// <summary>
    /// Payment has been initiated but not yet completed.
    /// </summary>
    Pending,

    /// <summary>
    /// Payment was successfully completed.
    /// </summary>
    Completed,

    /// <summary>
    /// Payment failed due to an error or rejection.
    /// </summary>
    Failed,

    /// <summary>
    /// Payment was cancelled by the user or system.
    /// </summary>
    Cancelled,

    /// <summary>
    /// Payment was refunded to the user.
    /// </summary>
    Refunded,

    /// <summary>
    /// Payment was reversed due to a chargeback claim.
    /// </summary>
    Chargeback,

    /// <summary>
    /// Payment attempt expired (e.g., payment link expired).
    /// </summary>
    Expired
}
