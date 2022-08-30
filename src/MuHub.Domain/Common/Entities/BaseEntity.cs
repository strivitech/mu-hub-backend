namespace MuHub.Domain.Common.Entities;

public abstract class BaseEntity<TKey> : IKeyedEntity<TKey>
{
    public TKey Id { get; set; } = default!;
}
