using System.Collections;
using System.Collections.Generic;
using Tools;
using UnityEngine;



public class QuestManager : Singleton<QuestManager>
{
    public bool callForUpdate;
   private QuestList qlist;
   public QuestManager()
    {
        callForUpdate = false;
        qlist = new QuestList();
        __activeQuests = new List<Quest>();
        __availableQuests = qlist.getQuestList();
        __activeQuests.Add(__availableQuests[0]);
    }

    public Quest getQuestData(int idx)
    {
        return __activeQuests[idx];
    }

    public int getActiveQuestQte()
    {
        return __activeQuests.Count;
    }

    public void UpdateQuestsWith(ObjType objectiveType, string Id, GameObject Player)
    {
        callFwdorUpdate = true;
        foreach (Quest q in __activeQuests)
        {
            q.UpdateQuest(objectiveType, Id);
        }
    }

   public void addQuest(Quest q)
   {
         __activeQuests.Add(q);
   } 

    public bool applyRewardIdx(GameObject Player, int QuestIdx)
    {
            if (__activeQuests[QuestIdx].isComplete())
            {
                __activeQuests[QuestIdx].ApplyReward(Player);
                __activeQuests.RemoveAt(QuestIdx);
                return true;
            }
        return false;
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
