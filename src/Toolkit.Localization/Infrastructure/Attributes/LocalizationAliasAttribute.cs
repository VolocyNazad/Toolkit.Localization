using System;
using Microsoft.Extensions.Localization;

namespace Toolkit.Localizations.Infrastructure.Attributes
{
    /// <summary>
    /// Используется, чтобы указать локализатору <see cref="IStringLocalizer{T}"/> псевдоним.
    /// Поиск выполняется во всей иерархии обобщений.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class LocalizationAliasAttribute : Attribute
    {
        public string Alias { get; set; }

        #region Constructors

        public LocalizationAliasAttribute(string alias)
        {
            Alias = alias;
        }

        #endregion
    }
}