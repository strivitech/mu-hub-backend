using Asp.Versioning;

using Microsoft.AspNetCore.Mvc;

using MuHub.Application.Contracts.Infrastructure;
using MuHub.Application.Models.Data.WatchList;
using MuHub.Application.Services.Interfaces;
using MuHub.Permissions;
using MuHub.Permissions.Policy;

namespace MuHub.Api.Controllers.V1;

/// <summary>
/// Watch list controller.
/// </summary>
[ApiController]
[ApiVersion(1.0)]
[Route("api/v{version:apiVersion}/[controller]")]
public class WatchListController : MuControllerBase
{
    private readonly IWatchListService _watchListService;
    private readonly ICurrentUserService _currentUserService;

    public WatchListController(IWatchListService watchListService,
        ICurrentUserService currentUserService)
    {
        _watchListService = watchListService;
        _currentUserService = currentUserService;
    }

    /// <summary>
    /// Adds a coin to the watch list.
    /// </summary>
    /// <param name="request">The request model.</param>
    /// <returns>An instance of <see cref="IActionResult"/>.</returns>
    [HttpPost]
    [HasPermission(Permission.WatchListCreate)]
    public async Task<IActionResult> AddToWatchList(AddToWatchListRequest request)
    {
        var isAdded = await _watchListService.AddToWatchListAsync(request.CoinId, _currentUserService.UserSessionData.UserId);

        return isAdded
            ? Ok("Added to watch list")
            : BadRequest("Failed to add to watch list");
    }
}
