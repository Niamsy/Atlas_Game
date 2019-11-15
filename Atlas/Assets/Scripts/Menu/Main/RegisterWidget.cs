using System.Collections;
using System.Text.RegularExpressions;
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

        [SerializeField] private Text _successText = null;

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
            if (isPasswordConform(_password.text))
            {
                _successText.text = "Your account has been successfully created";
                StartCoroutine(Redirect());
                ActualRequestManager.Register(_username.text, _emailAddress.text, _password.text);
                UpdateButtonState();
            }
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

        private bool isPasswordConform(string password)
        {
            if (Regex.Match(password, "^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)[a-zA-Z\\d]{8,}$").Success)
            {
                ErrorText.text = "";
                return true;
            } else
            {
                ErrorText.text = "Password should contain at least 8 characters, one uppercase letter and one number";
                return false;
            }
        }

        IEnumerator Redirect()
        {
            yield return new WaitForSeconds(4f);
            _returnButton.onClick.Invoke();
        }
    }
}
