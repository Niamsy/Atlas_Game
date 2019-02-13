using System;
using UnityEngine;

namespace Menu.Settings
{
    public abstract class SettingEntry : MonoBehaviour
    {
        private void Awake()
        {
            Initialization();
        }
        
        protected abstract void Initialization();
        
        public abstract void LoadData();

        public abstract bool DidValueChanged();
        
        #region OnValueDidChanged
        protected void OnValueDidChanged()
        {
            if (OnValueChanged != null)
                OnValueChanged();
        }
        public event Action OnValueChanged;
        #endregion
    }
}
