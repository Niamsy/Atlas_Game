using System;
using UnityEngine;
using FileSystem;

namespace Menu.Settings.Content
{
    public abstract class SettingEntry : MonoBehaviour
    {
        public AtlasFileSystem _fs;

        private void Awake()
        {
            _fs = AtlasFileSystem.Instance;
        }
            
        public string Key = FileSystem.Key.Lang;
        public string Section = FileSystem.Section.Default;
        
        public abstract string Value();

        public abstract void Initialization();
        
        public abstract void LoadData();

        public abstract bool DidValueChanged();
       
        public virtual void ReloadData() {}

        public abstract void SaveData();

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
