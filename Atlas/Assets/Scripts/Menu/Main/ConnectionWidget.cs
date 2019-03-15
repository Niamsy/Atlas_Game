using UnityEngine;
using UnityEngine.UI;

namespace Menu.Main
{
    public class ConnectionWidget : MainMenuWidget
    {
        public string NextScene = "Atlas";
        [SerializeField] private InputField _username;
        [SerializeField] private InputField _password;
        
        [SerializeField] private Button     _connectionButton;
        [SerializeField] private Button     _registerButton;
        [SerializeField] private Button     _passwordLost;

        #region Initialisation/Destruction
        protected override void InitialiseWidget()
        {
            ActualRequestManager.OnConnectionFinished += ConnectionFinished;

            _connectionButton.onClick.AddListener(Connect);
            _username.onValueChanged.AddListener(UpdateButtonState_StringListener);
            _password.onValueChanged.AddListener(UpdateButtonState_StringListener);
            
            UpdateButtonState();
        }

        private void OnDestroy()
        {
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
                Debug.Log("CONNECTED");
                SceneLoader.Instance.LoadScene(NextScene, "MainMenu");
            }
            else
                ErrorText.text = message;
        }
        #endregion
    }
}
