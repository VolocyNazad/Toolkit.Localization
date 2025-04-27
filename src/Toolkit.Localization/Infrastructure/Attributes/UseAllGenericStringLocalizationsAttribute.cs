using System;
using Microsoft.Extensions.Localization;

namespace Toolkit.Localizations.Infrastructure.Attributes
{
    /// <summary>
    /// Используется, чтобы указать локализатору <see cref="IStringLocalizer{T}"/> необходимость поиска значения в локализации его обобщений.
    /// Поиск выполняется во всей иерархии обобщений.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class UseAllGenericStringLocalizationsAttribute : Attribute
    {
        public UseAllGenericStringLocalizationsAttribute()
        {

        }
    }
}