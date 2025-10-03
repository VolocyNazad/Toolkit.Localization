using System.Reflection;
using Toolkit.Localizations.Abstractions.Services;
using Toolkit.Localizations.Infrastructure.Attributes;

namespace Toolkit.Localizations.Infrastructure.Services;

internal sealed class InheritedLocalizationTypeDefiner : ILocalizationTypeDefiner
{
    public IEnumerable<Type> Define(Type type)
    {
        var inheritorAttributes = type.GetCustomAttributes<UseInheritorStringLocalizationAttribute>();
        if (inheritorAttributes.Any())
        {
            return [type.BaseType];
        }
        return [];
    }
} 
