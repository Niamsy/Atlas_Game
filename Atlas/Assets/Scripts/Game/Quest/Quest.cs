using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Quest
{
    public class Quest : MonoBehaviour
    {
        public Quest(Func<GameObject, GameObject> reward, string desc)
        {
            rewarder = reward;
            questDescription = desc;
        }

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

        public void UpdateQuest(ObjType type, string Id)
        {
            foreach (Objective obj in __questObjs)
            {
                if (obj.getObjType() == type)
                {
                    obj.compare(Id);
                }
            }
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
}
