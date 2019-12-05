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

            if (_profilData == null || _profilData.CharacterGlobalInfo == null ||
                _profilData.CharacterGlobalInfo.LevelInfoDatas == null)
            {
                for (int index = 0; index < Levels.Count; index++)
                    _liveLevels.Add(new LiveLevelInfo(Levels[index]));
                
                return;
            }
            
            LevelInfoData[] levelInfoDatas = _profilData.CharacterGlobalInfo.LevelInfoDatas;
            for (int index = 0; index < levelInfoDatas.Length && index < Levels.Count; index++)
                _liveLevels.Add(new LiveLevelInfo(Levels[index], levelInfoDatas[index]));
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
