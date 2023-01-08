namespace MuHub.Api.Requests;

/// <summary>
/// 
/// </summary>
public class UserLinkRegistrationDataRequest
{
    /// <summary>
    /// 
    /// </summary>
    public string UserName { get; set; } = null!;
    
    /// <summary>
    /// 
    /// </summary>
    public DateTimeOffset? CreatedAt { get; set; } = null!;
}
