using System.Reflection;

using FluentValidation;

using MuHub.Api.Common.Factories;

namespace MuHub.Api.Common.Extensions;

/// <summary>
/// FluentValidation extensions.
/// </summary>
public static class FluentValidationExtensions
{
    /// <summary>
    /// Registers all validators in the given assemblies.
    /// </summary>
    /// <param name="builder">Mvc builder.</param>
    /// <param name="assemblies">List of assemblies to scan for validators.</param>
    public static void RegisterValidatorsInAssemblyList(this IMvcBuilder builder, List<Assembly> assemblies)
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentNullException.ThrowIfNull(assemblies);
        
        if (!assemblies.Any())
        {
            return;
        }

        // Register the validators from the respective assemblies
        assemblies.ForEach(x => builder.Services.AddValidatorsFromAssembly(x));
        
        // Register the custom validator factory, to get `IValidator` instances and validate requests
        builder.Services.AddSingleton<ICustomModelValidationFactory, CustomModelValidationFactory>();
    }
}
