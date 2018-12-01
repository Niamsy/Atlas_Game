using System;
using UnityEngine;

namespace Game.Item.Tools
{
	[Serializable,
	CreateAssetMenu(fileName = "Shovel", menuName = "Item/Tool/Shovel", order = 1)]
	public class Shovel : Tool
	{
		public override void Use()
		{
			// ToDo: Dig
		}
	}
}
