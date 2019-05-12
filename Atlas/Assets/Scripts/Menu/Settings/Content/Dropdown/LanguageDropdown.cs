using System;
using System.Collections.Generic;
using Localization;
using UnityEngine;

namespace Menu.Settings.Content.Dropdown
{
    public class LanguageDropdown : SettingsEntryDropdown
    {
        [Serializable]
        public class Language
        {
            public SystemLanguage SystemLanguage;
            public string Text;
        }
        [SerializeField] private Language[]  _languages;
        private SystemLanguage    _actualLanguage;
        protected override List<string> GetOptions()
        {
            List<string> options = new List<string>();
            foreach (var language in _languages)
                options.Add(language.Text);
            return (options);
        }

        public override void LoadData()
        {
            _actualLanguage = LocalizationManager.Instance.CurrentLanguage;
            for (int x = 0; x < _languages.Length; x++)
            {
                if (_actualLanguage == _languages[x].SystemLanguage)
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
            LocalizationManager.Instance.CurrentLanguage = _languages[Dropdown.value].SystemLanguage;
        }
    }
}
