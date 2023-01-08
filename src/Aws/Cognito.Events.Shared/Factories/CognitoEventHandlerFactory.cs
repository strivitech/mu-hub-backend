using System.Text.Json;

using Amazon.Lambda.Core;

using Cognito.Events.Shared.Core;
using Cognito.Events.Shared.Handlers;

namespace Cognito.Events.Shared.Factories;

public class CognitoEventHandlerFactory
{
    private readonly ILambdaLogger _logger;

    private static bool IsCognitoTriggerHandler(Type type) =>
        type.IsParticularGeneric(typeof(CognitoTriggerHandler<>));

    public CognitoEventHandlerFactory(ILambdaLogger logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public ICognitoTriggerHandler? GetHandler(JsonElement cognitoEvent)
    {
        var triggerEvent = cognitoEvent.Deserialize<CognitoTriggerEventBase>()
            ?? throw new ArgumentNullException(nameof(cognitoEvent));

        _logger.LogDebug($"Trigger source: {triggerEvent.TriggerSource}.");

        var handlers = from assembly in AppDomain.CurrentDomain.GetAssemblies()
            from type in assembly.GetTypes()
            where type.AnyBaseType(IsCognitoTriggerHandler)
            select type;

        foreach (var handler in handlers)
        {
            _logger.LogDebug($"Found handler: '{handler.Name}'");

            var property = handler.GetField("TriggerSourceName");

            if (property != null)
            {
                _logger.LogDebug("Found TriggerSourceName property");

                var value = property.GetValue(null);

                if (value != null && value.ToString() == triggerEvent.TriggerSource)
                {
                    _logger.LogDebug("Trigger source matches event");
                    _logger.LogInformation($"Using Cognito event handler {handler.Name}");
                    return (ICognitoTriggerHandler?)Activator.CreateInstance(handler, cognitoEvent, _logger);
                }
            }
        }

        _logger.LogWarning($"Could not find a suitable handler for trigger source '{triggerEvent.TriggerSource}'");
        return new NoActionHandler(cognitoEvent, _logger);
    }
}
