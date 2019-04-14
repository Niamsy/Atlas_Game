using System;
using Game.Inventory;
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
            return (true);
        }

        public override void Use(ItemStack selfStack, InputKeyStatus status)
        {
	        if (status == InputKeyStatus.Holded)
		        Behaviour.Watering();
	        if (status == InputKeyStatus.Released)
		        Behaviour.StopWatering();
        }
	}
}
