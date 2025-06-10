using System.Runtime.Serialization;

namespace Mentorile.Services.Note.Domain.Exceptions;

public class NoteDeleteException : Exception
{
    private const string defaultMessaage = "Could not be discount deleted.";
    public NoteDeleteException() : base(defaultMessaage) { }

    public NoteDeleteException(string? message) : base(message) { }

    public NoteDeleteException(string? message, Exception? innerException) : base(message, innerException) { }

    protected NoteDeleteException(SerializationInfo info, StreamingContext context) : base(info, context) { }
}