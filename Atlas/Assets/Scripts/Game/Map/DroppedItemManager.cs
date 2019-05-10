using System.Collections.Generic;
using Game.Inventory;
using Game.Item;
using Game.SavingSystem;
using UnityEngine;

namespace Game.Map
{
    public class DroppedItemManager : MonoBehaviour
    {
        private List<ItemDropped> _itemsDropped = new List<ItemDropped>();

        private void Awake()
        {
            GameControl.UponLoadingMapData += LoadingItems;
            GameControl.BeforeSavingData += SavingItems;
        }

        private void OnDestroy()
        {
            GameControl.UponLoadingMapData -= LoadingItems;
            GameControl.BeforeSavingData -= SavingItems;
        }
        
        #region Load/Save
        private void LoadingItems(GameControl gameControl)
        {
            if (gameControl.MapData.DroppedItems == null)
                return;
    
            foreach (var itemDropped in _itemsDropped)
                Destroy(itemDropped.gameObject);
            foreach (var itemDroppedsData in gameControl.MapData.DroppedItems)
            {
                var itemAbstract = ItemFactory.GetItemForId(itemDroppedsData.ID);
                if (itemAbstract)
                {
                    var trans = itemDroppedsData.WorldPos;
                    GameObject droppedObject = Instantiate(itemAbstract.PrefabDroppedGO, trans.Position.Value, trans.Rotation.Value);
                    var itemStackB = droppedObject.GetComponent<ItemStackBehaviour>();
                    itemStackB.Slot.SetItem(itemAbstract, itemDroppedsData.Quantity);
                }
            }
        }

        private void SavingItems(GameControl gameControl)
        {
            ItemDroppedsData[] droppedItem = new ItemDroppedsData[_itemsDropped.Count];
            for (int x = 0; x < _itemsDropped.Count; x++)
                droppedItem[x] = new ItemDroppedsData(_itemsDropped[x]);
            gameControl.MapData.DroppedItems = droppedItem;
        }
        #endregion

        public void RemoveItemDropped(ItemDropped itemDropped)
        {
            _itemsDropped.Remove(itemDropped);
        }
        public void AddItemDropped(ItemDropped itemDropped)
        {
            _itemsDropped.Add(itemDropped);
        }
    }
}
