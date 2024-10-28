using Microsoft.Extensions.Localization;
using System;

namespace Toolkit.Localizations.Infrastructure.Attributes
{
    /// <summary>
    /// Используется, чтобы указать локализатору <see cref="IStringLocalizer{T}"/> дополнительный тип для поиска ресурса.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public sealed class UsePseudoInheritorStringLocalizationAttribute : Attribute
    {
        public UsePseudoInheritorStringLocalizationAttribute(Type inheritFrom)
        {
            InheritFrom = inheritFrom ?? throw new ArgumentNullException(nameof(inheritFrom));
        }

        public Type InheritFrom { get; }
        public int Priority { get; set; }
    }
}
