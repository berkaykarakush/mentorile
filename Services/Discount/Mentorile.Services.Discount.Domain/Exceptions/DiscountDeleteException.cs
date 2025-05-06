using System.Runtime.Serialization;

namespace Mentorile.Services.Discount.Domain.Exceptions;
public class DiscountDeleteException : Exception
{
    private const string defaultMessage = "Could not be discount deleted.";
    public DiscountDeleteException() : base(defaultMessage)
    {
    }

    public DiscountDeleteException(string? message) : base(message)
    {
    }

    public DiscountDeleteException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected DiscountDeleteException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}