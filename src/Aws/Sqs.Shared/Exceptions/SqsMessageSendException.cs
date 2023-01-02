using System.Runtime.Serialization;

namespace Sqs.Shared.Exceptions;

[Serializable]
public class SqsMessageSendException : Exception
{
    public SqsMessageSendException()
    {
    }

    public SqsMessageSendException(Exception ex)
        : this("Unhandled exception", ex)
    {
    }

    public SqsMessageSendException(string message)
        : base(message)
    {
    }

    public SqsMessageSendException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    protected SqsMessageSendException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }
}
