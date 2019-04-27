using System.Collections.Generic;

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
			gameData.Inventory = new List<GameData.ItemSaveData>(Size);
			for (int x = 0; x < Size; x++)
				gameData.Inventory.Add(new GameData.ItemSaveData(Slots[x]));
		}

		private bool LoadData()
		{
			if (GameControl.Control == null)
				return (false);

			GameData gameData = GameControl.Control.GameData;

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