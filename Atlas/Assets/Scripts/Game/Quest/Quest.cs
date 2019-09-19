using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public class Quest
    {
        public Quest(string Name, string desc, List<Objective> obj, Func<GameObject, GameObject> reward)
        {
            questName = Name;
            rewarder = reward;
            questDescription = desc;
            __questObjs = new List<Objective>(obj);
        }

        private string questName;
        private List<Objective> __questObjs;
        private string questDescription { get; }
        private System.Func<GameObject, GameObject> rewarder;
        
        public List<Objective> getQuestObjs()
        {
            return __questObjs;
        }

        public string getQuestName()
        {
            return questName;
        }

        public string getQuestDesc()
        {
            return questDescription;
        }
        
        public bool isComplete()
        {
            foreach (Objective obj in __questObjs)
            {
                if (obj.isComplete() == false)
                {
                    return false;
                }
            }
            return true;
        }

        public bool UpdateQuest(ObjType type, string Id)
        {
            foreach (Objective obj in __questObjs)
            {
                if (obj.getObjType() == type)
                {
                    obj.compare(Id);
                }
            }
        return isComplete();
        }

        public bool ApplyReward(GameObject player)
        {
            foreach (Objective obj in __questObjs)
            {
                if (obj.isComplete() == false)
                {
                    return false;
                }
            }
            rewarder(player);
            return true;
        }
    }
