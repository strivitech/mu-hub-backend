using Amazon.SQS.Model;

using Sqs.Shared.Data;

namespace Sqs.Shared.Services;

public interface ISqsSenderService<in T>
    where T : IMessage
{
    Task<SendMessageResponse> SendMessageAsync(T message);
}
