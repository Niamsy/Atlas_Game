using System.Collections.Generic;
using UnityEngine;

namespace Menu.Settings.Content.Dropdown
{
    public abstract class SettingsEntryDropdown : SettingEntry
    {
        [SerializeField] protected UnityEngine.UI.Dropdown  Dropdown;
        protected int                                       CurrentIndex;

        public override void Initialization()
        {
            Dropdown.ClearOptions();
            Dropdown.AddOptions(GetOptions());
            Dropdown.onValueChanged.AddListener(useless => OnValueDidChanged());
        }

        protected abstract List<string> GetOptions();
    }
}
