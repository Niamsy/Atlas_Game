using System.Collections.Generic;
using Game.Item.PlantSeed;
using UnityEngine;

namespace Plants.Plant
{
    public class PlantModel : MonoBehaviour
    {
        
        
        public PlantStatistics   Statistics;
        
        public SoilType          ActualSoil;
        
        public List<Producer>    Producer;
        public List<Consumer>    Consumer;
    }
}
