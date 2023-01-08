using Amazon;

using Cognito.Common;

namespace Cognito.PostConfirmationSignUp.Sqs.Consumer;

public static class Config
{
    public const string RelateUserRegistrationFlowUri = Configuration.Host + "api/v1/Auth/RelateUserRegistrationFlow";
    public const uint PooledConnectionLifetimeMinutes = 1;
}
