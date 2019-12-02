using System.Collections.Generic;
using UnityEngine;

namespace Menu.Settings.Content.Dropdown
{
    public abstract class SettingsEntryDropdown : SettingEntry
    {
        [SerializeField] protected UnityEngine.UI.Dropdown  Dropdown;
        protected int                                       CurrentIndex;
        
        public override string Value() { return (Dropdown.value.ToString()); }

        public override void Initialization()
        {
            Dropdown.ClearOptions();
            Dropdown.AddOptions(GetOptions());
            Dropdown.onValueChanged.AddListener(useless => OnValueDidChanged());
        }

        public override void ReloadData()
        {
            var options = GetOptions();
            var actualOptions = Dropdown.options;
           
            for (int x = 0; x < options.Count; x++)
                actualOptions[x] = new UnityEngine.UI.Dropdown.OptionData(options[x]);
            Dropdown.options = actualOptions;
        }
        
        protected abstract List<string> GetOptions();
    }
}
