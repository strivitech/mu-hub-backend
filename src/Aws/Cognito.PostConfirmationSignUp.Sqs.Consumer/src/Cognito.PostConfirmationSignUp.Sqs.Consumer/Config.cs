using Amazon;

namespace Cognito.PostConfirmationSignUp.Sqs.Consumer;

public static class Config
{
    public static readonly RegionEndpoint DefaultRegion = RegionEndpoint.USWest1;
    public const string Uri = "https://api.strivitech.me/";
    public const string RelateUserRegistrationFlowUrl = "Auth/RelateUserRegistrationFlow";
    public const string AppLinkingName = "MuHub";
    public const uint PooledConnectionLifetimeMinutes = 1;
    public const string QueueName = "MuHub_Cognito_Post_Confirmation_SignUp";
}
