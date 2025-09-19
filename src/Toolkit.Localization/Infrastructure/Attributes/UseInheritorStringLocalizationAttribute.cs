using Microsoft.Extensions.Localization;

namespace Toolkit.Localizations.Infrastructure.Attributes
{
    /// <summary>
    /// Используется, чтобы указать локализатору <see cref="IStringLocalizer{T}"/> необходимость поиска значения в локализации наследников.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public sealed class UseInheritorStringLocalizationAttribute : Attribute
    {
    }
}
