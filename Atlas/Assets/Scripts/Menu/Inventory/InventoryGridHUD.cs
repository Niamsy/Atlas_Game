using System.Collections.Generic;
using Game.Inventory;
using UnityEngine;

namespace Menu.Inventory
{
    public class InventoryGridHUD : MonoBehaviour
    {
        [SerializeField] private GameObject _slotPrefab;
        [SerializeField] private BaseInventory _actualBaseInventory;
        private List<ItemStackHUD> _slots = new List<ItemStackHUD>();

        private void OnEnable()
        {
            if (_actualBaseInventory != null)
                LoadThisInventory(_actualBaseInventory);
        }

        public void LoadThisInventory(BaseInventory inventoryToLoad)
        {
            var newSize = 0;
            if (inventoryToLoad != null)
                newSize = inventoryToLoad.Size;
            var oldSize = _slots.Count;
            var modif = newSize - oldSize;

            for (int x = 0; x < newSize; x++)
            {
                ItemStackHUD itemStackHUD;
                if (x >= oldSize)
                    itemStackHUD = CreateNewSlot();
                else
                    itemStackHUD = _slots[x];
                itemStackHUD.SetItemStack(inventoryToLoad[x]);
            }             

            if (modif < 0)
            {
                for (int x = 0; x < -modif; x++)
                    Destroy(_slots[x].gameObject);
                _slots.RemoveRange(oldSize, modif);
            }
        }

        private ItemStackHUD CreateNewSlot()
        {
            var go = Instantiate(_slotPrefab, transform);
            go.SetActive(true);
            var itemStackHUD = go.GetComponent<ItemStackHUD>();
            _slots.Add(itemStackHUD);
            return (itemStackHUD);
        }
    }
}
