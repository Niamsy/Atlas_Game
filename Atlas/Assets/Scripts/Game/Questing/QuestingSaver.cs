using System.Collections.Generic;
using System.Linq;
using Game.Item;
using Game.SavingSystem;
using Game.SavingSystem.Datas;
using Tools.Tools;
using UnityEngine;

namespace Game.Questing
{
    public class QuestingSaver : MapSavingBehaviour
    {
        [SerializeField] private SideQuestPanelHud _sideQuestPanelHud = null;
        [SerializeField] private QuestingHud _newQuestHud = null;
        [SerializeField] private QuestingHud _questCompleteHud = null;
        [SerializeField] private QuestingHud _questLogHud = null;

        private List<Quest> _quests;
        private readonly List<LiveQuest> _liveQuests = new List<LiveQuest>();

        public void AddQuest(Quest quest)
        {
            var liveQuest = new LiveQuest(quest);
            _liveQuests.Add(liveQuest);
            _newQuestHud.SetData(liveQuest);
            _sideQuestPanelHud.AddQuest(liveQuest);
            _newQuestHud.Show(true);
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
                _questCompleteHud.Show(true);
                _questCompleteHud.SetDelegate(quest =>
                {
                    // Give Rewards to the player
                    Debug.Log("GIVE REWARDS TO THE PLAYER DAMN");
                    GiveRewards(quest);
                });
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

        private void OnEnable()
        {
            SaveManager.Instance.InputControls.Player.Quest.performed += _questLogHud.OpenCloseQuesting;
            SaveManager.Instance.InputControls.Player.Quest.Enable();
            _quests = new AssetsLoader<Quest>().Load();
        }

        private void OnDisable()
        {
            SaveManager.Instance.InputControls.Player.Quest.performed -= _questLogHud.OpenCloseQuesting;
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