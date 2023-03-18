using Microsoft.EntityFrameworkCore;

namespace MuHub.Application.Contracts.Persistence;

/// <summary>
/// Resolves the instance of the internal application's <see cref="DbContext"/>.
/// </summary>
public interface IApplicationDbContextInstanceResolver
{
    /// <summary>
    /// Gets the instance of the internal application's <see cref="DbContext"/>.
    /// </summary>
    public DbContext Instance { get; }
}
