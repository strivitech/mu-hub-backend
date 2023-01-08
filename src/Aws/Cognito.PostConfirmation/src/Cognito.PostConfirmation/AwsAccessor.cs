using Amazon.SQS;

using Cognito.Common;

namespace Cognito.PostConfirmation;

public static class AwsAccessor
{
    public static readonly AmazonSQSClient AmazonSqs = new(Configuration.DefaultRegion);
}

