using System.Runtime.Serialization;

namespace Mentorile.Services.Email.Domain.Exceptions;

public class UserNotFoundException : Exception
{
    private const string defaultMessage = "User not found.";
    public UserNotFoundException() : base(defaultMessage) { }

    public UserNotFoundException(string? message) : base(message)
    {
    }

    public UserNotFoundException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected UserNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}