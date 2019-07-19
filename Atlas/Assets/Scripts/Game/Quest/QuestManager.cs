using System.Collections;
using System.Collections.Generic;
using Tools;
using UnityEngine;



public class QuestManager : Singleton<QuestManager>
{
   private QuestList qlist;
   QuestManager()
    {
        qlist = new QuestList();
        __activeQuests = new List<Quest>();
        __availableQuests = qlist.getQuestList();
        __activeQuests.Add(__availableQuests[0]);
    }

    public void UpdateQuestsWith(ObjType objectiveType, string Id, GameObject Player)
    {
        bool flag = false;
        foreach (Quest q in __activeQuests)
        {
            if (q.UpdateQuest(objectiveType, Id))
            {
                flag = true;
            }
        }
        if (flag)
        {
            applyRewards(Player);
        }
    }

   public void addQuest(Quest q)
   {
         __activeQuests.Add(q);
   } 

   public void applyRewards(GameObject player)
   {
        List<int> idxs = new List<int>();
        int i = 0;
        foreach(Quest q in __activeQuests)
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
           __activeQuests.RemoveAt(idx);
        }
   }
   private List<Quest> __activeQuests;
   private List<Quest> __availableQuests;
}
