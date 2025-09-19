using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using Toolkit.Localizations.Abstractions.Services;
using Toolkit.Localizations.Infrastructure.Services;

namespace Toolkit.Localizations.Infrastructure.Extensions
{
    /// <summary>
    /// Выполняет регистрацию сервисов для локализации, которые поддерживают соответствующие атрибуты/>
    /// </summary>
    public static class LocalizerExtensions
    {
        public static IServiceCollection AddInheritLocalization(this IServiceCollection services, Action<LocalizationOptions> action = null)
        {
            if (services == null)  throw new ArgumentNullException(nameof(services));

            services.AddOptions()
                .AddSingleton<IStringLocalizerFactory, InheritStringLocalizerFactory>()
                .AddTransient(typeof(IStringLocalizer<>), typeof(LoggedStringLocalizer<>))
                .AddSingleton<ILocalizationTypeDefiner, InheritedLocalizationTypeDefiner>()
                .AddSingleton<ILocalizationTypeDefiner, PseudoLocalizationTypeDefiner>()
                .AddSingleton<ILocalizationTypeDefiner, InterfaceLocalizationTypeDefiner>()
                .AddSingleton<ILocalizationTypeDefiner, GenericLocalizationTypeDefiner>()
                .AddSingleton<ILocalizationTypeDefiner, AllGenericLocalizationTypeDefiner>();
            services.AddSingleton((IConfigureOptions<LocalizationOptions>)new DefaultLocalizationOptionsResourcePathConfigureOptions("Localizations"));
            if (action != null) services.Configure(action);

            return services;
        }
    }
}
