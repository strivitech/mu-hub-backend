using ErrorOr;

using MuHub.Application.Models.Data;
using MuHub.Application.Models.Requests.Info;

namespace MuHub.Application.Services.Interfaces;

public interface IInfoService
{
    /// <summary>
    /// Asynchronously creates info.
    /// </summary>
    /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.
    /// The task result contains an instance of <see cref="InfoDto"/>.
    /// </returns>
    Task<ErrorOr<InfoDto>> CreateAsync(CreateInfoRequest request);
}
