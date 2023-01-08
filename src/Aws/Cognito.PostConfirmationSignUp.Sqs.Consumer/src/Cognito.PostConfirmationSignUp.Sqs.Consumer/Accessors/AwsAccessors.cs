using Amazon;
using Amazon.SQS;

using Cognito.Common;

namespace Cognito.PostConfirmationSignUp.Sqs.Consumer.Accessors;

public static class AwsAccessors
{
    public static readonly AmazonSQSClient AmazonSqs = new(Configuration.DefaultRegion);
}
