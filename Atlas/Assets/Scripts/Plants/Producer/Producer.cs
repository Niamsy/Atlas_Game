﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Variables;

namespace Plants
{
    public class Producer<T>  : MonoBehaviour where T : IResource<T>
    {
        public IResource<T>         type;
        [SerializeField]
        protected int               _rate;
        protected int               _quantity;
        protected Stock<T>          _stock;
        protected bool              starverd;
        protected FloatReference    range;

        public void Produce()
        {
            List<T> addToStock = new List<T>();
            for (int i = 0; i < _rate; ++i)
            {
                addToStock.Add(type.Create());
            }
            _stock.Put(addToStock);
        }

        public List<T> Unload(int sub_quantity)
        {
            return _stock.Remove(sub_quantity);
        }

        public Stock<T> Stock
        {
            get { return _stock; }
        }

        public int Rate
        {
            get { return _rate; }

            set { _rate = value; }
        }

        public bool Starverd
        {
            get { return starverd; }

            set { starverd = value; }
        }

        public int Quantity
        {
            get { return _stock.GetCount(); }

            set { _quantity = value; }
        }

        public FloatReference Range
        {
            get { return range; }

            set { range = value; }
        }
    }
}