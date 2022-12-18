using Cognito.Events.Shared.Core;

namespace Cognito.Events.Shared.Extensions;

public static class EventExtensions
{
    private static readonly List<Func<ICognitoTriggerEvent, bool>>
        CognitoTriggerEventFilters = new()
        {
            IsRegionValid,
            IsVersionValid,
            IsTriggerSourceValid,
            IsUserNameValid,
            IsUserPoolIdValid,
            IsCallerContextValid
        };

    public static bool IsCognitoTriggerEventValid(this ICognitoTriggerEvent cognitoEvent)
    {
        ArgumentNullException.ThrowIfNull(cognitoEvent);
        return CognitoTriggerEventFilters.All(filter => filter(cognitoEvent));
    }

    public static bool IsRegionValid(ICognitoTriggerEvent cognitoEvent) =>
        !string.IsNullOrWhiteSpace(cognitoEvent.Region);

    public static bool IsVersionValid(ICognitoTriggerEvent cognitoEvent) =>
        !string.IsNullOrWhiteSpace(cognitoEvent.Version);

    public static bool IsTriggerSourceValid(ICognitoTriggerEvent cognitoEvent) =>
        !string.IsNullOrWhiteSpace(cognitoEvent.TriggerSource);

    public static bool IsUserNameValid(ICognitoTriggerEvent cognitoEvent) =>
        !string.IsNullOrWhiteSpace(cognitoEvent.UserName);

    public static bool IsUserPoolIdValid(ICognitoTriggerEvent cognitoEvent) =>
        !string.IsNullOrWhiteSpace(cognitoEvent.UserPoolId);
    
    public static bool IsCallerContextValid(ICognitoTriggerEvent cognitoEvent) =>
        !string.IsNullOrWhiteSpace(cognitoEvent.CallerContext.ClientId)
        && !string.IsNullOrWhiteSpace(cognitoEvent.CallerContext.AwsSdkVersion);
}
