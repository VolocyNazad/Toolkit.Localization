using System;
using System.Collections.Generic;

namespace Toolkit.Localizations.Abstractions.Services
{
    public interface ILocalizationTypeDefiner
    {
        IEnumerable<Type> Define(Type type);
    }
}
