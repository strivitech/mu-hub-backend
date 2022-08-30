using Microsoft.EntityFrameworkCore;

namespace MuHub.Application.Contracts.Persistence;

public interface IApplicationDbContextInstanceResolver
{
    public DbContext Instance { get; } // maybe remove this and create particular things like TransactionProcessor etc.
}
