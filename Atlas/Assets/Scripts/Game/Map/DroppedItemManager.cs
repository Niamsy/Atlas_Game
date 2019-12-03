using System.Collections.Generic;
using Game.Inventory;
using Game.Item;
using Game.SavingSystem;
using Game.SavingSystem.Datas;
using UnityEngine;
using Game.Questing;

namespace Game.Map
{
    public class DroppedItemManager : MapSavingBehaviour
    {
        private List<ItemDropped> _itemsDropped = new List<ItemDropped>();
        [SerializeField] private ConditionEvent _conditionEvent;
        [SerializeField] private Condition _raisedCondition;


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
                    if (itemStackB)
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
            if (itemDropped.item == null)
            {
                Debug.LogWarning("Unset Item Base in dropped item " + itemDropped.name);
            } else
            {
                int qte = itemDropped.BaseStack.Slot.Quantity;                
                _conditionEvent.Raise(_raisedCondition, itemDropped.item, (qte == 0) ? 1 : qte);
            }
            _itemsDropped.Remove(itemDropped);
        }
        public void AddItemDropped(ItemDropped itemDropped)
        {
            _itemsDropped.Add(itemDropped);
        }
    }
}
