using System.Runtime.Serialization;

namespace Mentorile.Services.Discount.Domain.Exceptions;
public class DiscountCreateException : Exception
{
    private const string defaultMessage = "Could not be discount created.";
    public DiscountCreateException() : base(defaultMessage)
    {
    }

    public DiscountCreateException(string? message) : base(message)
    {
    }

    public DiscountCreateException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected DiscountCreateException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}