﻿namespace MuHub.Api.Services;

/// <summary>
/// 
/// </summary>
public interface IMarketCoinsInteractionService
{
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    Task UpdateCoinInformation(IList<string> ids);
}
