using System;
using Game.Inventory;
using Game.ResourcesManagement;
using InputManagement;
using UnityEngine;

namespace Game.Item.Tools.Bucket
{
	[Serializable,
	CreateAssetMenu(fileName = "Bucket", menuName = "Item/Tool/Bucket", order = 1)]
	public class BucketItem : Tool<BucketBehaviour>
	{
        public override bool CanUse(Transform transform)
        {
            var canUse = Behaviour.Stock[Resource.Water].Quantity > 0;
	        return (canUse);
        }

        public override void Use(ItemStack selfStack)
        {
            Behaviour.SetState(true);
            //if (status == InputKeyStatus.Holded)
            // Behaviour.SetState(true);
            //if (status == InputKeyStatus.Released)
            // Behaviour.SetState(false);
        }

        public override bool CancelUse(ItemStack selfStack)
        {
            Behaviour.SetState(false);
            return true;
        }
    }
}
