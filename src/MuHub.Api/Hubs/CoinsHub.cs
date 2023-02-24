using Microsoft.AspNetCore.SignalR;

using MuHub.Api.Hubs.Clients;

namespace MuHub.Api.Hubs;

/// <summary>
/// Coins hub.
/// </summary>
public class CoinsHub : Hub<ICoinsClient>
{
}
