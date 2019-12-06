using System.Collections.Generic;
using System.Linq;
using Game.Notification;
using Game.SavingSystem;
using Game.SavingSystem.Datas;
using Menu.LevelSelector;
using UnityEngine;

namespace Game.HUD.Objectives
{
    public class ObjectiveHud : AccountAndMapSavingBehaviour
    {
        [Header("Prefabs")]
        [SerializeField] private ObjectiveIcon objective1 = null;
        [SerializeField] private ObjectiveIcon objective2 = null;
        [SerializeField] private ObjectiveIcon objective3 = null;
        
        [Header("Current Level Info")]
        [SerializeField] private LevelInfo levelInfo = null;
        
        [Header("Level Complete")]
        [SerializeField] private GameObject levelComplete = null;
        
        [Header("Notifications")] 
        [SerializeField] private NotificationEvent _notificationEvent = null;
        [SerializeField] private Game.Notification.Notification _notification = null;

        private LiveLevelInfo _currentLevel = null;
        private int _currentIndexLevel = -1;
        private ProfilData _profilData = null;
        

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

            if (objective3.IsComplete) return;
            objective3.SetComplete(true);
            _currentLevel.ChallengeThreeComplete = true;
            levelComplete.SetActive(true);
            
            if (_notification == null || _notificationEvent == null) return;
            
            _notificationEvent.Raise(_notification);  
        }

        protected override void SavingAccountData(AccountData data)
        {
            if (_profilData == null) return;
            if (_currentIndexLevel < 0)
            {
                _profilData.CharacterGlobalInfo.SaveLevels(new List<LiveLevelInfo>{ _currentLevel });   
            }
            else
            {
                _profilData.CharacterGlobalInfo.LevelInfoDatas[_currentIndexLevel].Challenge1Complete = objective1.IsComplete;
                _profilData.CharacterGlobalInfo.LevelInfoDatas[_currentIndexLevel].Challenge2Complete = objective2.IsComplete;
                _profilData.CharacterGlobalInfo.LevelInfoDatas[_currentIndexLevel].Challenge3Complete = objective3.IsComplete;    
            }
        }

        protected override void LoadingAccountData(AccountData data)
        {
            _profilData = SaveManager.Instance.SelectedProfil;
            if (levelInfo == null)
                return;
            _currentLevel = new LiveLevelInfo(levelInfo);
            if (_profilData == null)
                return;
            var index = _profilData.CharacterGlobalInfo.LevelInfoDatas.ToList()
                .FindIndex(it => it.LevelTitle == levelInfo.LevelTitle);
            _currentIndexLevel = index;

            if (_currentIndexLevel >= 0)
            {
                _currentLevel = new LiveLevelInfo(levelInfo, _profilData.CharacterGlobalInfo.LevelInfoDatas[index]);
            }
            objective1.SetComplete(_currentLevel.ChallengeOneComplete);
            objective2.SetComplete(_currentLevel.ChallengeTwoComplete);
            objective3.SetComplete(_currentLevel.ChallengeThreeComplete);

            if (!objective1.IsComplete || !objective2.IsComplete || !objective3.IsComplete) return;
            
            levelComplete.SetActive(true);
            
            if (_notification == null || _notificationEvent == null) return;
            
            _notificationEvent.Raise(_notification);   
        }
    }
}
