using Microsoft.Extensions.Localization;

namespace Toolkit.Localizations.Infrastructure.Attributes
{
    /// <summary>
    /// Используется, чтобы указать локализатору <see cref="IStringLocalizer{T}"/> дополнительный тип для поиска ресурса.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public sealed class UsePseudoInheritorStringLocalizationAttribute(Type inheritFrom) : Attribute
    {
        public Type InheritFrom { get; } = inheritFrom ?? throw new ArgumentNullException(nameof(inheritFrom));
        public int Priority { get; set; }
    }
}
