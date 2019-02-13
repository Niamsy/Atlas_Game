using Networking;
using UnityEngine;
using UnityEngine.UI;

namespace Menu
{
    [RequireComponent(typeof(Animator))]
    ///ToDo: Should use MenuWidget and remove copy of code
    public abstract class MainMenuWidget : MonoBehaviour
    {
        protected RequestManager        ActualRequestManager;
        private Selectable[]          _childSelectables;
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
            _childSelectables = GetComponentsInChildren<Selectable>();
            
            ActualRequestManager = RequestManager.Instance;
        
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
            if (ErrorText != null)
                ErrorText.text = "";

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
