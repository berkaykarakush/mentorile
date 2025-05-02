using System.Runtime.Serialization;

namespace Mentorile.Services.User.Domain.Exceptions;
public class AuthenticationFailedException : Exception
{
    private const string defaultMessage = "Email/Phone number or password is incorrect.";
    public AuthenticationFailedException() : base(defaultMessage)
    {
    }

    public AuthenticationFailedException(string? message) : base(message)
    {
    }

    public AuthenticationFailedException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected AuthenticationFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}