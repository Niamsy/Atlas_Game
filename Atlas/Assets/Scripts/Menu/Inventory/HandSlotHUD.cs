using System.Collections.Generic;
using Game.Inventory;
using Game.Item;
using Game.Player;
using Game.SavingSystem;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Menu.Inventory
{
    public class HandSlotHUD : MonoBehaviour
    {
        private HandSlots _handSlot = null;

        [SerializeField] private ItemStackActionHUD[] _handStackUI = null;
        [SerializeField] private Text _useText = null;
        
        #if UNITY_EDITOR
        private void Reset()
        {
            if (Application.isEditor)
                _handStackUI = GetComponentsInChildren<ItemStackActionHUD>();
        }
        #endif
        
        private void Awake()
        {
            _handSlot = FindObjectOfType<HandSlots>();
            #if ATLAS_DEBUG
            if (_handStackUI.Length != HandSlots.NbOfItemSlots)
                Debug.LogError("Not enougth item in list 'HandStackUI' for " + name + "." + "Actually '" + _handStackUI.Length + "' against expected '" + HandSlots.NbOfItemSlots + "'");
            #endif
            _handSlot.OnSelectSlotChanged += OnSelectSlotChanged;
        }

        private void OnSelectSlotChanged(int idx)
        {
            for (int x = 0; x < HandSlots.NbOfItemSlots; x++)
            {
                _handStackUI[x].Select(x == idx);
            }
        }
        
        private void Start()
        {
            InputControls.PlayerActions playerControls = SaveManager.Instance.InputControls.Player;
            InputAction[] actions = {playerControls.SelectField1, playerControls.SelectField2, playerControls.SelectField3, playerControls.SelectField4, playerControls.SelectField5, playerControls.SelectField6, playerControls.SelectField7, playerControls.SelectField8};
            
            for (int x = 0; x < HandSlots.NbOfItemSlots; x++)
            {
                _handStackUI[x].SetItemStack(_handSlot[x]);
                _handStackUI[x].SetAction(actions[x]);
            }
        }

        private void Update()
        {
            if (_handSlot.SelectedItem && _handSlot.SelectedItem.CanUse(_handSlot.transform))
                _useText.text = _handSlot.SelectedItem.UsageText;
            else
                _useText.text = "";
        }
        
        public int CountItems(ItemAbstract item)
        {
            var total = 0;

            for (var i = 0; i < _handSlot.Size; i++)
            {
                var itemStack = _handSlot[i];
                if (itemStack.Content && itemStack.Content.Id == item.Id)
                    total += itemStack.Quantity;
            }

            return total;
        }
        
        public bool HasEnoughItems(ItemAbstract item, int quantity)
        {
            return CountItems(item) >= quantity;
        }
        
        public int DestroyFirsts(ItemAbstract itemToDestroy, int toRemove)
        {
            var stacksToEmpty = new List<ItemStack>();
            var total = 0;
            
            for (var i = 0; i < _handSlot.Size; i++)
            {
                var itemStack = _handSlot[i];
                if (itemStack.IsEmpty == false && itemStack.Content.Id == itemToDestroy.Id)
                {
                    if (itemStack.Quantity - toRemove <= 0)
                    {
                        toRemove -= itemStack.Quantity;
                        total += itemStack.Quantity;
                        stacksToEmpty.Add(itemStack);
                    }
                    else
                    {
                        itemStack.ModifyQuantity(itemStack.Quantity - toRemove);
                        total += toRemove;
                        toRemove = 0;
                    }
                }

                if (toRemove <= 0) break;
            }
            
            stacksToEmpty.ForEach(stack => stack.EmptyStack());
            return total;
        }
    }
}
