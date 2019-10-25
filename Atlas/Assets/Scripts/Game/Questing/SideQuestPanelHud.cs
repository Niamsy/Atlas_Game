using System;
using System.Collections.Generic;
using System.Linq;
using Tools;
using UnityEngine;

namespace Game.Questing
{
    public class SideQuestPanelHud : MonoBehaviour
    {
        [SerializeField] private ObjectPool questPool = null;
        [SerializeField] private ObjectPool requirementPool = null;

        private readonly List<LiveQuest> _quests = new List<LiveQuest>();
        public delegate void OnQuestClickDelegate(LiveQuest liveQuest);
        private OnQuestClickDelegate _onQuestClickDelegate = null;

        public void RemoveQuest(LiveQuest quest)
        {
            var quests = GetComponentsInChildren<QuestHud>().Where(it => it.LiveQuest.Quest.Id == quest.Quest.Id);
            foreach (var questHud in quests)
            {
                questHud.ClearRequirements();
                questPool.ReturnObject(questHud.gameObject);
            }

            _quests.RemoveAll(it => it.Quest.Id == quest.Quest.Id);
        }

        public void ConsumeLiveQuest(LiveQuest liveQuest)
        {
            _quests.Add(liveQuest);
            var questObj = questPool.GetObject();
            questObj.transform.SetParent(transform);
            questObj.transform.localScale = new Vector3(1, 1, 1);
            var questHud = questObj.GetComponent<QuestHud>();
            questHud.SetOnOkClickDelegate(_onQuestClickDelegate);
            questHud.SetLiveQuest(liveQuest, requirementPool);
        }

        public void SetOnOkClickDelegate(OnQuestClickDelegate _delegate)
        {
            _onQuestClickDelegate = _delegate;
        }
    }
}