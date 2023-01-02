using Amazon.Lambda.Core;
using Amazon.SQS;
using Amazon.SQS.Model;

using Cognito.PostConfirmationSignUp.Sqs.Consumer.Messages;
using Cognito.PostConfirmationSignUp.Sqs.Consumer.Services;

using Sqs.Shared.Handlers;

namespace Cognito.PostConfirmationSignUp.Sqs.Consumer.Handlers;

internal class UserRegistrationMessageHandler : IMessageHandler<UserRegistrationMessage>
{
    private readonly ILinkUserRegistrationService _linkService;
    private readonly ILambdaLogger _logger;

    public UserRegistrationMessageHandler(ILinkUserRegistrationService linkService, ILambdaLogger logger)
    {
        _linkService = linkService ?? throw new ArgumentNullException(nameof(linkService));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<bool> HandleAsync(UserRegistrationMessage message)
    {
        _logger.LogTrace($"Handling message: {message}");
        if (!IsMessageValid(message))
        {
            _logger.LogError($"Invalid message: {message}");
            return false;
        }

        var appUserId = await _linkService.LinkAsync(message.CognitoUserName, message.CognitoUserPoolId);
        return !string.IsNullOrEmpty(appUserId);
    }

    private static bool IsMessageValid(UserRegistrationMessage message) 
        => !string.IsNullOrEmpty(message.CognitoUserName) && !string.IsNullOrEmpty(message.CognitoUserPoolId);
}
