namespace MuHub.Domain.Common.Entities;

public abstract class BaseEntity<TKey> : BaseEntity, IKeyedEntity<TKey>
{
    public TKey Id { get; set; } = default!;
}

public abstract class BaseEntity : IEntity
{
}
