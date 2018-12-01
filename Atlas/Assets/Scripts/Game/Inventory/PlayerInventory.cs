using Game.Item;
using UnityEngine;

namespace Game.Inventory
{
	public class PlayerInventory : BaseInventory
	{
		private readonly int _inventorySize = 84;
		
		protected override void InitialiseInventory()
		{
			if (!LoadData())
				InitMapWithSize(_inventorySize);
		}

		private void OnDisable()
		{
			SaveData();
		}
		
		#region Save/Load

		private bool SaveData()
		{
			if (GameControl.control == null)
				return (false);
			
			GameData gameData = GameControl.control.gameData;
			for (int x = 0; x < Size; x++)
				gameData.Inventory[x].SetObject(Slots[x]);
			return (true);
		}

		private bool LoadData()
		{
			if (GameControl.control == null)
				return (false);
			
			GameData gameData = GameControl.control.gameData;
			
			InitMapWithSize(gameData.Inventory.Count);

			for (int x = 0; x < Size; x++)
			{

				var savedItem = gameData.Inventory[x];

				Slots[x].SetFromGameData(savedItem);				
			}
			return (true);
		}
		#endregion
	}
}
