using UnityEngine;
using UnityEngine.UI;

namespace Menu.Main
{
    public class RegisterWidget : RequestManagerWidget
    {
        [SerializeField] private InputField _username = null;
        [SerializeField] private InputField _emailAddress = null;
        [SerializeField] private InputField _password = null;
        [SerializeField] private InputField _passwordConfirmation = null;
        
        [SerializeField] private Button     _registerButton = null;
        [SerializeField] private Button     _returnButton = null;
    
        #region Initialisation/Destruction
        protected override void InitialiseWidget()
        {
            ActualRequestManager.OnRegisterFinished += RegisterFinished;

            _registerButton.onClick.AddListener(Register);
            _username.onValueChanged.AddListener(UpdateButtonState_StringListener);
            _password.onValueChanged.AddListener(UpdateButtonState_StringListener);
            _passwordConfirmation.onValueChanged.AddListener(UpdateButtonState_StringListener);
            
            UpdateButtonState();
        }

        private void OnDestroy()
        {
            ActualRequestManager.OnRegisterFinished -= RegisterFinished;
        }
        #endregion

        #region UpdateButtonState
        /// <summary>
        /// Enable or not the button in case the field are filled correctly
        /// </summary>
        protected override void UpdateButtonState()
        {
            base.UpdateButtonState();
            _registerButton.interactable &= (_username.text.Length > 0 && _emailAddress.text.Length > 0
                                            && _password.text.Length > 0f && _password.text.Equals(_passwordConfirmation.text)
                                            && ActualRequestManager.CanReceiveANewRequest);
            _returnButton.interactable &= ActualRequestManager.CanReceiveANewRequest;
        }
        #endregion
        
        #region Request
        /// <summary>
        /// Call the request manager and the good function
        /// </summary>
        public void Register()
        {
            ActualRequestManager.Register(_username.text, _emailAddress.text, _password.text);
            UpdateButtonState();
        }
        private void RegisterFinished(bool success, string message)
        {
            UpdateButtonState();

            if (success)
            {
                Debug.Log("REGISTERED");
                //
                // ToDo: Go to next Scene
                //
            }
            else
                ErrorText.text = message;
        }
        #endregion
    }
}
