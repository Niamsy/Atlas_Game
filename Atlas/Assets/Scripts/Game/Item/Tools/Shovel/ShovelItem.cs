using System;
using Game.Inventory;
using InputManagement;
using Plants;
using UnityEngine;

namespace Game.Item.Tools
{
	[Serializable,
	CreateAssetMenu(fileName = "Shovel", menuName = "Item/Tool/Shovel", order = 1)]
	public class ShovelItem : Tool<ShovelBehaviour>
	{
        public override bool CanUse(Transform transform)
        {
            return (true);
        }

        public override void Use(ItemStack selfStack)
		{
			// ToDo: Dig
		}
	}
}
