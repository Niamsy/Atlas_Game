using System.Collections.Generic;
using System.Linq;
using Game.SavingSystem;
using Game.SavingSystem.Datas;
using Tools.Tools;
using UnityEngine;

namespace Game.Questing
{
    public class QuestingSaver : MapSavingBehaviour
    {
        public struct LiveQuestData
        {
            public Quest Quest;
            public int CurrentlyAccomplished;

            public LiveQuestData(Quest quest, int currentlyAccomplished)
            {
                Quest = quest;
                CurrentlyAccomplished = currentlyAccomplished;
            }
        }
        
        private List<Quest> _quests;
        private readonly List<LiveQuestData> _liveQuests = new List<LiveQuestData>();

        public List<LiveQuestData> LiveQuests
        {
            get => _liveQuests;
        }

        private void OnEnable()
        {
            _quests = new AssetsLoader<Quest>().Load();
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
                    LiveQuests.Add(new LiveQuestData(questSO, questData.CurrentlyAccomplished));
                }
                else
                {
                    Debug.LogWarning($"Quest with ID: {questData.Id} does not exist");
                }
            }
        }
    }
}