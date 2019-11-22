using Game.Inventory;
using Game.ResourcesManagement;
using Player;
using UnityEngine;

namespace Game.Item.Food
{
    [CreateAssetMenu(fileName = "Food", menuName = "Item/Food")]
    public class Food : ItemAbstract
    {
        private FoodProducer Producer = null;

        public override bool CanUse(Transform transform)
        {
            if (Producer && Producer.StockedResources[Resource.Satiety].Quantity <= 0)
                return false;
            return true;
        }

        public override void Use(ItemStack selfStack)
        {
            var playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
            playerController.GetPlayerStats().Resources.AddResources(Resource.Satiety, Producer.StockedResources[Resource.Satiety].Quantity);
        }

        public override GameObject Equip(Transform parent)
        {
            var ret = base.Equip(parent);
            Producer = ret.GetComponentInChildren<FoodProducer>();
            return ret;
        }
    }

}
