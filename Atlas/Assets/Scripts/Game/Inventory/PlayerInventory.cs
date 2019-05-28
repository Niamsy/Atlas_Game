using System.Collections.Generic;
using Game.SavingSystem;

namespace Game.Inventory
{
    public class PlayerInventory : BaseInventory
	{
		private readonly int _inventorySize = 84;

        protected override void InitializeInventory()
		{
            if (!LoadData())
                InitMapWithSize(_inventorySize);
		     GameControl.BeforeSavingPlayer += SaveData;
		}

		protected override void DestroyInventory()
		{
			GameControl.BeforeSavingPlayer -= SaveData;
		}
		
		#region Load/Saving Methods
		
		private void SaveData(GameControl gameControl)
		{
			GameData gameData = gameControl.GameData;
			gameData.Inventory = new List<ItemBaseData>(Size);
			for (int x = 0; x < Size; x++)
				gameData.Inventory.Add(new ItemBaseData(Slots[x]));
		}

		private bool LoadData()
		{
			if (GameControl.Instance == null)
				return (false);

			GameData gameData = GameControl.Instance.GameData;

			InitMapWithSize(_inventorySize);

			for (int x = 0; x < Size && x < gameData.Inventory.Count; x++)
			{
				var savedItem = gameData.Inventory[x];
				Slots[x].SetFromGameData(savedItem);
			}

			return (true);
		}
		#endregion
	}
}