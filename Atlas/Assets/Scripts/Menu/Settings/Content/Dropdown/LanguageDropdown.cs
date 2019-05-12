using System.Collections.Generic;
using Localization;
using UnityEngine;

namespace Menu.Settings.Content.Dropdown
{
    public class LanguageDropdown : SettingsEntryDropdown
    {
        private SystemLanguage[]  _languages;
        private SystemLanguage    _actualLanguage;

        public override void Initialization()
        {
            _languages = LocalizationSettings.Instance.AvailableLanguages.ToArray();
            base.Initialization();
        }

        protected override List<string> GetOptions()
        {
            List<string> options = new List<string>();
            foreach (SystemLanguage language in _languages)
                options.Add(language.ToString());
            return (options);
        }

        public override void LoadData()
        {
            _actualLanguage = LocalizationManager.Instance.CurrentLanguage;
            for (int x = 0; x < _languages.Length; x++)
            {
                if (_actualLanguage == _languages[x])
                {
                    CurrentIndex = x;
                    Dropdown.value = CurrentIndex;
                }
            }
        }
        
        public override bool DidValueChanged()
        {
            return (_actualLanguage == LocalizationManager.Instance.CurrentLanguage);
        }

        protected override void OnValueDidChanged()
        {
            LocalizationManager.Instance.CurrentLanguage = _languages[Dropdown.value];
        }
    }
}
