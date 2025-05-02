using System.Runtime.Serialization;

namespace Mentorile.Services.User.Domain.Exceptions;
public class RoleNotFoundException : Exception
{
    private const string defaultMessage = "Role not found.";
    public RoleNotFoundException() : base(defaultMessage)
    {
    }

    public RoleNotFoundException(string? message) : base(message)
    {
    }

    public RoleNotFoundException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected RoleNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}