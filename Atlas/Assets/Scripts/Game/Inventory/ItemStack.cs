using System;
using Game.Item;
using UnityEngine;

namespace Game.Inventory
{
    [Serializable]
    public class ItemStack
    {
        public delegate void ItemStackUpdate(ItemStack item);
        public event ItemStackUpdate OnItemStackUpdated;
        
        #region Content
        [SerializeField] private ItemAbstract _content;
        public ItemAbstract Content
        {
            get { return (_content); }
            private set { _content = value; }
        }
        #endregion
    
        #region Quantity
        [SerializeField] private int _quantity;
        public int Quantity
        {
            get { return (_quantity); }
            private set
            {
                if (_content == null || value <= 0)
                {
                    _quantity = 0;
                    _content = null;
                }
                else
                    _quantity = Mathf.Clamp(value, 0, _content.MaxStackSize);
            }
        }
        
        public bool IsEmpty
        {
            get { return(_quantity == 0); }
        }
        
        #endregion

        #region Slot content modification
        private void FireEvent()
        {
            if (OnItemStackUpdated != null)
                OnItemStackUpdated(this);
        }
        
        public void SetItem(ItemAbstract newItem, int quantity)
        {
            if (quantity == 0)
                newItem = null;
            if (newItem == null)
                quantity = 0;

            Content = newItem;
            Quantity = quantity;

            FireEvent();
        }
        
        public void CopyStack(ItemStack other)
        {
            if (other == null)
                return;
            
            Content = other.Content;
            Quantity = other.Quantity;
            
            FireEvent();
        }

        public void ModifyQuantity(int newQuatity)
        {
            Quantity = newQuatity;

            FireEvent();
        }
        
        public void EmptyStack()
        {
            Quantity = 0;
            Content = null;
        
            FireEvent();
        }

        #region Fuse/Swap
        public void SwapOrFuseStack(ItemStack other)
        {
            if (other.IsEmpty)
                return;

            if (!IsEmpty && other.Content.Id != Content.Id)
                FuseStack(other);
            else
                SwapStack(other);
        }

        public void SwapStack(ItemStack other)
        {
            ItemAbstract otherContent = other.Content;
            int otherQuantity = other.Quantity;
            
            other.SetItem(Content, Quantity);
            SetItem(otherContent, otherQuantity);
        }

        private void FuseStack(ItemStack other)
        {
            int fusedStackSize = other.Quantity + Quantity;
            int restStackItem = fusedStackSize % _content.MaxStackSize;
            
            SetItem(other.Content, fusedStackSize);
            other.ModifyQuantity(restStackItem);
        }
        #endregion
        #endregion
    }
}
