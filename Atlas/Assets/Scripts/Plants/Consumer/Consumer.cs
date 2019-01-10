using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Variables;

namespace Plants
{
    public class Consumer : MonoBehaviour, IConsumer
    {
        [SerializeField]
        protected Resources        type;
        [SerializeField]
        protected int              rate;
        [SerializeField]
        protected int              quantity;
        protected Stock            stocks;
        [SerializeField]
        protected int              starvationTimeLimit;
        protected bool             starverd;
        [SerializeField]
        protected FloatReference   range;

        public void Consume()
        {
            stocks.Remove(rate);
        }

        public void Load(IProducer producer, int quantity)
        {
            stocks.Put(producer.Unload(quantity));
        }

        public Stock Stocks
        {
            get { return stocks; }
        }

        public int Rate
        {
            get { return rate; }

            set { rate = value; }
        }

        public bool Starverd
        {
            get { return starverd; }

            set { starverd = value; }
        }

        public int StarvationTimeLimit
        {
            get { return starvationTimeLimit; }

            set { starvationTimeLimit = value; }
        }

        public int Quantity
        {
            get { return stocks.GetCount(); }

            set { quantity = value; }
        }

        public FloatReference Range
        {
            get { return range; }

            set { range = value; }
        }

        public Resources Type
        {
            get
            {
                return type;
            }
        }
    }
}