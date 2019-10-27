using System;
using System.Collections.Generic;
using System.Linq;
using AtlasAudio;
using AtlasEvents;
using Game.Inventory;
using Game.Item;
using Game.SavingSystem;
using Game.SavingSystem.Datas;
using Leveling;
using Localization;
using Player;
using TMPro;
using Tools.Tools;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace Game.Questing
{
    public class QuestingSaver : MapSavingBehaviour
    {
        [FormerlySerializedAs("_sideQuestPanelHud")] [SerializeField] private SideQuestPanelHud sideQuestPanelHud = null;
        [FormerlySerializedAs("_newQuestHud")] [SerializeField] private QuestingHud newQuestHud = null;
        [FormerlySerializedAs("_questCompleteHud")] [SerializeField] private QuestingHud questCompleteHud = null;
        [FormerlySerializedAs("_questLogHud")] [SerializeField] private QuestingHud questLogHud = null;
        [FormerlySerializedAs("_closingAnimation")] [SerializeField] private AnimationClip closingAnimation = null;
        [SerializeField] private GameObject warning = null;
        [SerializeField] private LocalizedText warningText = null;
        [SerializeField] private PlayerController playerController = null;
        [SerializeField] private LevelingEvent _event = null;

        [Header("Audio")] 
        [SerializeField] private Audio requirementValidatedAudio = null;
        [SerializeField] private AudioEvent validatedAudioEvent = null;
        
        private List<Quest> _quests;
        private readonly List<LiveQuest> _liveQuests = new List<LiveQuest>();
        private LiveQuest? _currentlySelectedQuest = null;

        private float closingTime = 0f;

        public void AddQuest(Quest quest)
        {
            var liveQuest = new LiveQuest(quest);
            _currentlySelectedQuest = liveQuest;
            _liveQuests.Add(liveQuest);
            newQuestHud.SetData(liveQuest);
            sideQuestPanelHud.ConsumeLiveQuest(liveQuest);
            ShowNewQuestHud();
        }

        public void ValidateRequirement(Condition condition, ItemAbstract item, int count)
        {
            var toRemove = new List<LiveQuest>();
            var validated = false;
            
            foreach (var liveQuest in _liveQuests)
            {
                var requirements = liveQuest.Requirements.Where(req =>
                    req.Requirement.Condition.Id == condition.Id && req.Requirement.Argument.Id == item.Id);
                
                foreach (var liveRequirement in requirements)
                {
                    liveRequirement.IncrementAccomplished(count);
                    validated = true;
                }

                if (!liveQuest.IsFinished) continue;
                toRemove.Add(liveQuest);
                questCompleteHud.SetData(liveQuest);
                questCompleteHud.SetDelegate(GiveRewards);
                ShowCompletedQuestHud();
            }

            if (validated)
            {
                if (validatedAudioEvent != null && requirementValidatedAudio != null)
                {
                    validatedAudioEvent.Raise(requirementValidatedAudio, null);
                }
            }
            
            foreach (var liveQuest in toRemove)
            {
                sideQuestPanelHud.RemoveQuest(liveQuest);
                _liveQuests.Remove(liveQuest);
                if (_currentlySelectedQuest == null || liveQuest.Quest.Id != _currentlySelectedQuest.Value.Quest.Id) continue;
                if (_liveQuests.Count > 0)
                    _currentlySelectedQuest = _liveQuests.First();
                else
                    _currentlySelectedQuest = null;
            }
        }

        public void GiveRewards(LiveQuest quest)
        {
            if (quest.Quest.Xp > 0)
            {
                _event.Raise(quest.Quest.Xp, 1);
            }
            
            var items = new List<ItemStack>();
            foreach (var reward in quest.Quest.Rewards)
            {
                var item = new ItemStack();
                item.SetItem(reward.reward, reward.Count);
                items.Add(item);
            }

            playerController.Inventory.AddItemStacks(items);
        }
        
        public List<LiveQuest> LiveQuests
        {
            get => _liveQuests;
        }

        private void OpenCloseQuestLog(InputAction.CallbackContext ctx)
        {
            if (newQuestHud.Displayed)
            {
                newQuestHud.Show(false);
                return;
            }

            if (questCompleteHud.Displayed)
            {
                newQuestHud.Show(false);
                return;
            }
            
            if (!questLogHud.Displayed && !_currentlySelectedQuest.HasValue)
            {
                var warn = Instantiate(warning, transform);
                warn.GetComponent<TextMeshProUGUI>().text = warningText;
                warn.transform.localScale = new Vector3(1, 1, 1);
            } 
            else if (!questLogHud.Displayed && _currentlySelectedQuest != null)
            {
                OnQuestClick(_currentlySelectedQuest.Value);
            }
            else
            {
                questLogHud.Show(false);
            }
        }
        
        private void ShowNewQuestHudDelay()
        {
            questCompleteHud.Show(true);
        }
        
        private void ShowNewQuestHud()
        {
            if (questLogHud.Displayed)
            {
                questLogHud.Show(false);
                Invoke(nameof(ShowNewQuestHudDelay), closingTime);
            } 
            else if (questCompleteHud.Displayed)
            {
                questCompleteHud.Show(false);
                Invoke(nameof(ShowNewQuestHudDelay), closingTime);
            }
            else if (!newQuestHud.Displayed)
            {
                newQuestHud.Show(true);
            }
        }

        private void ShowCompletedQuestHudDelay()
        {
            questCompleteHud.Show(true);
        }
        
        private void ShowCompletedQuestHud()
        {
            if (questLogHud.Displayed)
            {
                questLogHud.Show(false);
                Invoke(nameof(ShowCompletedQuestHudDelay), closingTime);
            } 
            else if (newQuestHud.Displayed)
            {
                newQuestHud.Show(false);
                Invoke(nameof(ShowCompletedQuestHudDelay), closingTime);
            }
            else if (!questCompleteHud.Displayed)
            {
                questCompleteHud.Show(true);
            }
        }

        private void ShowQuestLogDelay()
        {
            questLogHud.Show(true);
        }

        
        private void OnQuestClick(LiveQuest quest)
        {
            _currentlySelectedQuest = quest;
            questLogHud.SetData(_currentlySelectedQuest.Value);
            if (newQuestHud.Displayed)
            {
                newQuestHud.Show(false);
                Invoke(nameof(ShowQuestLogDelay), closingTime);
            } 
            else if (questCompleteHud.Displayed)
            {
                questCompleteHud.Show(false);
                Invoke(nameof(ShowQuestLogDelay), closingTime);
            }
            else if (!questLogHud.Displayed)
            {
                questLogHud.Show(true);
            }
        }

        private void OnEnable()
        {
            SaveManager.Instance.InputControls.Player.Quest.performed += OpenCloseQuestLog;
            SaveManager.Instance.InputControls.Player.Quest.Enable();
            _quests = new AssetsLoader<Quest>().Load();
            sideQuestPanelHud.SetOnOkClickDelegate(OnQuestClick);
            closingTime = closingAnimation.length;
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