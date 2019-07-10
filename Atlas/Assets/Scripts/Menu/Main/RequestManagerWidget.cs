using Networking;
using UnityEngine;
using UnityEngine.UI;

namespace Menu.Main
{
    public abstract class RequestManagerWidget : MenuWidget
    {
        protected RequestManager          ActualRequestManager = null;
        private Selectable[]              _childSelectables = null;
        [SerializeField] protected Text   ErrorText = null;

        protected override void Awake()
        {
            _childSelectables = GetComponentsInChildren<Selectable>();
            ActualRequestManager = RequestManager.Instance;
            if (ActualRequestManager == null)
                Debug.LogError("ERROR: No RequestManager found.");
            base.Awake();
        }
        
        public override void Show(bool display, bool force = false)
        {
            if (ErrorText != null)
                ErrorText.text = "";
            base.Show(display, force);

            UpdateButtonState();
        }
        
        /// <summary>
        /// Enable or not the button in case the field are filled correctly
        /// </summary>
        /// <param name="value">Value unused (Here to be added as a listener)</param>
        protected void UpdateButtonState_StringListener(string value)
        {
            UpdateButtonState();
        }

        /// <summary>
        /// Enable or not the button in case the field are filled correctly
        /// </summary>
        protected virtual void UpdateButtonState()
        {
            foreach (var selectables in _childSelectables)
                selectables.interactable = Displayed;
        }
    }
}
