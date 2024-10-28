using System;
using System.Globalization;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace Toolkit.Localizations.Infrastructure.Services
{
    /// <summary>
    /// Provides strings for <typeparamref name="TResourceSource"/>.
    /// </summary>
    /// <typeparam name="TResourceSource">The <see cref="Type"/> to provide strings for.</typeparam>
    internal class LoggedStringLocalizer<TResourceSource> : StringLocalizer<TResourceSource>
    {
        private readonly ILogger<LoggedStringLocalizer<TResourceSource>> _logger;
        private readonly CultureInfo _cultureInfo;

        #region Constructors

        /// <summary> Creates a new <see cref="StringLocalizer{TResourceSource}"/>.</summary>
        /// <param name="factory">The <see cref="IStringLocalizerFactory"/> to use.</param>
        public LoggedStringLocalizer(IStringLocalizerFactory factory, ILogger<LoggedStringLocalizer<TResourceSource>> logger) : base(factory)
        {
            _logger = logger;
        }

        [Obsolete("Obsolete")]
        private LoggedStringLocalizer(IStringLocalizerFactory factory, ILogger<LoggedStringLocalizer<TResourceSource>> logger, CultureInfo cultureInfo)
            : this(factory, logger)
        {
            _cultureInfo = cultureInfo;
        }

        #endregion

        public override LocalizedString this[string name]
        {
            get
            {
                var result = base[name];
                if (!result.ResourceNotFound)
                {
                    _logger.LogTrace(
                        $"The localizer managed to get the value from the key '{result.Name}' in the resource '{result.SearchedLocation}' for the culture '{_cultureInfo ?? CultureInfo.CurrentUICulture}'.");
                    return result;
                }

                _logger.LogWarning(
                    $"Localizer failed to retrieve value for key '{result.Name}' in resource '{result.SearchedLocation}' for culture '{_cultureInfo ?? CultureInfo.CurrentUICulture}'.");
                return result;
            }
        }

        public override LocalizedString this[string name, params object[] arguments]
        {
            get
            {
                var result = base[string.Format(name, arguments)];
                if (!result.ResourceNotFound)
                {
                    _logger.LogTrace(
                        $"The localizer '{GetType()}' managed to get the value from the key '{result.Name}' in the resource '{result.SearchedLocation}' for the culture '{_cultureInfo ?? CultureInfo.CurrentUICulture}'.");
                    return result;
                }

                _logger.LogWarning(
                    $"Localizer '{GetType()}' failed to retrieve value for key '{result.Name}' in resource '{result.SearchedLocation}' for culture '{_cultureInfo ?? CultureInfo.CurrentUICulture}'.");
                return result;
            }
        }
    }
}