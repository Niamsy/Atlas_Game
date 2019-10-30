using System.Collections.Generic;
using Game.Inventory;
using Game.Item;
using Game.SavingSystem;
using Game.SavingSystem.Datas;
using Tools;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Player
{
    [RequireComponent(typeof(BaseInventory))]
    public class HandSlots : MapSavingBehaviour
    {
        public bool                        IsObjectUsable => SelectedItem != null && SelectedItemStack.Quantity > 0 && SelectedItem.CanUse(_handTransform);

        public const int                   NbOfItemSlots = 8;
        private List<ItemStack>            _equippedItemStacks = null;
        private int                        _selectedIdx = 0;
        public ItemAbstract                SelectedItem => SelectedItemStack.Content;
        public ItemStack                   SelectedItemStack => _equippedItemStacks[_selectedIdx];
        public delegate void               SelectSlotChanged(int newIdx);
        public event SelectSlotChanged     OnSelectSlotChanged;

        private ItemAbstract               _selectedItem = null;
        
        [SerializeField] private Transform _handTransform = null;

        private GameObject                 _equippedItemInstance = null;
        private List<InputAction>          _selectShortcuts = new List<InputAction>(NbOfItemSlots);
        
        public ItemStack EquippedItemStack(int idx)
        {
            return (_equippedItemStacks[idx]);
        }

        #region Initialisation
        protected override void Awake()
        {
            base.Awake();
            _equippedItemStacks = new List<ItemStack>(NbOfItemSlots);
            for (int x = 0; x < NbOfItemSlots; x++)
                _equippedItemStacks.Add(new ItemStack());
            foreach (ItemStack stack in _equippedItemStacks)
                stack.OnItemStackUpdated += OnEquippedUpdate;

            var playerInput = SaveManager.Instance.InputControls.Player;
            _selectShortcuts.Add(playerInput.SelectField1);
            _selectShortcuts.Add(playerInput.SelectField2);
            _selectShortcuts.Add(playerInput.SelectField3);
            _selectShortcuts.Add(playerInput.SelectField4);
            _selectShortcuts.Add(playerInput.SelectField5);
            _selectShortcuts.Add(playerInput.SelectField6);
            _selectShortcuts.Add(playerInput.SelectField7);
            _selectShortcuts.Add(playerInput.SelectField8);
            foreach (InputAction act in _selectShortcuts)
                act.performed += ChangeSelectedSlot_Shortcut;
        }

        protected virtual void Start()
        {
            ChangeSelectedSlot(_selectedIdx);    
        }
        
        protected override void OnDestroy()
        {
            base.OnDestroy();
            
            foreach (InputAction act in _selectShortcuts)
                act.performed -= ChangeSelectedSlot_Shortcut;
        }
        #endregion
        
        public void ChangeSelectedSlot_Shortcut(InputAction.CallbackContext ctx)
        {
            int idx = _selectShortcuts.FindIndex(action => { return (action == ctx.action); });
            if (idx != -1)
                ChangeSelectedSlot(idx);
        }

        public void ChangeSelectedSlot(int idx)
        {
            _selectedIdx = MyMath.Mod(idx, NbOfItemSlots);
            UpdateHand();
            if (OnSelectSlotChanged != null)
                OnSelectSlotChanged(_selectedIdx);
        }
        
        public void UseItem()
        {
            if (SelectedItem != null)
                SelectedItem.Use(SelectedItemStack);
        }

        public bool CancelUse()
        {
            if (SelectedItem != null)
                return SelectedItem.CancelUse(SelectedItemStack);
            return false;
        }

        #region Load/Saving Methods
        protected override void SavingMapData(MapData data)
        {
            data.SelectedItems = _selectedIdx;
            
            if (data.EquippedItems == null)
            {
                data.EquippedItems = new List<ItemBaseData>(NbOfItemSlots);
                for (int x = 0; x < NbOfItemSlots; x++)
                    data.EquippedItems.Add(new ItemBaseData(_equippedItemStacks[x]));
            }
            else
            {
                for (int x = 0; x < NbOfItemSlots; x++)
                    data.EquippedItems[x].SetObject(_equippedItemStacks[x]);
            }
        }
        
        protected override void LoadingMapData(MapData data)
        {
            if (data.EquippedItems != null)
            {
                for (int x = 0; x < NbOfItemSlots; x++)
                {
                    if (data.EquippedItems[x] != null)
                        _equippedItemStacks[x].SetFromGameData(data.EquippedItems[x]);
                }
            }
        }
        #endregion
        

        private void OnEquippedUpdate(ItemStack updated)
        {
            UpdateHand();
        }

        public void Drop()
        {
            if (SelectedItem == null)
                return;
            GameObject droppedObject = Instantiate(SelectedItem.PrefabDroppedGO);
            droppedObject.transform.position = transform.position + transform.forward + Vector3.up;
            var itemStackB = droppedObject.GetComponent<ItemStackBehaviour>();
            itemStackB.Slot.SetItem(SelectedItem, SelectedItemStack.Quantity);
            SelectedItemStack.EmptyStack();
        }

        private void UpdateHand()
        {
            if (_selectedItem != null)
                _selectedItem.UnEquip();

            _selectedItem = SelectedItem;

            if (_selectedItem == null)
                return;
            _equippedItemInstance = _selectedItem.Equip(_handTransform);
        }
    }
}
