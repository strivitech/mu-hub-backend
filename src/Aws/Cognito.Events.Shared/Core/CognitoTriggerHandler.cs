using System.Runtime.Serialization;
using System.Text.Json;

using Amazon.Lambda.Core;

using Cognito.Events.Shared.Extensions;

namespace Cognito.Events.Shared.Core;

public abstract class CognitoTriggerHandler<T> : ICognitoTriggerHandler
    where T : ICognitoTriggerEvent
{
    protected T TriggerEvent { get; }

    public abstract string TriggerSource { get; }

    protected ILambdaLogger Logger { get; }

    protected CognitoTriggerHandler(JsonElement cognitoEvent, ILambdaLogger logger)
    {
        TriggerEvent = RetrieveRequiredEvent(cognitoEvent);
        Logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public virtual JsonElement HandleTriggerEvent()
    {
        Logger.LogInformation($"Handling Cognito event trigger source '${TriggerSource}'");

        Logger.LogDebug(JsonSerializer.Serialize(TriggerEvent));

        return JsonSerializer.SerializeToElement(TriggerEvent);
    }

    public virtual async Task<JsonElement> HandleTriggerEventAsync()
    {
        return await Task.FromResult(HandleTriggerEvent());
    }

    private static T RetrieveRequiredEvent(JsonElement cognitoEvent)
    {
        var deserializedEvent = cognitoEvent.Deserialize<T>();
        if (deserializedEvent is null)
        {
            throw new SerializationException("Unable to deserialize Cognito event");
        }
        deserializedEvent.IsCognitoTriggerEventValid();
        return deserializedEvent;
    }
}
