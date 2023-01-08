using System.Runtime.Serialization;

namespace Cognito.Events.Shared.Exceptions;

[Serializable]
public class CognitoPostAuthenticationException : Exception
{
    public CognitoPostAuthenticationException()
    {
    }

    public CognitoPostAuthenticationException(Exception ex)
        : this("Unhandled exception", ex)
    {
    }

    public CognitoPostAuthenticationException(string message)
        : base(message)
    {
    }

    public CognitoPostAuthenticationException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    protected CognitoPostAuthenticationException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }
}
