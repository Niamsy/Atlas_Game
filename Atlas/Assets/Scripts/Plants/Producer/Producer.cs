using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Variables;

namespace Plants
{
    public class Producer  : MonoBehaviour, IProducer
    {
        [SerializeField]
        protected Resources type;
        [SerializeField]
        protected int rate;
        [SerializeField]
        protected int quantity;
        protected Stock stocks;
        protected bool starverd;
        [SerializeField]
        protected FloatReference range;

        public void Produce()
        {
            List<Resources> addToStock = new List<Resources>();
            for (int i = 0; i < rate; ++i)
            {
                addToStock.Add(type.Create());
            }
            stocks.Put(addToStock);
        }

        public List<Resources> Unload(int sub_quantity)
        {
            return stocks.Remove(sub_quantity);
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