using System.Collections.Generic;
using UnityEngine;

namespace Game.Inventory
{
    //[CreateAssetMenu(fileName = "Inventory", menuName = "Item/Inventory", order = 1)]
    public class BaseInventory : MonoBehaviour
    {
        [SerializeField] protected List<ItemStack> Slots;
        public int Size
        {
            get { return (Slots.Count); }
        }
        
        #region Initialisation / Destruction
        /// <summary>
        /// ""Constructor""
        /// </summary>
        private void Awake()
        {
            InitialiseInventory();
        }

        protected void InitMapWithSize(int size)
        {
            Slots = new List<ItemStack>();
            Slots.Capacity = size;
            for (int x = 0; x < size; x++)
                Slots.Add(new ItemStack());
        }
        protected virtual void InitialiseInventory() {}
        #endregion

        public ItemStack this[int index]
        {
            get { return (GetItem(index)); }
            set { SetItem(index, value); }
        }

        public ItemStack GetItem(int index)
        {
            if (index < 0 || index > Size)
                return (null);
            return (Slots[index]);
        }
        
        public void SetItem(int index, ItemStack itemStack)
        {
            if (index < 0 || index > Size || itemStack == null)
                return;
            Slots[index].CopyStack(itemStack);
        }
        
        public List<ItemStack> AddItemStacks(List<ItemStack> newItems)
        {
            List<ItemStack> returnList = new List<ItemStack>();

            for (int x = 0; x < newItems.Count; x += 1)
            {
                ItemStack rest;
                if ((rest = AddItemStack(newItems[x])) != null)
                    returnList.Add(rest);
            }

            return (returnList);
        }
        public ItemStack AddItemStack(ItemStack newItem)
        {
            return (newItem);
        }
    }
}
