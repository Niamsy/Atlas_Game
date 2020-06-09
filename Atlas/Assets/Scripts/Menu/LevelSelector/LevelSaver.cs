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

        protected override void Awake()
        {
            base.Awake();
            //SaveManager.Instance.ReloadAccountData();
        }

        protected override void SavingAccountData(AccountData data)
        {
            _profilData?.CharacterGlobalInfo?.SaveLevels(_liveLevels);
        }

        protected override void LoadingAccountData(AccountData data)
        {
            _profilData = SaveManager.Instance.SelectedProfil;

            if (_profilData == null || _profilData.CharacterGlobalInfo.LevelInfoDatas == null)
            {
                foreach (var level in Levels)
                {
                    _liveLevels.Add(new LiveLevelInfo(level));
                } 
            }
            else
            {
                var levelInfoDatas = _profilData.CharacterGlobalInfo.LevelInfoDatas.ToList();
                int nbChallengeDone = 0;
                foreach (var levelInfo in Levels)
                {
                    var index = levelInfoDatas.FindIndex(it => it.LevelTitle == levelInfo.LevelTitle);
                    _liveLevels.Add(index >= 0
                        ? new LiveLevelInfo(levelInfo, levelInfoDatas[index])
                        : new LiveLevelInfo(levelInfo));
                    nbChallengeDone += _liveLevels.Last().NumberChallengeComplete();
                    Debug.Log("NbChallenge on level :" + _liveLevels.Count + " : " + nbChallengeDone);
                }
                _profilData.CharacterGlobalInfo.PlayerChallengeOwned = nbChallengeDone;
                Debug.Log("Total :" + _profilData.CharacterGlobalInfo.PlayerChallengeOwned);
            }
            _profilData?.CharacterGlobalInfo?.SaveLevels(_liveLevels);
        }

        public void forceReloadData(AccountData data)
        {
            //LoadingAccountData(data);
        }

        public void UpdateSelectedLevelWidget()
        {
            _prefab.SetActive(true);
            var levelSelector = _prefab.GetComponent<LevelSelector>();

            if (levelSelector != null)
            {
                levelSelector.UpdateWidget(_liveLevels, _profilData?.CharacterGlobalInfo?.PlayerChallengeOwned ?? 0);
                levelSelector.Show(true);
            }
        }
    }
}
