using System;
using Game.Inventory;
using UnityEngine;

namespace Game.Item.Tools.Bucket
{
	[Serializable,
	CreateAssetMenu(fileName = "Bucket", menuName = "Item/Tool/Bucket", order = 1)]
	public class Bucket : Tool<BucketBehaviour>
	{
		
        public override bool CanUse(Transform transform)
        {
            return (true);
        }

        public override void Use(ItemStack stack)
        {
	        Behaviour.Watering();
        }
	}
}
