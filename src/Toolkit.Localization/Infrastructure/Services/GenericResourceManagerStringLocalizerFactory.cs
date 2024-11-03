using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Reflection;
using Toolkit.Localizations.Infrastructure.Attributes;

namespace Toolkit.Localizations.Infrastructure.Services
{
    /// <summary>
    /// Фабрика локализации, которая в отличии от <see cref="ResourceManagerStringLocalizerFactory"/> способна выполнять поиск ресурса локализации по обобщенным классам
    /// </summary>
    /// <remarks>
    /// Базовая реализация <see cref="IStringLocalizerFactory"/> из коробки <see cref="GenericResourceManagerStringLocalizerFactory"/>
    /// Имеет существенный недостаток. Она не может найти ресурс, если <see cref="IStringLocalizer{T}"/> в качестве обобщенного типа имеет обобщенный тип. 
    /// Данная реализация устраняет данный недостаток
    /// </remarks>
    internal sealed class GenericResourceManagerStringLocalizerFactory : IStringLocalizerFactory
    {
        private readonly ResourceManagerStringLocalizerFactory _factory;

        //public IStringLocalizer Create(Type resourceSource)
        //{
        //    TypeInfo typeInfo = resourceSource.GetTypeInfo();

        //    if (!typeInfo.IsGenericType) return _factory.Create(resourceSource);

        //    string assemblyName = resourceSource.GetTypeInfo().Assembly.GetName().Name;
        //    string typeName = resourceSource.Name.Remove(resourceSource.Name.IndexOf('`'));

        //    string baseName = ($"{resourceSource.Namespace}.{typeName}")
        //        .Substring(assemblyName.Length).Trim('.');

        //    return Create(baseName, assemblyName);
        //}

        public IStringLocalizer Create(Type resourceSource)
        {
            TypeInfo typeInfo = resourceSource.GetTypeInfo();
            string assemblyName = typeInfo.Assembly.GetName().Name;

            var attribute = resourceSource.GetCustomAttribute<LocalizationAliasAttribute>();

            string typeName;
            if (attribute != null) typeName = attribute.Alias;
            else
            {
                typeName = resourceSource.Name;
                if (typeInfo.IsGenericType) 
                    typeName = typeName.Remove(resourceSource.Name.IndexOf('`'));

            }
            string baseName = ($"{resourceSource.Namespace}.{typeName}")
                .Substring(assemblyName.Length).Trim('.');

            return Create(baseName, assemblyName);
        }

        public IStringLocalizer Create(string baseName, string location) => _factory.Create(baseName, location);

        #region Constructors

        public GenericResourceManagerStringLocalizerFactory(
            IOptions<LocalizationOptions> localizationOptions, 
            ILoggerFactory loggerFactory)
        {
            _factory = new ResourceManagerStringLocalizerFactory(localizationOptions, loggerFactory);
        } 

        #endregion
    }
}
