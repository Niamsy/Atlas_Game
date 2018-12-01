﻿using System;
using System.Collections.Generic;
using Game.Inventory;

namespace Game
{
	[Serializable]
	public class GameData
	{
		[Serializable]
		public class ItemSaveData
		{
			public int ID;
			public int Quantity;

			public void SetObject(ItemStack item)
			{
				ID = (item.Quantity == 0) ? (0) : (item.Content.Id);
				Quantity = item.Quantity;
			}
		}
		public List<ItemSaveData> Inventory;
		
		public ItemSaveData LeftHandItem;
		public ItemSaveData RightHandItem;
	}
}
