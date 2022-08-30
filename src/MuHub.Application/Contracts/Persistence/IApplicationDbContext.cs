using Microsoft.EntityFrameworkCore;

using MuHub.Domain.Entities;

namespace MuHub.Application.Contracts.Persistence;

public interface IApplicationDbContext : IApplicationDbContextInstanceResolver
{
    DbSet<Info> Info { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
