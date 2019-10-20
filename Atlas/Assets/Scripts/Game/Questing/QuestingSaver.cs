using System;
using System.Collections.Generic;
using System.Linq;
using Game.Item;
using Game.SavingSystem;
using Game.SavingSystem.Datas;
using Tools.Tools;
using UnityEngine;

namespace Game.Questing
{
    [RequireComponent(typeof(QuestingHUD))]
    public class QuestingSaver : MapSavingBehaviour
    {
        private List<Quest> _quests;
        private QuestingHUD _questingHud;
        private readonly List<LiveQuest> _liveQuests = new List<LiveQuest>();
        
        public void AddQuest(Quest quest)
        {
            _liveQuests.Add(new LiveQuest(quest));
            _questingHud.Show(true);
        }

        public void ValidateRequirement(Condition condition, ItemAbstract item, int count)
        {
            foreach (var liveQuest in _liveQuests)
            {
                var requirements = liveQuest.Requirements.Where(req =>
                    req.Requirement.Condition.Id == condition.Id && req.Requirement.Argument.Id == item.Id);
                
                foreach (var liveRequirement in requirements)
                {
                    liveRequirement.IncrementAccomplished(count);
                }

                if (liveQuest.IsFinished)
                {
                    // TODO Trigger GUI for finished Quest
                    // ON OK PRESSED, GIVE REWARDS TO PlAYER, REMOVE QUEST FROM LIST
                }
            }
        }
        
        protected override void Awake()
        {
            base.Awake();
            _questingHud = GetComponent<QuestingHUD>();
        }

        public List<LiveQuest> LiveQuests
        {
            get => _liveQuests;
        }

        //TODO change the questing menu to log quest
        private void OnEnable()
        {
            SaveManager.Instance.InputControls.Player.Quest.performed += _questingHud.OpenCloseQuesting;
            SaveManager.Instance.InputControls.Player.Quest.Enable();
            _quests = new AssetsLoader<Quest>().Load();
        }

        private void OnDisable()
        {
            SaveManager.Instance.InputControls.Player.Quest.performed -= _questingHud.OpenCloseQuesting;
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
                var questSO = _quests.Find(quest => quest.Id == questData.Id);
                if (questSO != null)
                {
                    LiveQuests.Add(new LiveQuest(questSO, questData.Requirements));
                }
                else
                {
                    Debug.LogWarning($"Quest with ID: {questData.Id} does not exist");
                }
            }
        }
    }
}