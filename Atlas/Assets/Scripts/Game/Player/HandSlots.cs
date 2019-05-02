using Game.Inventory;
using Game.Item;
using Game.Item.PlantSeed;
using InputManagement;
using Player;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Player
{
    [RequireComponent(typeof(BaseInventory))]
    public class HandSlots : MonoBehaviour
    {
        private bool _objectIsUsableLast;
        public bool ObjectIsUsable { get { return (_objectIsUsableLast && EquippedItem != null && EquippedItemStack.Quantity > 0); } }
        private PlayerController _controller;

        [SerializeField] private ItemStack _equippedItemStack;
        [SerializeField] private Transform _handTransform;

        private ItemAbstract               _equippedItem;
        private GameObject                 _equippedItemInstance;
        
        public ItemStack                   EquippedItemStack { get { return (_equippedItemStack); } }
        public ItemAbstract                EquippedItem { get { return (_equippedItem); } }
      
        private void Awake()
        {
            _equippedItemStack.OnItemStackUpdated += OnEquippedUpdate;

            _controller = FindObjectOfType<PlayerController>();
            LoadData();

            GameControl.BeforeSavingPlayer += SaveData;
        }

        private void OnDestroy()
        {
            GameControl.BeforeSavingPlayer -= SaveData;
        }
        

        public bool CheckIfItemUsable()
        {
            return (_objectIsUsableLast = _equippedItem.CanUse(_handTransform));
        }

        public void UseItem(InputKeyStatus status)
        {
            _equippedItem.Use(EquippedItemStack, status);
            if (_equippedItem.Animation != null)
            {
                _controller.PlayAnimation(_equippedItem.Animation);
            }
        }

        #region Load/Saving Methods
        private void SaveData(GameControl gameControl)
        {
            GameData gameData = gameControl.GameData;
            if (gameData.EquippedHand != null)
                gameData.EquippedHand.SetObject(_equippedItemStack);
        }

        private void LoadData()
        {
            if (GameControl.Control == null)
                return;
            
            GameData gameData = GameControl.Control.GameData;
            EquippedItemStack.SetFromGameData(gameData.EquippedHand);
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
