using Amazon;
using Amazon.SQS;

namespace Cognito.PostConfirmationSignUp.Sqs.Consumer.Accessors;

public static class AwsAccessors
{
    public static readonly AmazonSQSClient AmazonSqs = new(Config.DefaultRegion);
}
