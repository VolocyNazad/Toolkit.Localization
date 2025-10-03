using Microsoft.Extensions.Localization;
using Toolkit.Localizations.Infrastructure.Attributes;

namespace Toolkit.Localization.Testing
{
    [AutoConstructor(addParameterless: true)]
    internal sealed partial class Service
    {
        private readonly IStringLocalizer<Service> _localizer;

        [AutoConstructorInitializer]
        private void Initialize()
        {
            string value = _localizer["First"] + " " + _localizer["Second"];
        }
    }

    [AutoConstructor(addParameterless: true)]
    [LocalizationAlias("Alias")]
    internal sealed partial class Service2
    {
        private readonly IStringLocalizer<Service2> _localizer;

        [AutoConstructorInitializer]
        private void Initialize()
        {
            string value = _localizer["First"] + " " + _localizer["Second"];
        }
    }

    [AutoConstructor(addParameterless: true)]
    internal sealed partial class Service<T>
    {
        private readonly IStringLocalizer<Service<T>> _localizer;
        private readonly IStringLocalizer<object> _localizer1;

        [AutoConstructorInitializer]
        private void Initialize()
        {
            string value = _localizer["First"] + " " + _localizer["Second"];
            var a = _localizer1;
        }
    }
}
