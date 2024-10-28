using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Toolkit.Localizations.Abstractions.Services;
using Toolkit.Localizations.Infrastructure.Attributes;

namespace Toolkit.Localizations.Infrastructure.Services
{
    internal class AllGenericLocalizationTypeDefiner : ILocalizationTypeDefiner
    {
        public IEnumerable<Type> Define(Type type)
        {
            var genericAttributes = type.GetCustomAttributes<UseAllGenericStringLocalizationsAttribute>();
            if (genericAttributes.Any())
            {
                var result = new List<Type>();

                IEnumerable<Type> types = type.GetGenericArguments();
                while (types.Any())
                {
                    foreach (Type genericType in types)
                    {
                        result.Add(genericType);
                    }

                    types = types.SelectMany(i => i.GetGenericArguments()).ToList();
                }
                return result;
            }

            return new Type[0];
        }
    }
}
