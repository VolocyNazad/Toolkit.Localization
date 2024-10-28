using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Toolkit.Localizations.Abstractions.Services;
using Toolkit.Localizations.Infrastructure.Attributes;

namespace Toolkit.Localizations.Infrastructure.Services
{
    internal class InheritedLocalizationTypeDefiner : ILocalizationTypeDefiner
    {
        public IEnumerable<Type> Define(Type type)
        {
            var inheritorAttributes = type.GetCustomAttributes<UseInheritorStringLocalizationAttribute>();
            if (inheritorAttributes.Any())
            {
                return new[] { type.BaseType };
            }
            return new Type[0];
        }
    } 
}
