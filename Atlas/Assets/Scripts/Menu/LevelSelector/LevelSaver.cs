using System.Collections.Generic;
using Game.SavingSystem;
using Game.SavingSystem.Datas;
using UnityEngine;

namespace Menu.LevelSelector
{
    public class LevelSaver : AccountSavingBehaviour
    {
        [SerializeField] public List<LevelInfo> Levels = new List<LevelInfo>();
        [SerializeField] private GameObject _prefab = null;
        private List<LiveLevelInfo> _liveLevels = new List<LiveLevelInfo>();

        private ProfilData _profilData = null;

        protected override void SavingAccountData(AccountData data)
        {
            _profilData?.CharacterGlobalInfo?.SaveLevels(_liveLevels);
        }

        protected override void LoadingAccountData(AccountData data)
        {
            _profilData = SaveManager.Instance.SelectedProfil;

            if (_profilData == null)
                return;

            LevelInfoData[] levelInfoDatas = _profilData.CharacterGlobalInfo.LevelInfoDatas;
            for (int index = 0; index < Levels.Count; index++)
            {
                if (levelInfoDatas != null && index < levelInfoDatas.Length)
                    _liveLevels.Add(new LiveLevelInfo(Levels[index], levelInfoDatas[index]));
                else
                    _liveLevels.Add(new LiveLevelInfo(Levels[index]));
            }
            
            _profilData?.CharacterGlobalInfo?.SaveLevels(_liveLevels);
        }

        public void UpdateSelectedLevelWidget()
        {
            _prefab.SetActive(true);
            var levelSelector = _prefab.GetComponent<LevelSelector>();
            
            levelSelector.UpdateWidget(_liveLevels);
            levelSelector.Show(true);
        }
    }
}
