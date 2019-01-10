using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Plants
{
    public class PlantModel : MonoBehaviour
    {
        public Plant            plant;
        public List<Producer>   producer;
        public List<Consumer>   consumer;
    }
}
