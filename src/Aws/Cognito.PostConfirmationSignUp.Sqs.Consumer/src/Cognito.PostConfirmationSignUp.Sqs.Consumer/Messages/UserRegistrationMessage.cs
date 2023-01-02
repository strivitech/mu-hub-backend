using System.Text.Json.Serialization;

using Sqs.Shared.Data;

namespace Cognito.PostConfirmationSignUp.Sqs.Consumer.Messages;

public class UserRegistrationMessage : IMessage
{
    [JsonPropertyName("cognitoUserId")]
    public string CognitoUserName { get; set; } = null!;
    
    [JsonPropertyName("cognitoUserPoolId")]
    public string CognitoUserPoolId { get; set; } = null!;

    [JsonIgnore]
    public string MessageTypeName => nameof(UserRegistrationMessage);
}
