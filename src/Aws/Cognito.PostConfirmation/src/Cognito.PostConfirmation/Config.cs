using Amazon;

namespace Cognito.PostConfirmation;

public static class Config
{
    public static readonly RegionEndpoint DefaultRegion = RegionEndpoint.USWest1;
    public const string TriggerSourceName = "PostConfirmation_ConfirmSignUp";
    public const string QueueName = "MuHub_Cognito_Post_Confirmation_SignUp";
    public const int SendSqsMessageDelaySeconds = 0;
}
