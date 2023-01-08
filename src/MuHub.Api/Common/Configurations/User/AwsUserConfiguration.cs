namespace MuHub.Api.Common.Configurations.User;

public class AwsUserConfiguration
{
    public const string SectionName = "Credentials:Aws";
    
    public string AccessKey { get; set; }
    public string SecretKey { get; set; }
}
