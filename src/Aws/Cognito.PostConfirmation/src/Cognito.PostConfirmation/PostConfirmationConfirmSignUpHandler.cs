using System.Net;
using System.Text;
using System.Text.Json;

using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using Amazon.Lambda.Core;
using Amazon.SQS;
using Amazon.SQS.Model;

using Cognito.Common;
using Cognito.Events.Shared.Core;
using Cognito.Events.Shared.Events;
using Cognito.Events.Shared.Exceptions;

using Sqs.Shared.Data;
using Sqs.Shared.Exceptions;
using Sqs.Shared.Services;

namespace Cognito.PostConfirmation;

internal class PostConfirmationConfirmSignUpHandler : CognitoTriggerHandler<PostConfirmationEvent>
{
    private readonly ISqsSenderService<UserRegistrationMessage> _userRegistrationSqsSenderService;
    public override string TriggerSource => Config.TriggerSourceName;

    public PostConfirmationConfirmSignUpHandler(
        JsonElement cognitoEvent,
        ILambdaLogger logger, ISqsSenderService<UserRegistrationMessage> userRegistrationSqsSenderService)
        : base(cognitoEvent, logger)
    {
        _userRegistrationSqsSenderService = userRegistrationSqsSenderService ??
                                      throw new ArgumentNullException(nameof(userRegistrationSqsSenderService));
    }

    public override async Task<JsonElement> HandleTriggerEventAsync()
    {
        Logger.LogDebug("Started handling PostConfirmationConfirmSignUp event");
        
        var message = new UserRegistrationMessage
        {
            CognitoUserName = TriggerEvent.UserName, CreatedAt = DateTimeOffset.Now
        };

        var response = await _userRegistrationSqsSenderService.SendMessageAsync(message);
        if (response.HttpStatusCode != HttpStatusCode.OK)
        {
            Logger.LogError($"Failed to send message to SQS queue {Configuration.QueueName} with message {message}");
            throw new SqsMessageSendException("Failed to send message to SQS queue");
        }

        Logger.LogInformation("PostConfirmationConfirmSignUp message was successfully sent for further processing");
        return await base.HandleTriggerEventAsync();
    }
}
