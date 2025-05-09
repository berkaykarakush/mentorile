using System.Runtime.Serialization;

namespace Mentorile.Services.Payment.Domain.Exceptions;
public class PaymentNotFoundException : Exception
{
    private const string defaultMessage = "Payment not found.";
    public PaymentNotFoundException()
    {
    }

    public PaymentNotFoundException(string? message) : base(message)
    {
    }

    public PaymentNotFoundException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected PaymentNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}