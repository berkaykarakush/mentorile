using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Mentorile.Client.Web.Exceptions;

public class EmailNotConfirmedException : Exception
{
    public EmailNotConfirmedException()
    {
    }

    public EmailNotConfirmedException(string? message) : base(message)
    {
    }

    public EmailNotConfirmedException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected EmailNotConfirmedException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}