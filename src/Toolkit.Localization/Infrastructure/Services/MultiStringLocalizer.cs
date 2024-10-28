using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;

namespace Toolkit.Localizations.Infrastructure.Services
{
    /// <summary>
    /// <see cref="IStringLocalizer"/>, который поддерживает обращение к нескольким <see cref="IStringLocalizer{T}"/>
    /// </summary>
    internal sealed class MultiStringLocalizer : IStringLocalizer
    {
        private readonly List<IStringLocalizer> _localizers;
        private readonly ILogger<MultiStringLocalizer> _logger;
        private readonly CultureInfo _cultureInfo;

        #region Constructors

        public MultiStringLocalizer(List<IStringLocalizer> localizers, ILogger<MultiStringLocalizer> logger)
        {
            if (localizers == null)
                throw new ArgumentNullException(nameof(localizers));
            if (localizers.Count == 0)
                throw new ArgumentException("Empty not supported", nameof(localizers));
            _localizers = localizers;
            _logger = logger ?? NullLogger<MultiStringLocalizer>.Instance;
        }

        [Obsolete("Obsolete")]
        private MultiStringLocalizer(List<IStringLocalizer> localizers, ILogger<MultiStringLocalizer> logger, CultureInfo cultureInfo)
            : this(localizers.Select(l => l.WithCulture(cultureInfo)).ToList(), logger)
        {
            _cultureInfo = cultureInfo;
        } 

        #endregion

        public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
        {
            var result = new Dictionary<string, LocalizedString>();
            foreach (var localizer in _localizers)
            {
                foreach (var entry in localizer.GetAllStrings(includeParentCultures))
                {
                    if (!result.ContainsKey(entry.Name))
                    {
                        result.Add(entry.Name, entry);
                    }
                }
            }

            return result.Values;
        }

        [Obsolete("Obsolete")]
        public IStringLocalizer WithCulture(CultureInfo culture)
        {
            return culture == null 
                ? new MultiStringLocalizer(_localizers, _logger) 
                : new MultiStringLocalizer(_localizers, _logger, culture);
        }

        public LocalizedString this[string name]
        {
            get
            {
                LocalizedString result = null;
                foreach (var localizer in _localizers)
                {
                    result = localizer[name];
                    if (!result.ResourceNotFound)
                    {
                        return result;
                    }
                }
                Debug.Assert(result != null);
                return result;
            }
        }
        public LocalizedString this[string name, params object[] arguments]
        {
            get
            {
                LocalizedString result = null;
                foreach (var localizer in _localizers)
                {
                    result = localizer[name, arguments];
                    if (!result.ResourceNotFound)
                    {
                        return result;
                    }
                }
                Debug.Assert(result != null);
                return result;
            }
        }

        public override string ToString() 
            => _localizers?.Select(i => i.ToString()).Aggregate((result, item) => result + Environment.NewLine + item) ?? string.Empty;
    }
}
