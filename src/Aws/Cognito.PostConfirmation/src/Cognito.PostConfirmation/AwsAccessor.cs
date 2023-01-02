using Amazon.SQS;

namespace Cognito.PostConfirmation;

public static class AwsAccessor
{
    public static readonly AmazonSQSClient AmazonSqs = new(Config.DefaultRegion);
}

