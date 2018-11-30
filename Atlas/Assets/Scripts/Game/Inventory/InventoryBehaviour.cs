using System.Collections.Generic;
using UnityEngine;

namespace Game.Inventory
{
    //[CreateAssetMenu(fileName = "Inventory", menuName = "Item/Inventory", order = 1)]
    public class InventoryBehaviour : MonoBehaviour
    {
        [SerializeField] private List<ItemStack>      _slots;
        public int Size
        {
            get { return (_slots.Count); }
        }
        
        #region Initialisation / Destruction
        /// <summary>
        /// ""Constructor""
        /// </summary>
        private void Awake()
        {
            for (int x = 0; x < Size; x++)
                _slots[x] = _slots[x]??(new ItemStack());
        }
        /// <summary>
        /// ""Destructor""
        /// </summary>
        private void OnDestroy() {}
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
            return (_slots[index]);
        }
        
        public void SetItem(int index, ItemStack itemStack)
        {
            if (index < 0 || index > Size || itemStack == null)
                return;
            _slots[index].CopyStack(itemStack);
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
