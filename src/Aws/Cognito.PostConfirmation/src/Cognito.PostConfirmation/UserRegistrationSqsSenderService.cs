using System.Text.Json;

using Amazon.Lambda.Core;
using Amazon.Runtime.Internal.Util;
using Amazon.SQS;
using Amazon.SQS.Model;

using Sqs.Shared.Data;
using Sqs.Shared.Services;

namespace Cognito.PostConfirmation;

public class UserRegistrationSqsSenderService : ISqsSenderService<UserRegistrationMessage>
{
    private readonly IAmazonSQS _sqsClient;
    private readonly ILambdaLogger _logger;

    public UserRegistrationSqsSenderService(IAmazonSQS sqsClient, ILambdaLogger lambdaLogger)
    {
        _sqsClient = sqsClient ?? throw new ArgumentNullException(nameof(sqsClient));
        _logger = lambdaLogger ?? throw new ArgumentNullException(nameof(lambdaLogger));
    }

    public async Task<SendMessageResponse> SendMessageAsync(UserRegistrationMessage message)
    {
        ArgumentNullException.ThrowIfNull(message);
        
        _logger.LogDebug("Start sending message to SQS queue");
        var queueUrl = await _sqsClient.GetQueueUrlAsync(Config.QueueName);
        var messageBody = JsonSerializer.Serialize(message);

        var sendRequest = new SendMessageRequest
        {
            QueueUrl = queueUrl.QueueUrl,
            DelaySeconds = Config.SendSqsMessageDelaySeconds,
            MessageAttributes = new Dictionary<string, MessageAttributeValue>
            {
                [nameof(IMessage.MessageTypeName)] = new()
                {
                    DataType = "String", StringValue = nameof(UserRegistrationMessage)
                }
            },
            MessageBody = messageBody
        };
        
        var sendResponse = await _sqsClient.SendMessageAsync(sendRequest);
        _logger.LogInformation($"Message sent to the SQS queue by url: {queueUrl.QueueUrl}");
        return sendResponse;
    }
}
