using System.Text.Json;

using Amazon.CognitoIdentityProvider;
using Amazon.Lambda.Core;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace Cognito.PostConfirmation;

public class PostConfirmationFunction
{
    /// <summary>
    /// A simple function that takes a string and does a ToUpper
    /// </summary>
    /// <param name="cognitoEvent"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    [LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.CamelCaseLambdaJsonSerializer))]
    public async Task<JsonElement> HandleSignUpAsync(JsonElement cognitoEvent, ILambdaContext context)
    {
        context.Logger.LogTrace("Start handling PostConfirmationSignUpFunction");
        using var client = new AmazonCognitoIdentityProviderClient();
        return await new PostConfirmationConfirmSignUpHandler(cognitoEvent, context.Logger, client)
            .HandleTriggerEventAsync();
    }
}
