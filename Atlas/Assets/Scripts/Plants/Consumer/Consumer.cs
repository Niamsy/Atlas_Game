using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Variables;

namespace Plants
{
    public class Consumer<T> : MonoBehaviour where T : IResource<T>
    {
        public  IResource<T>        type;
        [SerializeField]
        protected int               _rate;
        protected int               quantity;
        protected Stock<T>          _stock;
        protected int               starvationTimeLimit;
        protected bool              starverd;
        protected FloatReference    range;

        public void Consume()
        {
            _stock.Remove(_rate);
        }

        public void Load(Producer<T> producer, int quantity)
        {
            _stock.Put(producer.Unload(quantity));
        }

        public Stock<T> Stock
        {
            get { return _stock; } 
        }

        public int Rate
        {
            get { return _rate; }
        }
    }
}