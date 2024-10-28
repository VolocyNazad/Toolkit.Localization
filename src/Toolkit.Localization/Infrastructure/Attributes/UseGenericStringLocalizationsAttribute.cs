using System;
using Microsoft.Extensions.Localization;

namespace Toolkit.Localizations.Infrastructure.Attributes
{
    /// <summary>
    /// Используется, чтобы указать локализатору <see cref="IStringLocalizer{T}"/> необходимость поиска значения в локализации его обобщений.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class UseGenericStringLocalizationsAttribute : Attribute
    {
        public int Deep { get; set; } = 0;

        #region Constructors

        public UseGenericStringLocalizationsAttribute(int deep)
        {
            Deep = deep;
        }

        public UseGenericStringLocalizationsAttribute()
        {

        }

        #endregion
    }
}