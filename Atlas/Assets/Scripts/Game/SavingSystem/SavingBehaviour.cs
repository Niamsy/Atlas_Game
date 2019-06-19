using Game.SavingSystem.Datas;
using UnityEngine;

namespace Game.SavingSystem
{
    public abstract class AccountAndMapSavingBehaviour : MonoBehaviour
    {
        #region Methods
        protected virtual void Awake()
        {
            SaveManager.BeforeSavingMapData += SavingMapData;
            SaveManager.UponLoadingMapData += LoadingMapData;
            SaveManager.BeforeSavingAccountData += SavingAccountData;
            SaveManager.UponLoadingAccountData += LoadingAccountData;
        }

        protected virtual void OnDestroy()
        {
            SaveManager.BeforeSavingMapData -= SavingMapData;
            SaveManager.UponLoadingMapData -= LoadingMapData;
            SaveManager.BeforeSavingAccountData -= SavingAccountData;
            SaveManager.UponLoadingAccountData -= LoadingAccountData;
        }

        protected virtual void SavingMapData(MapData data) {}
        protected virtual void LoadingMapData(MapData data) {}
        protected virtual void SavingAccountData(AccountData data) {}
        protected virtual void LoadingAccountData(AccountData data) {}
        #endregion
    }
    
    public abstract class AccountSavingBehaviour : MonoBehaviour
    {
        #region Methods
        protected virtual void Awake()
        {
            SaveManager.BeforeSavingAccountData += SavingAccountData;
            SaveManager.UponLoadingAccountData += LoadingAccountData;
        }

        protected virtual void OnDestroy()
        {
            SaveManager.BeforeSavingAccountData -= SavingAccountData;
            SaveManager.UponLoadingAccountData -= LoadingAccountData;
        }

        protected virtual void SavingAccountData(AccountData data) {}
        protected virtual void LoadingAccountData(AccountData data) {}
        #endregion
    }
    
    public abstract class MapSavingBehaviour : MonoBehaviour
    {
        #region Methods
        protected virtual void Awake()
        {
            SaveManager.BeforeSavingMapData += SavingMapData;
            SaveManager.UponLoadingMapData += LoadingMapData;
        }

        protected virtual void OnDestroy()
        {
            SaveManager.BeforeSavingMapData -= SavingMapData;
            SaveManager.UponLoadingMapData -= LoadingMapData;
        }

        protected virtual void SavingMapData(MapData data) {}
        protected virtual void LoadingMapData(MapData data) {}
        #endregion
    }
}
