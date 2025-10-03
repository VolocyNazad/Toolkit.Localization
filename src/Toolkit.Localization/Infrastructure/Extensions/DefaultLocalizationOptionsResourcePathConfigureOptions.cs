using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;

namespace Toolkit.Localizations.Infrastructure.Extensions;

internal sealed class DefaultLocalizationOptionsResourcePathConfigureOptions
    : ConfigureOptions<LocalizationOptions>
{
    public DefaultLocalizationOptionsResourcePathConfigureOptions(string path) : base(options =>
    {
        options.ResourcesPath = path;
    }) { }
}
