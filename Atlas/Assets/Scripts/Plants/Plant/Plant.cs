using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Item;

namespace Plants
{
    public class Plant : ItemAbstract
    {
        public PlantDataModel   data;
        public Soil             soil;

        public override void Use()
        {
        }
    }
}

