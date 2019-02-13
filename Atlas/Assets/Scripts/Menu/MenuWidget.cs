using System;
using UnityEngine;

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
            
            Show(Displayed, true);
        }

        protected abstract void InitialiseWidget();

        public event Action<bool> OnShow;
        
        public virtual void Show(bool display, bool force = false)
        {
            if (OnShow != null && !force)
                OnShow(display);
            _animator.SetBool(_hashShowed, display);
            _displayed = display;
        }
        
        public void Open() { Show(true); }
        public void Close() { Show(false); }
    }
}
