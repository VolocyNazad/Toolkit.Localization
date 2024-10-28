using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Toolkit.Localizations.Abstractions.Services;
using Toolkit.Localizations.Infrastructure.Attributes;

namespace Toolkit.Localizations.Infrastructure.Services
{
    internal class PseudoLocalizationTypeDefiner : ILocalizationTypeDefiner
    {
        public IEnumerable<Type> Define(Type type)
        {
            var pseudoInheritorAttributes = type.GetCustomAttributes<UsePseudoInheritorStringLocalizationAttribute>();
            if (pseudoInheritorAttributes.Any())
            {
                var result = new List<Type>();

                foreach (var attribute in pseudoInheritorAttributes.OrderBy(a => a.Priority).ToList())
                {
                    result.Add(attribute.InheritFrom);
                }
                return result;
            }
            return new Type[0];
        }
    } 
}
