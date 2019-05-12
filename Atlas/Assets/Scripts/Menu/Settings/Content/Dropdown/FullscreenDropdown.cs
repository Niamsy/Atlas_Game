using System;
using System.Collections.Generic;
using Localization;
using UnityEngine;

namespace Menu.Settings.Content.Dropdown
{
    public class FullscreenDropdown : SettingsEntryDropdown
    {
        [Serializable]
        public struct ScreenMode
        {
            public FullScreenMode Mode;
            public LocalizedText  Text;
        }
        
        public ScreenMode[] Modes;
        
        private FullScreenMode _currentMode;
        
        protected override List<string> GetOptions()
        {
            List<string> options = new List<string>();
            foreach (ScreenMode mode in Modes)
                options.Add(mode.Text);
            return (options);
        }

        public override void LoadData()
        {
            _currentMode = Screen.fullScreenMode;
            
            for (int x = 0; x < Modes.Length; x++)
            {
                if (Modes[x].Mode == _currentMode)
                {
                    CurrentIndex = x;
                    Dropdown.value = CurrentIndex;
                }
            }
        }

        public FullScreenMode ActualValue() { return Modes[Dropdown.value].Mode;}
        public override bool DidValueChanged() { return (_currentMode != ActualValue()); }
    }
}
