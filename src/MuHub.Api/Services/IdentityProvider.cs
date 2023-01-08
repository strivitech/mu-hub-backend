using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;

using Cognito.Common;

using Microsoft.Extensions.Options;

using MuHub.Api.Common.Configurations.Identity;
using MuHub.Api.Responses;
using MuHub.Application.Contracts.Infrastructure;
using MuHub.Application.Models.Responses;

namespace MuHub.Api.Services;

/// <summary>
/// 
/// </summary>
public class IdentityProvider : IIdentityProvider
{
    private readonly IAmazonCognitoIdentityProvider _cognitoIdentityProvider;
    private readonly AwsCognitoUserPoolOptions _cognitoUserPoolOptions;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="cognitoIdentityProvider"></param>
    /// <param name="awsCognitoOptions"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public IdentityProvider(
        IAmazonCognitoIdentityProvider cognitoIdentityProvider,
        IOptions<AwsCognitoUserPoolOptions> awsCognitoOptions)
    {
        _cognitoIdentityProvider =
            cognitoIdentityProvider ?? throw new ArgumentNullException(nameof(cognitoIdentityProvider));
        _cognitoUserPoolOptions = awsCognitoOptions.Value ?? throw new ArgumentNullException(nameof(awsCognitoOptions));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="userName"></param>
    /// <returns></returns>
    public async Task<GetIdentityProviderUserResponse> GetIdentityProviderUserAsync(string userName)
    {
        if (string.IsNullOrEmpty(userName))
        {
            throw new ArgumentException($"{nameof(userName)} should not be null or empty");
        }

        var request = new AdminGetUserRequest { Username = userName, UserPoolId = _cognitoUserPoolOptions.UserPoolId };
        var response = await _cognitoIdentityProvider.AdminGetUserAsync(request);

        if (response is null)
        {
            throw new InvalidOperationException($"Failed to fetch user data for UserName {userName}");
        }

        return GenerateGetIdentityProviderUserResponse(response);
    }

    private static GetIdentityProviderUserResponse GenerateGetIdentityProviderUserResponse(
        AdminGetUserResponse response)
    {
        var userAttributes = response.UserAttributes;
        
        return new GetIdentityProviderUserResponse
        {
            UserName = response.Username,
            Email = FindRequiredValueFromUserAttributes(userAttributes, UserAttributesNames.Email),
            EmailConfirmed =
                Boolean.Parse(FindRequiredValueFromUserAttributes(userAttributes, UserAttributesNames.EmailConfirmed)),
            PhoneNumber = FindRequiredValueFromUserAttributes(userAttributes, UserAttributesNames.PhoneNumber),
            PhoneNumberConfirmed =
                Boolean.Parse(FindRequiredValueFromUserAttributes(userAttributes,
                    UserAttributesNames.PhoneNumberConfirmed))
        };
    }

    private static string FindRequiredValueFromUserAttributes(List<AttributeType> userAttributes, string propName)
        => userAttributes.Find(x => x.Name == propName)?.Value
           ?? throw new InvalidOperationException($"Value for name {propName} is null");
}
