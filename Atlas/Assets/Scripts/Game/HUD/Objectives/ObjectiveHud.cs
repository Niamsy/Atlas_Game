using System.Collections.Generic;
using System.Linq;
using Game.SavingSystem;
using Game.SavingSystem.Datas;
using Menu.LevelSelector;
using TMPro;
using UnityEngine;

namespace Game.HUD.Objectives
{
    public class ObjectiveHud : AccountAndMapSavingBehaviour
    {
        [SerializeField] private ObjectiveIcon objective1 = null;
        [SerializeField] private ObjectiveIcon objective2 = null;
        [SerializeField] private ObjectiveIcon objective3 = null;
        [SerializeField] private LevelInfo levelInfo = null;

        private LiveLevelInfo _currentLevel = null;
        private int _currentIndexLevel = -1;
        
        [SerializeField] private GameObject levelComplete = null;

        protected override void Awake()
        {
            base.Awake();
        }

        public void ValidateOneObjective()
        {
            if (!objective1.IsComplete)
            {
                objective1.SetComplete(true);
                _currentLevel.ChallengeOneComplete = true;
                return;
            }

            if (!objective2.IsComplete)
            {
                objective2.SetComplete(true);
                _currentLevel.ChallengeTwoComplete = true;
                return;
            }

            if (!objective3.IsComplete)
            {
                objective3.SetComplete(true);
                _currentLevel.ChallengeThreeComplete = true;
                levelComplete.SetActive(true);
            }
        }

        protected override void SavingAccountData(AccountData data)
        {
            if (_currentIndexLevel < 0)
            {
                data.CharacterGlobalInfo.SaveLevels(new List<LiveLevelInfo>{ _currentLevel });   
            }
            else
            {
                data.CharacterGlobalInfo.LevelInfoDatas[_currentIndexLevel].Challenge1Complete = objective1.IsComplete;
                data.CharacterGlobalInfo.LevelInfoDatas[_currentIndexLevel].Challenge2Complete = objective2.IsComplete;
                data.CharacterGlobalInfo.LevelInfoDatas[_currentIndexLevel].Challenge3Complete = objective3.IsComplete;    
            }
        }

        protected override void LoadingAccountData(AccountData data)
        {
            if (levelInfo == null) return;

            var index = data.CharacterGlobalInfo.LevelInfoDatas.ToList()
                .FindIndex(it => it.LevelTitle == levelInfo.LevelTitle);
            _currentIndexLevel = index;

            if (_currentIndexLevel < 0)
            {
                _currentLevel = new LiveLevelInfo(levelInfo);
            }
            else
            {
                _currentLevel = new LiveLevelInfo(levelInfo, data.CharacterGlobalInfo.LevelInfoDatas[index]);
            }
            
            objective1.SetComplete(_currentLevel.ChallengeOneComplete);
            objective2.SetComplete(_currentLevel.ChallengeTwoComplete);
            objective3.SetComplete(_currentLevel.ChallengeThreeComplete);
            
            if (objective1.IsComplete && objective2.IsComplete && objective3.IsComplete)
            {
                levelComplete.SetActive(true);
            }
        }
    }
}
