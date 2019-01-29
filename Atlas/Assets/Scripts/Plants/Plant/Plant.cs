using Game.Item;
using Plants.Plant;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Plants.Plant
{
    [CreateAssetMenu(menuName = "Plant System/PlantItem")]
    public class PlantItem : ItemAbstract
    {
        public PlantStatistics Statistics;
        protected bool isSowed = false;

        public override void Use()
        {
        }

        public bool IsSowed
        {
            get { return isSowed; }
        }

        public void Sow()
        {
            isSowed = true;
        }
    }
}