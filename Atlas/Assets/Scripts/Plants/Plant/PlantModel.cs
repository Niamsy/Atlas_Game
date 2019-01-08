using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Plants
{
    public class PlantModel<T, U> : MonoBehaviour where T : IResource<T> where U : IResource<U>
    {
        public Plant        plant;
        public Producer<T>  producer;
        public Consumer<U>  consumer;
    }
}
