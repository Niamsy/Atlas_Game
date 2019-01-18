using System;
using Plants.Plant;
using UnityEngine;

namespace Game.Item.PlantSeed
{
    [Serializable,
     CreateAssetMenu(fileName = "Seed", menuName = "Item/Seed", order = 1)]
    public class Seed : ItemAbstract
    {
        public override void Use()
        {
            // ToDo: Plant Seed
        }

        [Header("Seed variables")]
        [SerializeField] private GameObject _prefabPlanted;
        public GameObject PrefabPlanted
        {
            get { return (_prefabPlanted); }
        }
        
        [SerializeField] private PlantStatistics _plantStatistics;
        public PlantStatistics PlantStatistics
        {
            get { return (_plantStatistics); }
        }
        
    }
}
