using System.Runtime.Serialization;

namespace Mentorile.Services.Discount.Domain.Exceptions;
public class DiscountNotFoundException : Exception
{
    private const string defaultMessage = "Discount not found.";
    public DiscountNotFoundException() : base(defaultMessage)
    {
    }

    public DiscountNotFoundException(string? message) : base(message)
    {
    }

    public DiscountNotFoundException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected DiscountNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}