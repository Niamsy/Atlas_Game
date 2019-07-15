// Copyright (c) H. Ibrahim Penekli. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using UnityEditor;
using UnityEngine;

namespace Localization.Editor
{
	/// <summary>
	/// Unity Editor menu for changing localization under "Tools/Localization".
	/// </summary>
    public static class LocalizationMenu
    {
        private const string ParentMenu = LocalizationEditorHelper.LocalizationMenu + "Set Locale/";

        [MenuItem(ParentMenu + "Afrikaans")]
        static void ChangeToAfrikaans()
        {
            global::Localization.LocalizationManager.Instance.CurrentLanguage = SystemLanguage.Afrikaans;
        }

        [MenuItem(ParentMenu + "Arabic")]
        static void ChangeToArabic()
        {
            global::Localization.LocalizationManager.Instance.CurrentLanguage = SystemLanguage.Arabic;
        }

        [MenuItem(ParentMenu + "Basque")]
        static void ChangeToBasque()
        {
            global::Localization.LocalizationManager.Instance.CurrentLanguage = SystemLanguage.Basque;
        }

        [MenuItem(ParentMenu + "Belarusian")]
        static void ChangeToBelarusian()
        {
            global::Localization.LocalizationManager.Instance.CurrentLanguage = SystemLanguage.Belarusian;
        }

        [MenuItem(ParentMenu + "Bulgarian")]
        static void ChangeToBulgarian()
        {
            global::Localization.LocalizationManager.Instance.CurrentLanguage = SystemLanguage.Bulgarian;
        }

        [MenuItem(ParentMenu + "Catalan")]
        static void ChangeToCatalan()
        {
            global::Localization.LocalizationManager.Instance.CurrentLanguage = SystemLanguage.Catalan;
        }

        [MenuItem(ParentMenu + "Chinese")]
        static void ChangeToChinese()
        {
            global::Localization.LocalizationManager.Instance.CurrentLanguage = SystemLanguage.Chinese;
        }

        [MenuItem(ParentMenu + "Czech")]
        static void ChangeToCzech()
        {
            global::Localization.LocalizationManager.Instance.CurrentLanguage = SystemLanguage.Czech;
        }

        [MenuItem(ParentMenu + "Danish")]
        static void ChangeToDanish()
        {
            global::Localization.LocalizationManager.Instance.CurrentLanguage = SystemLanguage.Danish;
        }

        [MenuItem(ParentMenu + "Dutch")]
        static void ChangeToDutch()
        {
            global::Localization.LocalizationManager.Instance.CurrentLanguage = SystemLanguage.Dutch;
        }

        [MenuItem(ParentMenu + "English")]
        static void ChangeToEnglish()
        {
            global::Localization.LocalizationManager.Instance.CurrentLanguage = SystemLanguage.English;
        }

        [MenuItem(ParentMenu + "Estonian")]
        static void ChangeToEstonian()
        {
            global::Localization.LocalizationManager.Instance.CurrentLanguage = SystemLanguage.Estonian;
        }

        [MenuItem(ParentMenu + "Faroese")]
        static void ChangeToFaroese()
        {
            global::Localization.LocalizationManager.Instance.CurrentLanguage = SystemLanguage.Faroese;
        }

        [MenuItem(ParentMenu + "Finnish")]
        static void ChangeToFinnish()
        {
            global::Localization.LocalizationManager.Instance.CurrentLanguage = SystemLanguage.Finnish;
        }

        [MenuItem(ParentMenu + "French")]
        static void ChangeToFrench()
        {
            global::Localization.LocalizationManager.Instance.CurrentLanguage = SystemLanguage.French;
        }

        [MenuItem(ParentMenu + "German")]
        static void ChangeToGerman()
        {
            global::Localization.LocalizationManager.Instance.CurrentLanguage = SystemLanguage.German;
        }

        [MenuItem(ParentMenu + "Greek")]
        static void ChangeToGreek()
        {
            global::Localization.LocalizationManager.Instance.CurrentLanguage = SystemLanguage.Greek;
        }

        [MenuItem(ParentMenu + "Hebrew")]
        static void ChangeToHebrew()
        {
            global::Localization.LocalizationManager.Instance.CurrentLanguage = SystemLanguage.Hebrew;
        }

        [MenuItem(ParentMenu + "Hungarian")]
        static void ChangeToHungarian()
        {
            global::Localization.LocalizationManager.Instance.CurrentLanguage = SystemLanguage.Hungarian;
        }

        [MenuItem(ParentMenu + "Hugarian")]
        static void ChangeToHugarian()
        {
            global::Localization.LocalizationManager.Instance.CurrentLanguage = SystemLanguage.Hungarian;
        }

        [MenuItem(ParentMenu + "Icelandic")]
        static void ChangeToIcelandic()
        {
            global::Localization.LocalizationManager.Instance.CurrentLanguage = SystemLanguage.Icelandic;
        }

