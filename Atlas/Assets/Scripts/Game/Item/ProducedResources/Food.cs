using Game.Inventory;
using Game.ResourcesManagement;
using UnityEngine;

namespace Game.Item.Food
{
    [RequireComponent(typeof(ResourcesStock))]
    public class Food : ItemAbstract
    {
        public ResourcesStock Stock;
        public FoodProducer Producer;
        private bool _isFeeding = false;

        private void Awake()
        {
            Producer.gameObject.SetActive(false);
            Producer.StockedResources = Stock;

#if UNITY_EDITOR
            if (Stock[Resource.Satiety] == null)
                Debug.LogError("Food item doesn't have any satiety stock");
#endif
        }

        private void Update()
        {
            if (_isFeeding && Producer.StockedResources[Resource.Satiety].Quantity == 0)
                Producer.gameObject.SetActive(false);
        }

        public override bool CanUse(Transform transform)
        {
            if (Stock[Resource.Satiety].Quantity <= 0)
                return false;
            return true;
        }

        public override void Use(ItemStack selfStack)
        {
            Producer.gameObject.SetActive(true);
        }

        public override bool CancelUse(ItemStack selfStack)
        {
            Producer.gameObject.SetActive(false);
            return true;
        }
    }

}
