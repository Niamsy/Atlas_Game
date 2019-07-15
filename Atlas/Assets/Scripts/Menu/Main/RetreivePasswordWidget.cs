using UnityEngine;
using UnityEngine.UI;

namespace Menu.Main
{
    public class RetreivePasswordWidget : RequestManagerWidget
    {
        [SerializeField] private InputField _email = null;

        [SerializeField] private Button     _acceptButton = null;
        [SerializeField] private Button     _returnButton = null;
        
        [SerializeField] private RetreivePasswordSucessWidget     _retreivePasswordSucess = null;
    
        #region Initialisation/Destruction
        protected override void InitialiseWidget()
        {
            ActualRequestManager.OnResetRequestFinished += RegisterFinished;

            _acceptButton.onClick.AddListener(Register);
            _email.onValueChanged.AddListener(UpdateButtonState_StringListener);
            
            UpdateButtonState();
        }

        private void OnDestroy()
        {
            ActualRequestManager.OnResetRequestFinished -= RegisterFinished;
        }
        #endregion

        #region UpdateButtonState
        /// <summary>
        /// Enable or not the button in case the field are filled correctly
        /// </summary>
        protected override void UpdateButtonState()
        {
            base.UpdateButtonState();
            _acceptButton.interactable &= (_email.text.Length > 0 && ActualRequestManager.CanReceiveANewRequest);
            _returnButton.interactable &= ActualRequestManager.CanReceiveANewRequest;
        }
        #endregion
        
        #region Request
        /// <summary>
        /// Call the request manager and the good function
        /// </summary>
        public void Register()
        {
            ActualRequestManager.ResetPassword(_email.text);
            UpdateButtonState();
        }
        private void RegisterFinished(bool success, string message)
        {
            UpdateButtonState();

            if (success)
            {
                _retreivePasswordSucess.SetEmail(_email.text);
                Show(false);

                _retreivePasswordSucess.Show(true);
            }
            else
                ErrorText.text = message;
        }
        #endregion
    }
}
