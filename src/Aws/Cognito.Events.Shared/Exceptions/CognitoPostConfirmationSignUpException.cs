using System.Runtime.Serialization;

namespace Cognito.Events.Shared.Exceptions;

[Serializable]
public class CognitoPostConfirmationSignUpException : Exception
{
    public CognitoPostConfirmationSignUpException()
    {
    }

    public CognitoPostConfirmationSignUpException(Exception ex)
        : this("Unhandled exception", ex)
    {
    }

    public CognitoPostConfirmationSignUpException(string message)
        : base(message)
    {
    }

    public CognitoPostConfirmationSignUpException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    protected CognitoPostConfirmationSignUpException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }
}
