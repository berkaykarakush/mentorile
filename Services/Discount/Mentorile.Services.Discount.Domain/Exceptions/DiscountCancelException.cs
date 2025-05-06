using System.Runtime.Serialization;

namespace Mentorile.Services.Discount.Domain.Exceptions;
public class DiscountCancelException : Exception
{
    private const string defaultMessage = "Could not be discount canceled.";
    public DiscountCancelException() : base(defaultMessage)
    {
    }

    public DiscountCancelException(string? message) : base(message)
    {
    }

    public DiscountCancelException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected DiscountCancelException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}