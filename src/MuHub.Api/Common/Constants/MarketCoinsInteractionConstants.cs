namespace MuHub.Api.Common.Constants;

/// <summary>
/// Constants for Market Coins Interaction
/// </summary>
public static class MarketCoinsInteractionConstants
{
    /// <summary>
    /// Per page default value.
    /// </summary>
    public const int PerPage = 250;
    
    /// <summary>
    /// Desired ids count default value. It's used to warning user about too many ids.
    /// </summary>
    public const int DesiredIdsCountMaxValue = 10000;
    
    /// <summary>
    /// Valid data period in seconds. Data within this period is considered valid.
    /// </summary>
    public const int ValidDataPeriodSeconds = 45;
}
