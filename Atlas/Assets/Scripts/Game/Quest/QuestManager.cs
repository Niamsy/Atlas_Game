using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Quest
{


    public class QuestManager : Tools.Singleton<QuestManager>
    {

        public void UpdateQuestsWith(ObjType objectiveType, string Id)
        {
            foreach (Quest q in __quests)
            {
                q.UpdateQuest(objectiveType, Id);
            }
        }

        public void addQuest(Quest q)
        {
            __quests.Add(q);
        }

        public void applyRewards(GameObject player)
        {
            List<int> idxs = new List<int>();
            int i = 0;
            foreach(Quest q in __quests)
            {
                if (q.isComplete())
                {
                    q.ApplyReward(player);
                    idxs.Add(i);
                }
                ++i;
            }
            foreach (int idx in idxs)
            {
                __quests.RemoveAt(idx);
            }
        }

        private List<Quest> __quests;
    }
}
