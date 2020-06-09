using System;
using Inputs;
using UnityEngine;

namespace Menu
{
    [RequireComponent(typeof(Animator))]
    public abstract class MenuWidget : MonoBehaviour
    {
        #region Displayed

        [SerializeField] private bool _enableCursorOnDisplay = false;
        [SerializeField] private bool   _displayed;
        public bool                     Displayed => _displayed;

        public Animator MenuAnimator => _animator;
        #endregion
        
        #region Animator Variables
        private Animator    _animator;
        private int         _hashShowed = Animator.StringToHash("Showed");
        #endregion

        protected virtual void Awake()
        {
            _animator = GetComponent<Animator>();

            InitialiseWidget();
            
            Show(Displayed, true);
        }

        protected abstract void InitialiseWidget();

        public event Action<bool> OnShow;
        
        public virtual void Show(bool display, bool force = false)
        {
            if (_enableCursorOnDisplay && display != _displayed)
            {
                if (display)
                   CursorTools.AskForCursor(this);
                else
                    CursorTools.LetGoCursor(this);
            }
            if (OnShow != null && !force)
                OnShow(display);
            if (MenuAnimator != null)
                MenuAnimator.SetBool(_hashShowed, display);
            _displayed = display;
        }
        
        public void Open() { Show(true); }
        public void Close() { Show(false); }
    }
}
