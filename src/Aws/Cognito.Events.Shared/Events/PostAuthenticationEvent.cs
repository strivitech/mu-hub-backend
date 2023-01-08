using Cognito.Events.Shared.Core;

namespace Cognito.Events.Shared.Events;

public class PostAuthenticationEvent : CognitoTriggerEvent<PostAuthenticationRequest, PostAuthenticationResponse>
{
}
