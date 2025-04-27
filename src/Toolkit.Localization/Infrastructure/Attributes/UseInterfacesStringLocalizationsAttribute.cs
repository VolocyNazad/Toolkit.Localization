using System;
using Microsoft.Extensions.Localization;

namespace Toolkit.Localizations.Infrastructure.Attributes
{
    /// <summary>
    /// Используется, чтобы указать локализатору <see cref="IStringLocalizer{T}"/> необходимость поиска значения в локализацияхб реализованных им интерфейсов.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class UseInterfacesStringLocalizationsAttribute : Attribute
    {
        public UseInterfacesStringLocalizationsAttribute()
        {

        }
    }
}