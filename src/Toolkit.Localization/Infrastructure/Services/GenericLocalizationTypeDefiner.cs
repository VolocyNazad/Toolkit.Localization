using System.Reflection;
using Toolkit.Localizations.Abstractions.Services;
using Toolkit.Localizations.Infrastructure.Attributes;

namespace Toolkit.Localizations.Infrastructure.Services;

internal sealed class GenericLocalizationTypeDefiner : ILocalizationTypeDefiner
{
    public IEnumerable<Type> Define(Type type)
    {
        var genericAttributes = type.GetCustomAttributes<UseGenericStringLocalizationsAttribute>();
        if (genericAttributes.Any())
        {
            var result = new List<Type>();

            IEnumerable<Type> types = type.GetGenericArguments();
            int deep = genericAttributes.First().Deep;
            int counter = 0;
            while (deep >= counter)
            {
                foreach (Type genericType in types)
                {
                    result.Add(genericType);
                }

                types = types.SelectMany(i => i.GetGenericArguments()).ToList();
                counter++;
            }
            return result;
        }

        return [];
    }
}
