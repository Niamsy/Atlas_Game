using System.Collections.Generic;
using Game.Inventory;
using UnityEngine;

namespace Menu.Inventory
{
    public class InventoryGridHUD : MonoBehaviour
    {
        [SerializeField] private BaseInventory _actualBaseInventory;
        private List<ItemStackHUD> _slots = new List<ItemStackHUD>();

        private void OnEnable()
        {
            _slots = new List<ItemStackHUD>(GetComponentsInChildren<ItemStackHUD>(true));
            if (_actualBaseInventory != null)
                LoadThisInventory(_actualBaseInventory);
        }

        public void LoadThisInventory(BaseInventory inventoryToLoad)
        {
            var newSize = 0;
            if (inventoryToLoad != null)
                newSize = inventoryToLoad.Size;
            var oldSize = _slots.Count;

            for (int x = 0; x < oldSize; x++)
            {
                ItemStackHUD slot = _slots[x];
                slot.gameObject.SetActive(x < newSize);
                if (x < newSize)
                    slot.SetItemStack(inventoryToLoad[x], _actualBaseInventory.Drop);
            }
        }
    }
}
