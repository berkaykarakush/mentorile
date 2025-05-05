using System.Runtime.Serialization;

namespace Mentorile.Services.Course.Domain.Exceptions;
public class CourseNotFoundException : Exception
{
    private const string defaultMessage = "Course not found.";
    public CourseNotFoundException() : base(defaultMessage)
    {
    }

    public CourseNotFoundException(string? message) : base(message)
    {
    }

    public CourseNotFoundException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected CourseNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}