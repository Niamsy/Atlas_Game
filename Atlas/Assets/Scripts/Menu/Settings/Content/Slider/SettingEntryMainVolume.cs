using AtlasAudio;

namespace Menu.Settings.Content.Slider
{
    public class SettingEntryMainVolume : SettingEntrySlider
    {
        private AudioPlayer _audio;

        public override void LoadData()
        {
            _audio = AudioPlayer.Instance;
            Slider.value = _audio.MasterVolume;
        }

        protected override void OnValueDidChanged()
        {
            AudioPlayer.Instance.SetMasterVolume(Slider.value);
        }

    }
}
