using System;
using UnityEngine;

namespace Menu.Settings.Content
{
    public abstract class SettingEntry : MonoBehaviour
    {
        public abstract void Initialization();
        
        public abstract void LoadData();

        public abstract bool DidValueChanged();
        
        #region OnValueDidChanged
        protected virtual void OnValueDidChanged()
        {
            if (OnValueChanged != null)
                OnValueChanged();
        }
        public event Action OnValueChanged;
        #endregion
    }
}
