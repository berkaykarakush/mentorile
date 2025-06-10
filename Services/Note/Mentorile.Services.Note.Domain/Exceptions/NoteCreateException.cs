using System.Runtime.Serialization;

namespace Mentorile.Services.Note.Domain.Exceptions;

public class NoteCreateException : Exception
{
    private const string defaultMessaage = "Could not be note created.";
    public NoteCreateException() : base(defaultMessaage) { }
    public NoteCreateException(string? message) : base(message) { }
    public NoteCreateException(string? message, Exception? innerException) : base(message, innerException) { }
    protected NoteCreateException(SerializationInfo info, StreamingContext context) : base(info, context) { }   
}