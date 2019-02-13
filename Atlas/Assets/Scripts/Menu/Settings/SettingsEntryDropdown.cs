using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Menu.Settings
{
    public abstract class SettingsEntryDropdown : SettingEntry
    {
        [SerializeField] protected Dropdown  Dropdown;
        protected int                        CurrentIndex;

        protected override void Initialization()
        {
            Dropdown.ClearOptions();
            Dropdown.AddOptions(GetOptions());
            Dropdown.onValueChanged.AddListener(useless => OnValueDidChanged());
        }

        protected abstract List<string> GetOptions();
    }
}
