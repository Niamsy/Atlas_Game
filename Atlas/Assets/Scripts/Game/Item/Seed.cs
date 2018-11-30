using UnityEngine;

namespace Game.Item
{
    [CreateAssetMenu(fileName = "Seed", menuName = "Item/Seed", order = 1)]
    public class Seed : ItemAbstract
    {
        public override void Use()
        {
            // ToDo: Plant Seed
        }

        [SerializeField] private GameObject _prefabPlanted;
        public GameObject PrefabPlanted
        {
            get { return (_prefabPlanted); }
        }
    }
}
