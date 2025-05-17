using System.Runtime.Serialization;

namespace Mentorile.Services.PhotoStock.Domain.Exceptions;
public class PhotoNotFoundException : Exception
{
    private const string defaultMessage = "Photo not found.";
    public PhotoNotFoundException() : base(defaultMessage)
    {
    }

    public PhotoNotFoundException(string? message) : base(message)
    {
    }

    public PhotoNotFoundException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected PhotoNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}