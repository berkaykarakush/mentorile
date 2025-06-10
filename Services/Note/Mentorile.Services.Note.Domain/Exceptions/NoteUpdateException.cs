using System.Runtime.Serialization;

namespace Mentorile.Services.Note.Domain.Exceptions;

public class NoteUpdateException : Exception
{
    private const string defaultMessage = "Could not be note updated.";
    public NoteUpdateException() : base(defaultMessage) { } 

    public NoteUpdateException(string? message) : base(message) { }

    public NoteUpdateException(string? message, Exception? innerException) : base(message, innerException) { }

    protected NoteUpdateException(SerializationInfo info, StreamingContext context) : base(info, context) { }
}