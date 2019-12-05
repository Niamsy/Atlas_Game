using System;
using System.Collections.Generic;
using System.Linq;
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

            if (_profilData?.CharacterGlobalInfo?.LevelInfoDatas == null) return;
            
            var levelInfoDatas = _profilData.CharacterGlobalInfo.LevelInfoDatas.ToList();
            foreach (var levelInfo in Levels)
            {
                var index = levelInfoDatas.FindIndex(it => it.LevelTitle == levelInfo.LevelTitle);
                _liveLevels.Add(index >= 0
                    ? new LiveLevelInfo(levelInfo, levelInfoDatas[index])
                    : new LiveLevelInfo(levelInfo));
            }
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
