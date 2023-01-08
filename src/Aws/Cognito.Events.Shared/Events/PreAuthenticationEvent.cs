using Cognito.Events.Shared.Core;

namespace Cognito.Events.Shared.Events;

public class PreAuthenticationEvent : CognitoTriggerEvent<PreAuthenticationRequest, PreAuthenticationResponse>
{
}
