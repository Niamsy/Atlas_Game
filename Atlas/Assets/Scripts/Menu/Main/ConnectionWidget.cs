using System;
using FileSystem;
using Game.SavingSystem;
using Menu.LevelSelector;
using SceneManagement;
using UnityEngine;
using UnityEngine.UI;

namespace Menu.Main
{
    public class ConnectionWidget : RequestManagerWidget
    {
        [SerializeField] private InputField _username = null;
        [SerializeField] private InputField _password = null;
        
        [SerializeField] private Button     _connectionButton = null;
        [SerializeField] private Button     _registerButton = null;
        [SerializeField] private Button     _passwordLost = null;
        [SerializeField] private Toggle     _saveUsername = null;

        [SerializeField] private MenuWidget _nextWidget = null;

        [SerializeField] private LevelSaver _levelMenu = null;
        private AtlasFileSystem _fs;
        private bool _connected = false;

        protected override void Awake()
        {
            if (SceneLoader.FromGame)
            {
                SceneLoader.FromGame = false;
                if (SaveManager.Instance.SelectedProfil != null)
                {
                    SaveManager.InstantiateProfilToUse(SaveManager.Instance.AccountData,
                        SaveManager.Instance.SelectedProfil, SaveManager.Instance.SelectedProfil.Name);
                    SaveManager.Instance.SelectProfilToUseForSave(SaveManager.Instance.SelectedProfil);
                    SaveManager.Instance.ReloadAccountData();
                }

                if (_levelMenu)
                    _levelMenu.UpdateSelectedLevelWidget();
                gameObject.SetActive(false);
                return;
            }
            
            base.Awake();
            _fs = AtlasFileSystem.Instance;

            try
            {
                _username.text = _fs.getConfigValue(Key.Username, Section.Default);
                _password.text = _fs.getConfigValue(Key.Password, Section.Default);
                _saveUsername.isOn = _fs.GetConfigBoolValue(Key.SaveUsername, Section.Default);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        
        #region Initialisation/Destruction
        protected override void InitialiseWidget()
        {
            ActualRequestManager.OnConnectionFinished += ConnectionFinished;
            _connected = true;

            _connectionButton.onClick.AddListener(Connect);
            _username.onValueChanged.AddListener(UpdateButtonState_StringListener);
            _password.onValueChanged.AddListener(UpdateButtonState_StringListener);
            
            UpdateButtonState();
        }

        private void OnDestroy()
        {
            if (_connected)
                ActualRequestManager.OnConnectionFinished -= ConnectionFinished;
        }
        #endregion

        #region UpdateButtonState
        /// <summary>
        /// Enable or not the button in case the field are filled correctly
        /// </summary>
        protected override void UpdateButtonState()
        {
            base.UpdateButtonState();
            _connectionButton.interactable &= (_username.text.Length > 0 && _password.text.Length > 0f && ActualRequestManager.CanReceiveANewRequest);
            _registerButton.interactable &= ActualRequestManager.CanReceiveANewRequest;
            _passwordLost.interactable &= ActualRequestManager.CanReceiveANewRequest;
        }
        #endregion
        
        #region Request
        /// <summary>
        /// Call the request manager and the good function
        /// </summary>
        public void Connect()
        {
            ActualRequestManager.Connect(_username.text, _password.text);
            UpdateButtonState();
        }
        private void ConnectionFinished(bool success, string message)
        {
            UpdateButtonState();

            if (success)
            {
                if (_saveUsername.isOn)
                    SaveValue(_username.text, _password.text);
                else
                    SaveValue("", "");
                
                Close();
                _nextWidget.Open();
            }
            else
                ErrorText.text = message;
        }

        public void SaveValue(string username, string password)
        {
            _fs.setConfigFileValue(Key.SaveUsername, Section.Default, (_saveUsername.isOn).ToString());

            _fs.setConfigFileValue(Key.Password, Section.Default, password);
            _fs.setConfigFileValue(Key.Username, Section.Default, username);
        }
        #endregion
    }
}
