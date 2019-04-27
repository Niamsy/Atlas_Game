using System;
using System.Collections.Generic;
using Game.Inventory;

namespace Game
{
    [Serializable]
	public class GameData : SaveData
	{
		[Serializable]
		public class ItemSaveData
		{
			public int ID;
			public int Quantity;

			public ItemSaveData(ItemStack item)
			{
				ID = (item.Quantity == 0) ? (0) : (item.Content.Id);
				Quantity = item.Quantity;
			}

			public void SetObject(ItemStack item)
			{
				ID = (item.Quantity == 0) ? (0) : (item.Content.Id);
				Quantity = item.Quantity;
			}
		}
		
        public DateTime						LastGetScannedPlant;
		public List<ItemSaveData>			Inventory;
		public ItemSaveData					EquippedHand;
        public TransformSaveData			TransformData;
        public DayNightSaver.CalendarData	CalendarData;
	}
}
