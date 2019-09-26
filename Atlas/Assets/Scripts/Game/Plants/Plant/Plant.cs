using Game.Item;
using Game.Inventory;
using UnityEngine;

namespace Plants.Plant
{
    [CreateAssetMenu(menuName = "Plant System/PlantItem")]
    public class PlantItem : ItemAbstract
    {
        protected bool isSowed = false;

        public override void Use(ItemStack selfStack)
        {
            // TODO: USE
        }

        public bool IsSowed
        {
            get { return isSowed; }
        }

        public void Sow()
        {
            isSowed = true;
        }

        public override bool CanUse(Transform transform)
        {
            // TODO: Check to use
            return false;
        }
    }
}