using System;
using System.Collections.Generic;
using Game.DayNight;

namespace Game.SavingSystem
{
    [Serializable]
	public class GameData
	{
        public DateTime						LastGetScannedPlant;
		public List<ItemBaseData>			Inventory;
		public ItemBaseData					EquippedHand;
        public TransformSaveData			TransformData;
        public Date							CalendarData;
	}
}
