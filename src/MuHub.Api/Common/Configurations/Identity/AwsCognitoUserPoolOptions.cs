namespace MuHub.Api.Common.Configurations.Identity;

/// <summary>
/// 
/// </summary>
public class AwsCognitoUserPoolOptions
{
    /// <summary>
    /// 
    /// </summary>
    public const string SectionName = "Authentication:Cognito";
    
    /// <summary>
    /// 
    /// </summary>
    public string UserPoolId { get; set; } = null!;
}
