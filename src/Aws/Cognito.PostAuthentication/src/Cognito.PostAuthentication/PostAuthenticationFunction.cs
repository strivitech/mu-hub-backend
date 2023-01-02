using System.Text.Json;

using Amazon.CognitoIdentityProvider;
using Amazon.Lambda.Core;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace Cognito.PostAuthentication;

public class PostAuthenticationFunction
{
    /// <summary>
    /// A simple function that takes a string and does a ToUpper
    /// </summary>
    /// <param name="cognitoEvent"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    [LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.CamelCaseLambdaJsonSerializer))]
    public async Task<JsonElement> HandleAsync(JsonElement cognitoEvent, ILambdaContext context)
    {
        context.Logger.LogTrace("Start handling PostAuthenticationFunction");
        using var client = new AmazonCognitoIdentityProviderClient();
        return await new PostAuthenticationHandler(cognitoEvent, context.Logger).HandleTriggerEventAsync();
    }
}
