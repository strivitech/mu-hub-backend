namespace MuHub.Api.Common.Configurations.Identity;

/// <summary>
/// 
/// </summary>
public class AwsCognitoAuthenticationConfiguration
{
    /// <summary>
    /// 
    /// </summary>
    public const string SectionName = "Authentication:Cognito";

    /// <summary>
    /// 
    /// </summary>
    public string Region { get; set; } = null!;
    
    /// <summary>
    /// 
    /// </summary>
    public string UserPoolId { get; set; } = null!;
    
    /// <summary>
    /// 
    /// </summary>
    public string ValidIssuer { get; set; } = null!;
    
    /// <summary>
    /// 
    /// </summary>
    public bool ValidateIssuerSigningKey { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public bool ValidateIssuer { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public bool ValidateLifetime { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public string ValidAudience { get; set; } = null!;
    
    /// <summary>
    /// 
    /// </summary>
    public bool ValidateAudience { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public string RoleClaimType { get; set; } = null!;
    
    /// <summary>
    /// 
    /// </summary>
    public string MetadataAddress { get; set; } = null!;
}
