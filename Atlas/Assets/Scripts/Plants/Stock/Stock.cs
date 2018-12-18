using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Plants
{
    public class Stock<T> : MonoBehaviour
    {
        private List<T>     objects;
        private int         count;
        protected int       limit;

        public int GetCount()
        {
            return count;
        }

        public int GetLimit()
        {
            return limit;
        }

        public void SetLimit(int n_limit)
        {
            limit = n_limit;
        }

        public List<T> Put(List<T> quantity)
        {
            foreach (T obj in quantity)
            {
                objects.Add(obj);
            }
            count = objects.Count;
            return objects;
        }

        public List<T> Remove(int quantity)
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