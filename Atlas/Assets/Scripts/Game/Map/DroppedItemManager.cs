using System.Collections.Generic;
using Game.Inventory;
using Game.Item;
using Game.SavingSystem;
using Game.SavingSystem.Datas;
using UnityEngine;

namespace Game.Map
{
    public class DroppedItemManager : MapSavingBehaviour
    {
        private List<ItemDropped> _itemsDropped = new List<ItemDropped>();
        
        #region Load/Save
        protected override void LoadingMapData(MapData data)
        {
            if (data.DroppedItems == null)
                return;
    
            foreach (var itemDropped in _itemsDropped)
                Destroy(itemDropped.gameObject);
            foreach (var itemDroppedsData in data.DroppedItems)
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

        protected override void SavingMapData(MapData data)
        {
            ItemDroppedsData[] droppedItem = new ItemDroppedsData[_itemsDropped.Count];
            for (int x = 0; x < _itemsDropped.Count; x++)
                droppedItem[x] = new ItemDroppedsData(_itemsDropped[x]);
            data.DroppedItems = droppedItem;
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
