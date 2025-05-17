using System.Runtime.Serialization;

namespace Mentorile.Services.PhotoStock.Domain.Exceptions;
public class PhotoDeleteException : Exception
{
    private const string defaulMessage = "Could not be photo deleted.";
    public PhotoDeleteException() : base() { }

    public PhotoDeleteException(string? message) : base(message)
    {
    }

    public PhotoDeleteException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected PhotoDeleteException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}