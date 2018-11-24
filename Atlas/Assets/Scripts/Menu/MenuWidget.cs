using Networking;
using UnityEngine;
using UnityEngine.UI;

namespace Menu
{
    [RequireComponent(typeof(Animator))]
    public abstract class MenuWidget : MonoBehaviour
    {
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

            InitialiseWidget();
            
            Show(Displayed);
        }

        protected abstract void InitialiseWidget();
        
        public virtual void Show(bool display)
        {
            _animator.SetBool(_hashShowed, display);
            _displayed = display;
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
