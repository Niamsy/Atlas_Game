using UnityEngine;

namespace Menu.Settings.Content.Slider
{
    public abstract class SettingEntrySlider : SettingEntry
    {
        [SerializeField] protected UnityEngine.UI.Slider Slider;

        public override string Value() { return (Slider.value.ToString()); }

        protected float                                  ActualValue;

        protected virtual float        MinValue { get { return (0); } }
        protected virtual float        MaxValue { get { return (1); } }

        public override void Initialization()
        {
            Slider.minValue = MinValue;
            Slider.maxValue = MaxValue;
            Slider.onValueChanged.AddListener(val => OnValueDidChanged());
        }

        public override void SaveData()
        {
            _fs.setConfigFileValue("Nothing", "Nothing", (ActualValue).ToString());
        }
        public override bool DidValueChanged() { return (false); }
    }
}
