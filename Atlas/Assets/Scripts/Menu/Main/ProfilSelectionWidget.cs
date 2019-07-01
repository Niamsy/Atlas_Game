﻿using Game.SavingSystem;
using Game.SavingSystem.Datas;
using SceneManagement;

namespace Menu.Main
{
    public class ProfilSelectionWidget : MenuWidget
    {
        public ProfilDisplay[] Displays = new ProfilDisplay[5];
        private SaveManager _saveManager = null;
        
        public int NextSceneIndex = 2;
        public int MainMenuSceneIndex = 1;
        
        protected override void InitialiseWidget()
        {
            _saveManager = SaveManager.Instance;
            SaveManager.UponLoadingAccountData += LoadingAccountData;
            foreach (var display in Displays)
                display.OnClick += SelectProfil;
        }
        
        protected virtual void OnDestroy()
        {
            SaveManager.UponLoadingAccountData -= LoadingAccountData;
        }

        private void LoadingAccountData(AccountData data)
        {
            for (int index = 0; index < data.Profils.Length; ++index)
                Displays[index].SetProfilData(data.Profils[index]);
        }

        protected void SelectProfil(ProfilData data)
        {
            if (false) //Delete
                SaveManager.DestroyProfile(_saveManager.AccountData, data);
            else if (data.Used == false) //Create
                    SaveManager.InstantiateProfilToUse(_saveManager.AccountData, data, "Game Profil " + data.ID);
            else //Select Profil
            {
                _saveManager.SelectProfilToUseForSave(data);
                SceneLoader.Instance.LoadScene(NextSceneIndex, MainMenuSceneIndex);
            }
        }
    }
}
