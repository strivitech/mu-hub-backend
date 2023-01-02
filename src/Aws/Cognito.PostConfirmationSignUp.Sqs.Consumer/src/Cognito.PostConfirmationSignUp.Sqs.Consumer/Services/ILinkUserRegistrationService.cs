namespace Cognito.PostConfirmationSignUp.Sqs.Consumer.Services;

public interface ILinkUserRegistrationService
{
    Task<string> LinkAsync(string identityUserName, string cognitoPoolId);
}
