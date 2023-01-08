using Amazon.Lambda.Core;
using Amazon.SQS;
using Amazon.SQS.Model;

using Cognito.Common;
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
        ValidateIdentityUserName(message.CognitoUserName);
        ValidateCreatedAtTime(message.CreatedAt);
        
        _logger.LogTrace($"Handling message: {message}");
        var appUserId = await _linkService.LinkAsync(message.CognitoUserName, message.CreatedAt);
        return !string.IsNullOrEmpty(appUserId);
    }
    
    private void ValidateIdentityUserName(string? identityUserName)
    {
        if (string.IsNullOrEmpty(identityUserName))
        {
            _logger.LogError("Identity UserName is null or empty");
            throw new ArgumentException("Identity UserName is null or empty");
        }
    }
    
    private void ValidateCreatedAtTime(DateTimeOffset createdAt)
    {
        var utcNow = DateTimeOffset.UtcNow;
        var minSignUpTime = utcNow.AddMinutes(-UserRegistration.MaxRegistrationDelayDays);
        if (createdAt >= minSignUpTime && createdAt <= utcNow)
        {
            return;
        }

        _logger.LogError("CognitoPoolId is null or empty");
        throw new ArgumentException("Identity UserName is null or empty");
    }
}
