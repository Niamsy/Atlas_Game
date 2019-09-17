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
