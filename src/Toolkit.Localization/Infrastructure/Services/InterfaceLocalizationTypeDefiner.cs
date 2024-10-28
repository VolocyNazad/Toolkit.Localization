using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Toolkit.Localizations.Abstractions.Services;
using Toolkit.Localizations.Infrastructure.Attributes;

namespace Toolkit.Localizations.Infrastructure.Services
{
    internal class InterfaceLocalizationTypeDefiner : ILocalizationTypeDefiner
    {
        public IEnumerable<Type> Define(Type type)
        {
            var interfaceAttributes = type.GetCustomAttributes<UseInterfacesStringLocalizationsAttribute>();
            if (interfaceAttributes.Any())
            {
                var result = new List<Type>();

                IEnumerable<Type> types = type.GetInterfaces();
                foreach (Type interfaceType in result)
                {
                    result.Add(interfaceType);
                }
                return result;
            }
            return new Type[0];
        }
    }
}
