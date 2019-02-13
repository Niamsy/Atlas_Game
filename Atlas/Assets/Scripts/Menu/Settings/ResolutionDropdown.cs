using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Menu.Settings
{
    public class ResolutionDropdown : SettingsEntryDropdown
    {
        private Resolution[]               _resolutions;
        private Resolution                 _actualResolution;
        
        protected override void Initialization()
        {
            _resolutions = Screen.resolutions;
            base.Initialization();
        }

        protected override List<string> GetOptions()
        {
            List<string> options = new List<string>();
            foreach (Resolution resolution in _resolutions)
                options.Add(resolution.width + "x" + resolution.height);
            return (options);
        }

        public override void LoadData()
        {
            _actualResolution = Screen.currentResolution;
            for (int x = 0; x < _resolutions.Length; x++)
            {
                if (ResolutionAreSame(_actualResolution, _resolutions[x]))
                {
                    CurrentIndex = x;
                    Dropdown.value = CurrentIndex;
                }
            }
        }

        private bool ResolutionAreSame(Resolution res1, Resolution res2)
        {
            return (res1.width == res2.width && res1.height == res2.height);
        }
        
        public override bool DidValueChanged()
        {
            return (!ResolutionAreSame(ActualResolution(), Screen.currentResolution));
        }

        public Resolution ActualResolution() { return (_resolutions[Dropdown.value]); }
    }
}
