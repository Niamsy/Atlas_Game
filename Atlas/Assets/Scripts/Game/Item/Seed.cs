using System;
using UnityEngine;

namespace Game.Item
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
    }
}
