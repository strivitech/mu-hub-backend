namespace MuHub.Domain.Common.Entities;

public interface IKeyedEntity<TKey> : IEntity
{
    public TKey Id { get; set; }
}