        [MenuItem(ParentMenu + "Indonesian")]
        static void ChangeToIndonesian()
        {
            global::Localization.LocalizationManager.Instance.CurrentLanguage = SystemLanguage.Indonesian;
        }

        [MenuItem(ParentMenu + "Italian")]
        static void ChangeToItalian()
        {
            global::Localization.LocalizationManager.Instance.CurrentLanguage = SystemLanguage.Italian;
        }

        [MenuItem(ParentMenu + "Japanese")]
        static void ChangeToJapanese()
        {
            global::Localization.LocalizationManager.Instance.CurrentLanguage = SystemLanguage.Japanese;
        }

        [MenuItem(ParentMenu + "Korean")]
        static void ChangeToKorean()
        {
            global::Localization.LocalizationManager.Instance.CurrentLanguage = SystemLanguage.Korean;
        }

        [MenuItem(ParentMenu + "Latvian")]
        static void ChangeToLatvian()
        {
            global::Localization.LocalizationManager.Instance.CurrentLanguage = SystemLanguage.Latvian;
        }

        [MenuItem(ParentMenu + "Lithuanian")]
        static void ChangeToLithuanian()
        {
            global::Localization.LocalizationManager.Instance.CurrentLanguage = SystemLanguage.Lithuanian;
        }

        [MenuItem(ParentMenu + "Norwegian")]
        static void ChangeToNorwegian()
        {
            global::Localization.LocalizationManager.Instance.CurrentLanguage = SystemLanguage.Norwegian;
        }

        [MenuItem(ParentMenu + "Polish")]
        static void ChangeToPolish()
        {
            global::Localization.LocalizationManager.Instance.CurrentLanguage = SystemLanguage.Polish;
        }

        [MenuItem(ParentMenu + "Portuguese")]
        static void ChangeToPortuguese()
        {
            global::Localization.LocalizationManager.Instance.CurrentLanguage = SystemLanguage.Portuguese;
        }

        [MenuItem(ParentMenu + "Romanian")]
        static void ChangeToRomanian()
        {
            global::Localization.LocalizationManager.Instance.CurrentLanguage = SystemLanguage.Romanian;
        }

        [MenuItem(ParentMenu + "Russian")]
        static void ChangeToRussian()
        {
            global::Localization.LocalizationManager.Instance.CurrentLanguage = SystemLanguage.Russian;
        }

        [MenuItem(ParentMenu + "SerboCroatian")]
        static void ChangeToSerboCroatian()
        {
            global::Localization.LocalizationManager.Instance.CurrentLanguage = SystemLanguage.SerboCroatian;
        }

        [MenuItem(ParentMenu + "Slovak")]
        static void ChangeToSlovak()
        {
            global::Localization.LocalizationManager.Instance.CurrentLanguage = SystemLanguage.Slovak;
        }

        [MenuItem(ParentMenu + "Slovenian")]
        static void ChangeToSlovenian()
        {
            global::Localization.LocalizationManager.Instance.CurrentLanguage = SystemLanguage.Slovenian;
        }

        [MenuItem(ParentMenu + "Spanish")]
        static void ChangeToSpanish()
        {
            global::Localization.LocalizationManager.Instance.CurrentLanguage = SystemLanguage.Spanish;
        }

        [MenuItem(ParentMenu + "Swedish")]
        static void ChangeToSwedish()
        {
            global::Localization.LocalizationManager.Instance.CurrentLanguage = SystemLanguage.Swedish;
        }

        [MenuItem(ParentMenu + "Thai")]
        static void ChangeToThai()
        {
            global::Localization.LocalizationManager.Instance.CurrentLanguage = SystemLanguage.Thai;
        }

        [MenuItem(ParentMenu + "Turkish")]
        static void ChangeToTurkish()
        {
            global::Localization.LocalizationManager.Instance.CurrentLanguage = SystemLanguage.Turkish;
        }

        [MenuItem(ParentMenu + "Ukrainian")]
        static void ChangeToUkrainian()
        {
            global::Localization.LocalizationManager.Instance.CurrentLanguage = SystemLanguage.Ukrainian;
        }

        [MenuItem(ParentMenu + "Vietnamese")]
        static void ChangeToVietnamese()
        {
            global::Localization.LocalizationManager.Instance.CurrentLanguage = SystemLanguage.Vietnamese;
        }

        [MenuItem(ParentMenu + "ChineseSimplified")]
        static void ChangeToChineseSimplified()
        {
            global::Localization.LocalizationManager.Instance.CurrentLanguage = SystemLanguage.ChineseSimplified;
        }

        [MenuItem(ParentMenu + "ChineseTraditional")]
        static void ChangeToChineseTraditional()
        {
            global::Localization.LocalizationManager.Instance.CurrentLanguage = SystemLanguage.ChineseTraditional;
        }

        [MenuItem(ParentMenu + "Unknown")]
        static void ChangeToUnknown()
        {
            global::Localization.LocalizationManager.Instance.CurrentLanguage = SystemLanguage.Unknown;
        }
    }
}
