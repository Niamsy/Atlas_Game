using AtlasAudio;

namespace Menu.Settings.Content.Slider
{
    public class SettingEntryMixerVolume : SettingEntrySlider
    {
        public AudioGroup LinkedGroup;
        
        private AudioPlayer _audio;

        public override void LoadData()
        {
            _audio = AudioPlayer.Instance;
            Slider.value = _audio.GetGroupVolume(LinkedGroup);
        }

        protected override void OnValueDidChanged()
        {
            _audio.SetGroupVolume(LinkedGroup, Slider.value);
        }

    }
}
