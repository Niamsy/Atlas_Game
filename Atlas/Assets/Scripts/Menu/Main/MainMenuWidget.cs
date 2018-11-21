using Networking;
using UnityEngine;
using UnityEngine.UI;

namespace Menu.Main
{
    [RequireComponent(typeof(Animator))]
    public abstract class MainMenuWidget : MonoBehaviour
    {
        protected RequestManager        ActualRequestManager;

        [SerializeField] protected Text ErrorText;

        #region Displayed
        [SerializeField] private bool   _displayed;
        public bool                     Displayed
        {
            get { return (_displayed); }
        }
        #endregion
        
        #region Animator Variables
        private Animator    _animator;
        private int         _hashShowed = Animator.StringToHash("Showed");
        #endregion
        
        private void Awake()
        {
            _animator = GetComponent<Animator>();
            ActualRequestManager = FindObjectOfType<RequestManager>();
        
            if (ActualRequestManager == null)
                Debug.LogError("ERROR: No RequestManager found.");

            InitialiseWidget();
            
            Show(Displayed);
        }

        protected abstract void InitialiseWidget();
        
        public void Show(bool display)
        {
            _animator.SetBool(_hashShowed, display);
            _displayed = display;
            ErrorText.text = "";
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
        protected abstract void UpdateButtonState();
    }
}
