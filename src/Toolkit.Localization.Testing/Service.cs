using Microsoft.Extensions.Localization;

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
    internal sealed partial class Service<T>
    {
        private readonly IStringLocalizer<Service<T>> _localizer;

        [AutoConstructorInitializer]
        private void Initialize()
        {
            string value = _localizer["First"] + " " + _localizer["Second"];
        }
    }
}
