namespace MuHub.IdentityProvider.Configurations.Store;

/// <summary>
/// Operational store configuration.
/// </summary>
public static class OperationalStoreConfiguration
{
    public const bool EnableTokenCleanup = true;
    public const int TokenCleanupInterval = 3600;
}
