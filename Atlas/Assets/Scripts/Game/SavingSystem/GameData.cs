using System;
using System.Collections.Generic;

namespace Game.SavingSystem
{
    [Serializable]
	public class GameData
	{
        public DateTime						LastGetScannedPlant;
		public List<ItemBaseData>			Inventory;
		public ItemBaseData					EquippedHand;
        public TransformSaveData			TransformData;
        public DayNightSaver.CalendarData	CalendarData;
	}
}
