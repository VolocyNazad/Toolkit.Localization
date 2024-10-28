using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Toolkit.Localizations.Abstractions.Services;

namespace Toolkit.Localizations.Infrastructure.Services
{
    /// <summary>
    /// Фабрика локализации, которая в отличии от <see cref="ResourceManagerStringLocalizerFactory"/> способна выполнять поиск ресурса 
    /// локализации не только по типу <see cref="{T}"/> <see cref="IStringLocalizer{T}"/>, но и также: <br /> <br />
    ///     - выполнять поиск в интерфейсах, которые использует класс, <br />
    ///     - выполнять поиск в родительских классах, <br />
    ///     - выполнять поиск в дженериках класса, <br />
    ///     - а также использовать совсем не связанные с классом типы <br />
    /// </summary>
    /// <remarks>
    /// Поддерживает многоуровневою проверку классов на наличие атрибутов.
    /// Использует преимущества <see cref="GenericResourceManagerStringLocalizerFactory"/>.
    /// </remarks>
    internal sealed class InheritStringLocalizerFactory : IStringLocalizerFactory
    {
        private readonly ConcurrentDictionary<Type, IStringLocalizer> _cache = new ConcurrentDictionary<Type, IStringLocalizer>();
        private readonly GenericResourceManagerStringLocalizerFactory _factory;
        private readonly IEnumerable<ILocalizationTypeDefiner> _localizationTypeDefiners;
        private readonly ILoggerFactory _loggerFactory;

        #region Constructors

        public InheritStringLocalizerFactory(
            IEnumerable<ILocalizationTypeDefiner> localizationTypeDefiners,
            IOptions<LocalizationOptions> localizationOptions, 
            ILoggerFactory loggerFactory)
        {
            _factory = new GenericResourceManagerStringLocalizerFactory(localizationOptions, loggerFactory);
            _localizationTypeDefiners = localizationTypeDefiners;
            _loggerFactory = loggerFactory;
        }

        #endregion

        public IStringLocalizer Create(string baseName, string location) => _factory.Create(baseName, location);
        public IStringLocalizer Create(Type resourceSource)
        {
            if (resourceSource == null)
                throw new ArgumentNullException(nameof(resourceSource));

            return CreateStringLocalizer(resourceSource);
        }
        private IStringLocalizer CreateStringLocalizer(Type type) => _cache.GetOrAdd(type, CreateStringLocalizerDirect);
        private IStringLocalizer CreateStringLocalizerDirect(Type type)
        {
            bool isMultiLocalizer = false;
            var localizers = new List<IStringLocalizer>();

            // Локализатор для текущего типа
            var mainLocalizer = _factory.Create(type);
            localizers.Add(mainLocalizer);

            foreach (ILocalizationTypeDefiner definer in _localizationTypeDefiners)
            {
                foreach (Type dependentType in definer.Define(type))
                {
                    localizers.Add(CreateStringLocalizer(dependentType));
                    if (!isMultiLocalizer) isMultiLocalizer = true;
                }
            }

            return isMultiLocalizer 
                ? new MultiStringLocalizer(localizers, _loggerFactory.CreateLogger<MultiStringLocalizer>()) 
                : mainLocalizer;
        }
    }
}
