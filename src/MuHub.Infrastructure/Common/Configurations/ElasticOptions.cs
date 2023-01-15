namespace MuHub.Infrastructure.Common.Configurations;

public class ElasticOptions
{
    public const string Name = "ElasticSearch";
    
    public string Uri { get; set; } = null!;

    public bool AutoRegisterTemplate { get; set; }

    public int NumberOfShards { get; set; }

    public int NumberOfReplicas { get; set; } 
}
