using Sqs.Shared.Data;

namespace Sqs.Shared.Handlers;

public interface IMessageHandler<in T> where T : IMessage 
{
    Task<bool> HandleAsync(T message);
}
