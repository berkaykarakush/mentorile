using System.Runtime.Serialization;

namespace Mentorile.Services.Payment.Domain.Exceptions;
public class PaymentCreateException : Exception
{
    private const string defaultMessage = "Could not be payment created.";
    public PaymentCreateException() : base(defaultMessage)
    {
    }

    public PaymentCreateException(string? message) : base(message)
    {
    }

    public PaymentCreateException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected PaymentCreateException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}