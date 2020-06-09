using System.Collections.Generic;
using Game.SavingSystem;
using Game.SavingSystem.Datas;

namespace Game.Inventory
{
    public class PlayerInventory : BaseInventory
	{
		private readonly int _inventorySize = 64;

        protected void Awake()
		{
			InitMapWithSize(_inventorySize);
			SaveManager.BeforeSavingMapData += SaveData;
			SaveManager.UponLoadingMapData += LoadData;
		}

		protected void OnDestroy()
		{
			SaveManager.BeforeSavingMapData -= SaveData;
			SaveManager.UponLoadingMapData -= LoadData;
		}
		
		#region Load/Saving Methods
		
		private void SaveData(MapData data)
		{
			data.Inventory = new List<ItemBaseData>(Size);
			for (int x = 0; x < Size; x++)
				data.Inventory.Add(new ItemBaseData(Slots[x]));
		}

		private void LoadData(MapData data)
		{
			
			InitMapWithSize(_inventorySize);
			if (data.Inventory != null)
			for (int x = 0; x < Size && x < data.Inventory.Count; x++)
			{
				var savedItem = data.Inventory[x];
				Slots[x].SetFromGameData(savedItem);
			}
		}
		#endregion
	}
}