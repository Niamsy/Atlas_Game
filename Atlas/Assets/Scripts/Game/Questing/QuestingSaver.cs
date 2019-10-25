using System;
using System.Collections.Generic;
using System.Linq;
using Game.Item;
using Game.SavingSystem;
using Game.SavingSystem.Datas;
using Localization;
using TMPro;
using Tools.Tools;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Questing
{
    public class QuestingSaver : MapSavingBehaviour
    {
        [SerializeField] private SideQuestPanelHud _sideQuestPanelHud = null;
        [SerializeField] private QuestingHud _newQuestHud = null;
        [SerializeField] private QuestingHud _questCompleteHud = null;
        [SerializeField] private QuestingHud _questLogHud = null;
        [SerializeField] private AnimationClip _closingAnimation = null;
        [SerializeField] private GameObject warning = null;
        [SerializeField] private LocalizedText warningText = null;
        
        private List<Quest> _quests;
        private readonly List<LiveQuest> _liveQuests = new List<LiveQuest>();
        private LiveQuest? _currentlySelectedQuest = null;

        private float closingTime = 0f;

        public void AddQuest(Quest quest)
        {
            var liveQuest = new LiveQuest(quest);
            _currentlySelectedQuest = liveQuest;
            _liveQuests.Add(liveQuest);
            _newQuestHud.SetData(liveQuest);
            _sideQuestPanelHud.ConsumeLiveQuest(liveQuest);
            ShowNewQuestHud();
        }

        public void ValidateRequirement(Condition condition, ItemAbstract item, int count)
        {
            var toRemove = new List<LiveQuest>();
            
            foreach (var liveQuest in _liveQuests)
            {
                var requirements = liveQuest.Requirements.Where(req =>
                    req.Requirement.Condition.Id == condition.Id && req.Requirement.Argument.Id == item.Id);
                
                foreach (var liveRequirement in requirements)
                {
                    liveRequirement.IncrementAccomplished(count);
                }

                if (!liveQuest.IsFinished) continue;
                toRemove.Add(liveQuest);
                _questCompleteHud.SetData(liveQuest);
                _questCompleteHud.SetDelegate(quest =>
                {
                    // Give Rewards to the player
                    GiveRewards(quest);
                });
                ShowCompletedQuestHud();
                
                if (_currentlySelectedQuest == null || liveQuest.Quest != _currentlySelectedQuest.Value.Quest) continue;
                if (_liveQuests.Count > 0)
                    _currentlySelectedQuest = _liveQuests.First();
                else
                    _currentlySelectedQuest = null;
            }
            
            foreach (var liveQuest in toRemove)
            {
                _sideQuestPanelHud.RemoveQuest(liveQuest);
                _liveQuests.Remove(liveQuest);
            }
        }

        public void GiveRewards(LiveQuest quest)
        {
            
        }
        
        public List<LiveQuest> LiveQuests
        {
            get => _liveQuests;
        }

        private void OpenCloseQuestLog(InputAction.CallbackContext ctx)
        {
            if (_newQuestHud.Displayed)
            {
                _newQuestHud.Show(false);
                return;
            }

            if (_questCompleteHud.Displayed)
            {
                _newQuestHud.Show(false);
                return;
            }
            
            if (!_questLogHud.Displayed && !_currentlySelectedQuest.HasValue)
            {
                var warn = Instantiate(warning, transform);
                warn.GetComponent<TextMeshProUGUI>().text = warningText;
                warn.transform.localScale = new Vector3(1, 1, 1);
            } 
            else if (!_questLogHud.Displayed && _currentlySelectedQuest != null)
            {
                OnQuestClick(_currentlySelectedQuest.Value);
            }
            else
            {
                _questLogHud.Show(false);
            }
        }
        
        private void ShowNewQuestHudDelay()
        {
            _questCompleteHud.Show(true);
        }
        
        private void ShowNewQuestHud()
        {
            if (_questLogHud.Displayed)
            {
                _questLogHud.Show(false);
                Invoke(nameof(ShowNewQuestHudDelay), closingTime);
            } 
            else if (_questCompleteHud.Displayed)
            {
                _questCompleteHud.Show(false);
                Invoke(nameof(ShowNewQuestHudDelay), closingTime);
            }
            else if (!_newQuestHud.Displayed)
            {
                _newQuestHud.Show(true);
            }
        }

        private void ShowCompletedQuestHudDelay()
        {
            _questCompleteHud.Show(true);
        }
        
        private void ShowCompletedQuestHud()
        {
            if (_questLogHud.Displayed)
            {
                _questLogHud.Show(false);
                Invoke(nameof(ShowCompletedQuestHudDelay), closingTime);
            } 
            else if (_newQuestHud.Displayed)
            {
                _newQuestHud.Show(false);
                Invoke(nameof(ShowCompletedQuestHudDelay), closingTime);
            }
            else if (!_questCompleteHud.Displayed)
            {
                _questCompleteHud.Show(true);
            }
        }

        private void ShowQuestLogDelay()
        {
            _questLogHud.Show(true);
        }

        
        private void OnQuestClick(LiveQuest quest)
        {
            _currentlySelectedQuest = quest;
            _questLogHud.SetData(_currentlySelectedQuest.Value);
            if (_newQuestHud.Displayed)
            {
                _newQuestHud.Show(false);
                Invoke(nameof(ShowQuestLogDelay), closingTime);
            } 
            else if (_questCompleteHud.Displayed)
            {
                _questCompleteHud.Show(false);
                Invoke(nameof(ShowQuestLogDelay), closingTime);
            }
            else if (!_questLogHud.Displayed)
            {
                _questLogHud.Show(true);
            }
        }

        private void OnEnable()
        {
            SaveManager.Instance.InputControls.Player.Quest.performed += OpenCloseQuestLog;
            SaveManager.Instance.InputControls.Player.Quest.Enable();
            _quests = new AssetsLoader<Quest>().Load();
            _sideQuestPanelHud.SetOnOkClickDelegate(OnQuestClick);
            closingTime = _closingAnimation.length;
        }

        private void OnDisable()
        {
            SaveManager.Instance.InputControls.Player.Quest.performed -= OpenCloseQuestLog;
            SaveManager.Instance.InputControls.Player.Quest.Disable();
        }

        protected override void SavingMapData(MapData data)
        {
            data.Questing.Quests = LiveQuests.Select(liveQuest => new MapData.QuestData(liveQuest)).ToArray();
        }

        protected override void LoadingMapData(MapData data)
        {
            foreach (var questData in data.Questing.Quests)
            {
                var questSo = _quests.Find(quest => quest.Id == questData.Id);
                if (questSo != null)
                {
                    LiveQuests.Add(new LiveQuest(questSo, questData.Requirements));
                }
                else
                {
                    Debug.LogWarning($"Quest with ID: {questData.Id} does not exist");
                }
            }
        }
    }
}