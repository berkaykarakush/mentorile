using System.Runtime.Serialization;

namespace Mentorile.Services.Payment.Domain.Exceptions;
public class PaymentUpdateException : Exception
{
    private const string defaultMessage = "Could not be payment updated.";

    public PaymentUpdateException() : base(defaultMessage)
    { 
    }

    public PaymentUpdateException(string? message) : base(message)
    {
    }

    public PaymentUpdateException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected PaymentUpdateException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}