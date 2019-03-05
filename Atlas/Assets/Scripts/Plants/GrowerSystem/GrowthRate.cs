using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Plants.GrowerSystem
{
    [CreateAssetMenu(menuName = "Plant System/Growth Rate")]
    public class GrowthRate : ScriptableObject
    {
        [Header("Production rates")]
        public float ProducerScanRate;
        public int QuantityTaken;
        [Header("Consumption rates")]
        public float ConsumationRate;
        public int QuantityConsumed;
    }
}