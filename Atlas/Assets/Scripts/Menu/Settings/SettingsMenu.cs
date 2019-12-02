using FileSystem;
using Localization;
using Menu.Settings.Content;
using Menu.Settings.Content.Dropdown;
using UnityEngine;
using UnityEngine.UI;

namespace Menu.Settings
{
    public class SettingsMenu : MenuWidget
    {
        #region Variables
        [Header("Specials settings")]
        [SerializeField] private ResolutionDropdown _resolution = null;
        [SerializeField] private FullscreenDropdown _fullscreen = null;
        
        [Header("Exits buttons")]
        [SerializeField] private Button             _saveClose = null;
        [SerializeField] private Button             _close = null;
        
        private SettingEntry[] _settings = null;
        private AtlasFileSystem _fs;
        #endregion

        protected override void Awake()
        {
#if UNITY_EDITOR
            Tools.DevTools.BeforeSplashScreen_RuntimeMethod();   
#endif
            
            base.Awake();
            _fs = AtlasFileSystem.Instance;

            LocalizationManager.Instance.CurrentLanguage = (SystemLanguage)(_fs.GetConfigIntValue(Key.Lang));;

            LocalizationManager.Instance.LocaleChanged += ReloadDataForNewLanguage;
        }

        private void OnDestroy()
        {
            LocalizationManager.Instance.LocaleChanged -= ReloadDataForNewLanguage;
        }

        private void ReloadDataForNewLanguage(object sender, LocaleChangedEventArgs e)
        {
            foreach (var setting in _settings)
                setting.ReloadData();
        }

        protected override void InitialiseWidget()
        {
            _settings = GetComponentsInChildren<SettingEntry>();

            foreach (var setting in _settings)
            {
                setting.Initialization();
                setting.OnValueChanged += SubSettingChanged;
            }

            SubSettingChanged();
            
            _saveClose.onClick.AddListener(OnSaveThenClose);
            _close.onClick.AddListener(Close);
        }

        #region Update sub settings
        public override void Show(bool display, bool force = false)
        {
            if (display)
                LoadSubSettingData();

            base.Show(display, force);
        }

        private void LoadSubSettingData()
        {
            foreach (var setting in _settings)
                setting.LoadData();
        }
        
        private void SubSettingChanged()
        {
            _saveClose.enabled = DidASettingChanged();
        }
        
        private bool DidASettingChanged()
        {
            foreach (var setting in _settings)
                if (setting.DidValueChanged())
                    return (true);
            return (false);
        }
        #endregion

        private void OnSaveThenClose()
        {
            if (_resolution.DidValueChanged())
                Screen.SetResolution(_resolution.ActualResolution().width, _resolution.ActualResolution().height, _fullscreen.ActualValue());
            else if (_fullscreen.DidValueChanged())
                Screen.fullScreenMode = _fullscreen.ActualValue();
            
            foreach (SettingEntry entry in _settings)
                entry.SaveData();
            
            AtlasFileSystem.Instance.saveConfig();
            
            Close();
        }
    }
}