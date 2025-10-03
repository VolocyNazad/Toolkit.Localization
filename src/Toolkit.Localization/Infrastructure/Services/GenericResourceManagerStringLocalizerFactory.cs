using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Reflection;
using Toolkit.Localizations.Infrastructure.Attributes;

namespace Toolkit.Localizations.Infrastructure.Services;

/// <summary>
/// Фабрика локализации, которая в отличии от <see cref="ResourceManagerStringLocalizerFactory"/> способна выполнять поиск ресурса локализации по обобщенным классам
/// </summary>
/// <remarks>
/// Базовая реализация <see cref="IStringLocalizerFactory"/> из коробки <see cref="GenericResourceManagerStringLocalizerFactory"/>
/// Имеет существенный недостаток. Она не может найти ресурс, если <see cref="IStringLocalizer{T}"/> в качестве обобщенного типа имеет обобщенный тип. 
/// Данная реализация устраняет данный недостаток
/// </remarks>
internal sealed class GenericResourceManagerStringLocalizerFactory(
    IOptions<LocalizationOptions> localizationOptions,
    ILoggerFactory loggerFactory) : IStringLocalizerFactory
{
    private readonly ResourceManagerStringLocalizerFactory _factory = new(localizationOptions, loggerFactory);
    private readonly ILogger<GenericResourceManagerStringLocalizerFactory> _logger = loggerFactory.CreateLogger<GenericResourceManagerStringLocalizerFactory>();

    public IStringLocalizer Create(Type resourceSource)
    {
        _logger.LogDebug("Creating a localizer for a type '{localization.type}'.", resourceSource);

        TypeInfo typeInfo = resourceSource.GetTypeInfo();
        string assemblyName = typeInfo.Assembly.GetName().Name;

        var attribute = resourceSource.GetCustomAttribute<LocalizationAliasAttribute>();

        string typeName;
        if (attribute != null) {
            typeName = attribute.Alias;
            _logger.LogTrace("An alias '{localization.alias}' was found for the type '{localization.type}'.", attribute.Alias, resourceSource);
        }
        else
        {
            typeName = resourceSource.Name;
            if (typeInfo.IsGenericType) 
                typeName = typeName[..resourceSource.Name.IndexOf('`')];

        }
        string baseName = $"{resourceSource.Namespace}.{typeName}"[assemblyName.Length..].Trim('.');

        var localizer = _factory.Create(baseName, assemblyName);

        _logger.LogTrace("localizer for type '{localization.type}' created.", resourceSource);

        return localizer;
    }

    public IStringLocalizer Create(string baseName, string location)
    {
        _logger.LogTrace("Creating a localizer for baseName '{localization.baseName}' and '{localization.location}'.", baseName, location);

        var localizer = _factory.Create(baseName, location);

        _logger.LogTrace("localizer for baseName '{localization.baseName}' and '{localization.location}' created.", baseName, location);

        return localizer;
    }
}
