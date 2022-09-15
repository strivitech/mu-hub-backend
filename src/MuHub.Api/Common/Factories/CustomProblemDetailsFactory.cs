using System.Diagnostics;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Options;

using MuHub.Api.Common.Constants;

namespace MuHub.Api.Common.Factories;

/// <summary>
/// A custom factory for creating <see cref="ProblemDetails"/> instances.
/// </summary>
public sealed class CustomProblemDetailsFactory : ProblemDetailsFactory
{
    private readonly ApiBehaviorOptions _options;

    /// <summary>
    /// Initializes a new instance of the <see cref="CustomProblemDetailsFactory"/> class.
    /// </summary>
    /// <param name="options">ApiBehaviorOptions.</param>
    /// <exception cref="ArgumentNullException">If option value is null.</exception>
    public CustomProblemDetailsFactory(IOptions<ApiBehaviorOptions> options)
    {
        _options = options?.Value ?? throw new ArgumentNullException(nameof(options));
    }

    /// <summary>
    /// Creates a <see cref="ProblemDetails"/> instance.
    /// </summary>
    /// <param name="httpContext">Http context.</param>
    /// <param name="statusCode">Status code.</param>
    /// <param name="title">Title.</param>
    /// <param name="type">Type.</param>
    /// <param name="detail">Detail.</param>
    /// <param name="instance">Instance.</param>
    /// <returns>An instance of <see cref="ProblemDetails"/></returns>
    public override ProblemDetails CreateProblemDetails(
        HttpContext httpContext,
        int? statusCode = null,
        string? title = null,
        string? type = null,
        string? detail = null,
        string? instance = null)
    {
        statusCode ??= StatusCodes.Status500InternalServerError;

        var problemDetails = new ProblemDetails
        {
            Status = statusCode,
            Title = title,
            Type = type,
            Detail = detail,
            Instance = instance,
        };

        ApplyProblemDetailsDefaults(httpContext, problemDetails, statusCode.Value);

        return problemDetails;
    }

    /// <summary>
    /// Creates a <see cref="ValidationProblemDetails"/> instance.
    /// </summary>
    /// <param name="httpContext">Http context.</param>
    /// <param name="modelStateDictionary">ModelState dictionary.</param>
    /// <param name="statusCode">Status code.</param>
    /// <param name="title">Title.</param>
    /// <param name="type">Type.</param>
    /// <param name="detail">Detail.</param>
    /// <param name="instance">Instance.</param>
    /// <returns>An instance of <see cref="ValidationProblemDetails"/></returns>
    public override ValidationProblemDetails CreateValidationProblemDetails(
        HttpContext httpContext,
        ModelStateDictionary modelStateDictionary,
        int? statusCode = null,
        string? title = null,
        string? type = null,
        string? detail = null,
        string? instance = null)
    {
        ArgumentNullException.ThrowIfNull(modelStateDictionary);

        statusCode ??= StatusCodes.Status400BadRequest;

        var problemDetails = new ValidationProblemDetails(modelStateDictionary)
        {
            Status = statusCode, Type = type, Detail = detail, Instance = instance,
        };

        if (title != null)
        {
            // For validation problem details, don't overwrite the default title with null.
            problemDetails.Title = title;
        }

        ApplyProblemDetailsDefaults(httpContext, problemDetails, statusCode.Value);

        return problemDetails;
    }

    private void ApplyProblemDetailsDefaults(HttpContext httpContext, ProblemDetails problemDetails, int statusCode)
    {
        problemDetails.Status ??= statusCode;

        if (_options.ClientErrorMapping.TryGetValue(statusCode, out var clientErrorData))
        {
            problemDetails.Title ??= clientErrorData.Title;
            problemDetails.Type ??= clientErrorData.Link;
        }

        var traceId = Activity.Current?.Id ?? httpContext?.TraceIdentifier;
        if (traceId != null)
        {
            problemDetails.Extensions["traceId"] = traceId;
        }

        if (httpContext?.Items[ProblemDetailsConstants.AdditionalContext] is Dictionary<string, string[]> additionalContext)
        {
            problemDetails.Extensions.Add(ProblemDetailsConstants.AdditionalContext, additionalContext);
        }
    }
}
