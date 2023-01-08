using Amazon;

namespace Cognito.PostConfirmation;

public static class Config
{
    public const string TriggerSourceName = "PostConfirmation_ConfirmSignUp";
    public const int SendSqsMessageDelaySeconds = 0;
}
