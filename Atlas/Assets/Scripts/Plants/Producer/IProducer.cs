using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Variables;

namespace Plants
{
    public interface IProducer
    {
        Resources Type { get; }
        int Rate { get; }
        int Quantity { get; }
        Stock Stocks { get; }
        bool Starverd { get; }
        FloatReference Range { get; }

        void Produce();

        List<Resources> Unload(int sub_quantity);
    }
}
