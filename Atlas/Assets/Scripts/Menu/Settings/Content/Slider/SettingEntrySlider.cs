using UnityEngine;

namespace Menu.Settings.Content.Slider
{
    public abstract class SettingEntrySlider : SettingEntry
    {
        [SerializeField] protected UnityEngine.UI.Slider Slider;
        protected float                                  ActualValue;

        protected virtual float        MinValue { get { return (0); } }
        protected virtual float        MaxValue { get { return (1); } }

        public override void Initialization()
        {
            Slider.minValue = MinValue;
            Slider.maxValue = MaxValue;
            Slider.onValueChanged.AddListener(val => OnValueDidChanged());
        }
        
        public override bool DidValueChanged() { return (false); }
    }
}
