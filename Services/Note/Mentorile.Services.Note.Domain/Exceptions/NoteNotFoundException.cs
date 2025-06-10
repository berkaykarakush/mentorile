using System.Runtime.Serialization;

namespace Mentorile.Services.Note.Domain.Exceptions;

public class NoteNotFoundException : Exception
{
    private const string defaultMessage = "Note not found.";
    public NoteNotFoundException() : base(defaultMessage){}

    public NoteNotFoundException(string? message) : base(message){}

    public NoteNotFoundException(string? message, Exception? innerException) : base(message, innerException){}

    protected NoteNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context){}
}