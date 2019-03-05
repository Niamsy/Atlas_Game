using System.Collections.Generic;
using UnityEngine;

namespace Plants
{
    public class Stock : MonoBehaviour
    {
        private List<Resources> objects = new List<Resources>();
        private int                 count = 0;
        protected int               limit = 60;

        public int GetCount()
        {
            return count;
        }

        public int GetLimit()
        {
            return limit;
        }

        public List<Resources> GetObjects()
        {
            return objects;
        }

        public void SetLimit(int n_limit)
        {
            limit = n_limit;
        }

        public void SetCount(int lcount)
        {
            count = lcount;
        }

        public List<Resources> Put(List<Resources> quantity)
        {
            foreach (Resources obj in quantity)
            {
                objects.Add(obj);
                if (objects.Count >= limit)
                    break;
            }
            count = objects.Count;
            return objects;
        }

        public List<Resources> Remove(int quantity)
        {
            List<Resources> toRemove = new List<Resources>();
            int i = 0;
            
            while (i < quantity && objects.Count > 0)
            {
                toRemove.Add(objects[0]);
                objects.RemoveAt(0);
                ++i;
                --count;
            }

            return toRemove;
        }
    }
}