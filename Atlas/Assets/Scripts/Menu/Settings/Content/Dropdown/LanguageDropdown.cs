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
            public SystemLanguage SystemLanguage = SystemLanguage.English;
            public string Text = "English";
        }
        
        [SerializeField] private Language[]   _languages = null;
        private SystemLanguage                _actualLanguage = SystemLanguage.English;

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
        

        public override void SaveData()
        {
            _fs.setConfigFileValue(FileSystem.Key.Lang, FileSystem.Section.Default, ((int)_languages[Dropdown.value].SystemLanguage).ToString());
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
