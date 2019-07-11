using Game.Inventory;
using Game.Item;
using Game.Item.PlantSeed;
using Game.SavingSystem;
using Game.SavingSystem.Datas;
using InputManagement;
using Player;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Player
{
    [RequireComponent(typeof(BaseInventory))]
    public class HandSlots : MapSavingBehaviour
    {
        public bool IsObjectUsable => (EquippedItem != null && EquippedItemStack.Quantity > 0) && _equippedItem.CanUse(_handTransform);

        [SerializeField] private ItemStack _equippedItemStack = null;
        [SerializeField] private Transform _handTransform = null;

        private ItemAbstract               _equippedItem = null;
        private GameObject                 _equippedItemInstance = null;
        
        public ItemStack                   EquippedItemStack => _equippedItemStack;
        public ItemAbstract                EquippedItem => _equippedItem;

        protected override void Awake()
        {
            base.Awake();

            _equippedItemStack.OnItemStackUpdated += OnEquippedUpdate;
        }

        public void UseItem()
        {
            _equippedItem.Use(EquippedItemStack);
        }

        public bool CancelUse()
        {
           return _equippedItem.CancelUse(EquippedItemStack);
        }

        #region Load/Saving Methods
        protected override void SavingMapData(MapData data)
        {
            if (data.EquippedHand != null)
                data.EquippedHand.SetObject(_equippedItemStack);
        }
        
        protected override void LoadingMapData(MapData data)
        {
            EquippedItemStack.SetFromGameData(data.EquippedHand);
        }
        #endregion
        
        public void Equip(bool left, ItemStack newItem)
        {
            EquippedItemStack.SwapStack(newItem);
        }

        private void OnEquippedUpdate(ItemStack updated)
        {
            UpdateHand(ref _equippedItemInstance, updated, _handTransform);
        }

        public void Drop()
        {
            if (EquippedItem == null)
                return;
            GameObject droppedObject = Instantiate(EquippedItem.PrefabDroppedGO);
            droppedObject.transform.position = transform.position + transform.forward + Vector3.up;
            var itemStackB = droppedObject.GetComponent<ItemStackBehaviour>();
            itemStackB.Slot.SetItem(EquippedItemStack.Content, EquippedItemStack.Quantity);
            EquippedItemStack.EmptyStack();
        }

        private void UpdateHand(ref GameObject itemGo, ItemStack item, Transform position)
        {
            if (_equippedItem != null)
                _equippedItem.UnEquip();

            _equippedItem = null;

            if (item.IsEmpty)
                return;
            
            _equippedItem = item.Content;
            itemGo = _equippedItem.Equip(position);
        }
    }
}
