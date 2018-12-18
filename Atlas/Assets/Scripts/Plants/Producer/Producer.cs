using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Variables;

namespace Plants
{
    public class Producer<T> : MonoBehaviour
    {
        public Resources            type;
        protected FloatVariable     rate;
        protected int               quantity;
        protected Stock<T>          stock;
        protected bool              starverd;
        protected FloatReference    range;

        public void Produce()
        {

        }

        public List<T> Unload(int sub_quantity)
        {
            return stock.Remove(sub_quantity);
        }

        public Stock<T> GetStock()
        {
            return stock;
        }

        public Resources GetRecourceType()
        {
            return type;
        }
    }
}