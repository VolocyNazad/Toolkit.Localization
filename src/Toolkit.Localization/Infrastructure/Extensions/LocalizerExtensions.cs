using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using Toolkit.Localizations.Abstractions.Services;
using Toolkit.Localizations.Infrastructure.Services;

namespace Toolkit.Localizations.Infrastructure.Extensions;

/// <summary>
/// Extension methods for setting up localization services in an <see cref="IServiceCollection" />.
/// </summary>
public static class LocalizerExtensions
{
    /// <summary>
    /// Adds services required for application localization.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add the services to.</param>
    /// <param name="action">
    /// An <see cref="Action{LocalizationOptions}"/> to configure the <see cref="LocalizationOptions"/>.
    /// </param>
    /// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
    public static IServiceCollection AddInheritLocalization(this IServiceCollection services, Action<LocalizationOptions>? action = null)
    {
        if (services == null)  throw new ArgumentNullException(nameof(services));

        services.AddOptions();

        services.TryAddSingleton<IStringLocalizerFactory, InheritStringLocalizerFactory>();
        services.TryAddTransient(typeof(IStringLocalizer<>), typeof(LoggedStringLocalizer<>));

        services.AddSingleton<ILocalizationTypeDefiner, InheritedLocalizationTypeDefiner>();
        services.AddSingleton<ILocalizationTypeDefiner, PseudoLocalizationTypeDefiner>();
        services.AddSingleton<ILocalizationTypeDefiner, InterfaceLocalizationTypeDefiner>();
        services.AddSingleton<ILocalizationTypeDefiner, GenericLocalizationTypeDefiner>();
        services.AddSingleton<ILocalizationTypeDefiner, AllGenericLocalizationTypeDefiner>();

        services.AddSingleton((IConfigureOptions<LocalizationOptions>)new DefaultLocalizationOptionsResourcePathConfigureOptions("Localizations"));
        if (action != null) services.Configure(action);

        return services;
    }
}
