using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;

namespace Toolkit.Localizations.Infrastructure.Utils
{
    public static class LocalizationUtils
    {
        /// <summary> Выполняет проверку подключения ресурса локализации</summary>
        /// <param name="localizer"> Проверяемая локализация </param>
        /// <returns> В случае, если локализация подключена, то возвращает true, в противном случае false</returns>
        public static bool Check(IStringLocalizer localizer)
        {
            IEnumerable<LocalizedString> localizedStrings = localizer.GetAllStrings();
            foreach (LocalizedString localizedString in localizedStrings)
            {
                if (localizedString.Name == localizedString.Value) return false;
            }

            return true;
        }

        /// <summary> Выполняет проверку подключения ресурса локализации</summary>
        /// <param name="localizer"> Проверяемая локализация </param>
        /// <returns> В случае, если локализация подключена, то ничего не возвращает, в противном случае выдает исключение </returns>
        public static void ThrowCheck(IStringLocalizer localizer)
        {
            IEnumerable<LocalizedString> localizedStrings = localizer.GetAllStrings();
            foreach (LocalizedString localizedString in localizedStrings)
            {
                if (localizedString.Name == localizedString.Value) 
                    throw new Exception("Not all localization data was connected");
            }
        }
    }
}
