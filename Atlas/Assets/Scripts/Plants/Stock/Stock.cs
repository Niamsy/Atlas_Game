using System.Collections.Generic;
using UnityEngine;

namespace Plants
{
    public class Stock : MonoBehaviour
    {
        private List<Resources>     objects;
        private int                 count;
        protected int               limit;

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
            }
            count = objects.Count;
            return objects;
        }

        public List<Resources> Remove(int quantity)
        {
            if (quantity < count)
            {
                objects.RemoveRange(0, quantity);
                count = objects.Count;
            }
            else
            {
                objects.Clear();
                count = 0;
            }
            return objects;
        }
    }
}