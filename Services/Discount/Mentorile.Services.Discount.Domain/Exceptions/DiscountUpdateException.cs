using System.Runtime.Serialization;

namespace Mentorile.Services.Discount.Domain.Exceptions;
public class DiscountUpdateException : Exception
{
    private const string defaultMessage = "Could not be discount updated.";
    public DiscountUpdateException() : base(defaultMessage)
    {
    }

    public DiscountUpdateException(string? message) : base(message)
    {
    }

    public DiscountUpdateException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected DiscountUpdateException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}