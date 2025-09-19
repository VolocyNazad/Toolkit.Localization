using Microsoft.Extensions.Localization;

namespace Toolkit.Localizations.Infrastructure.Attributes
{
    /// <summary>
    /// Используется, чтобы указать локализатору <see cref="IStringLocalizer{T}"/> псевдоним.
    /// Поиск выполняется во всей иерархии обобщений.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public sealed class LocalizationAliasAttribute(string alias) : Attribute
    {
        public string Alias { get; set; } = alias;
    }
}