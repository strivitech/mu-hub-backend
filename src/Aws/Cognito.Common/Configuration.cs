using Amazon;

namespace Cognito.Common;

public static class Configuration
{
    public const string UserPoolId = "us-west-1_GxzjpST6e";
    public static readonly RegionEndpoint DefaultRegion = RegionEndpoint.USWest1;
    public const string Host = "http://api.strivitech.me/";
    public const string AppName = "MuHub";
    public const string QueueName = "MuHub_Cognito_Post_Confirmation_SignUp";
}
