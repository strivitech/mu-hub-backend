using System.Text.Json;

using Amazon.CognitoIdentityProvider;
using Amazon.Lambda.Core;
using Amazon.Lambda.SQSEvents;
using Amazon.SQS.Model;

using Cognito.Events.Shared.Exceptions;
using Cognito.PostConfirmationSignUp.Sqs.Consumer.Accessors;
using Cognito.PostConfirmationSignUp.Sqs.Consumer.Handlers;
using Cognito.PostConfirmationSignUp.Sqs.Consumer.Messages;
using Cognito.PostConfirmationSignUp.Sqs.Consumer.Services;


// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace Cognito.PostConfirmationSignUp.Sqs.Consumer;

public class Function
{
    /// <summary>
    /// Default constructor. This constructor is used by Lambda to construct the instance. When invoked in a Lambda environment
    /// the AWS credentials will come from the IAM role associated with the function and the AWS region will be set to the
    /// region the Lambda function is executed in.
    /// </summary>
    public Function()
    {
    }


    /// <summary>
    /// This method is called for every Lambda invocation. This method takes in an SQS event object and can be used 
    /// to respond to SQS messages.
    /// </summary>
    /// <param name="sqsEvent"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public async Task FunctionHandler(SQSEvent sqsEvent, ILambdaContext context)
    {
        ArgumentNullException.ThrowIfNull(sqsEvent);
        ArgumentNullException.ThrowIfNull(context);
        
        foreach (var message in sqsEvent.Records)
        {
            await ProcessMessageAsync(message, context);
        }
    }

    private static async Task ProcessMessageAsync(SQSEvent.SQSMessage message, ILambdaContext context)
    {
        ArgumentNullException.ThrowIfNull(message);
        
        context.Logger.LogDebug($"Started processing message with id {message.MessageId}");
        var queueUrl = await AwsAccessors.AmazonSqs.GetQueueUrlAsync(Config.QueueName);
        
        using var client = new AmazonCognitoIdentityProviderClient();
        var messageHandler =
            new UserRegistrationMessageHandler(new LinkUserRegistrationService(client, context.Logger), context.Logger);

        var deserializedMessage = JsonSerializer.Deserialize<UserRegistrationMessage>(message.Body);

        if (deserializedMessage is null)
        {
            throw new CognitoPostConfirmationSignUpException("Unable to deserialize a message");
        }
 
        context.Logger.LogDebug("Message was deserialized");
        
        await messageHandler.HandleAsync(deserializedMessage);
        
        context.Logger.LogDebug("Message was handled");
        await AwsAccessors.AmazonSqs.DeleteMessageAsync(
            new DeleteMessageRequest { QueueUrl = queueUrl.QueueUrl, ReceiptHandle = message.ReceiptHandle });
        
        context.Logger.LogDebug("Message was deleted from the queue");
        context.Logger.LogInformation($"Message with id {message.MessageId} has been successfully processed");
        await Task.CompletedTask;
    }
}
