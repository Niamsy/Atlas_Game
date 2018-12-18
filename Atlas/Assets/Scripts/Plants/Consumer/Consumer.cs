using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Variables;

namespace Plants
{
    public class Consumer<T> : MonoBehaviour
    {
        public Resources            type;
        protected FloatVariable     rate;
        protected int               quantity;
        protected Stock<T>          stock;
        protected int               starvationTimeLimit;
        protected bool              starverd;
        protected FloatReference    range;

        public void Consume()
        {

        }

        public void Load(Producer<T> producer, int quantity)
        {
            stock.Put(producer.Unload(quantity));
        }

        public Stock<T> GetStock()
        {
            return stock;
        }

        public Resources GetResourceType()
        {
            return type;
        }

        public void SetRate(FloatVariable n_rate)
        {
            rate = n_rate;
        }
    }
}