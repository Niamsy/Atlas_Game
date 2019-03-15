using AtlasAudio;
using AtlasEvents;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Inventory
{
    public class BaseInventory : MonoBehaviour
    {
        [SerializeField] protected List<ItemStack> Slots;

        [Header("Audio")] public Audio OnDropItemAudio;
        public AudioEvent OnDropItemEvent;

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
            InitializeInventory();
        }

        protected void InitMapWithSize(int size)
        {
            Slots = new List<ItemStack>();
            Slots.Capacity = size;
            for (int x = 0; x < size; x++)
                Slots.Add(new ItemStack());
        }
        protected virtual void InitializeInventory() {}
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
            foreach (ItemStack itemStack in Slots)
            {
                if (itemStack.FuseStack(newItem) && newItem.IsEmpty)
                    return (null);
            }

            foreach (ItemStack itemStack in Slots)
            {
                if (itemStack.IsEmpty)
                {
                    itemStack.SwapStack(newItem);
                    return (null);
                }
            }
            return (newItem);
        }
        
        public void Drop(ItemStack stack)
        {
            if (stack.IsEmpty)
                return;
            
            if (stack.Content.GetType() == typeof(Plants.Plant.PlantItem))
            {
                Plants.Plant.PlantItem item = stack.Content as Plants.Plant.PlantItem;
                item.Sow();
            }
            else
            {
                GameObject droppedObject = Instantiate(stack.Content.PrefabDroppedGO);
                droppedObject.transform.position = transform.position + transform.forward + Vector3.up;
                var itemStackB = droppedObject.GetComponent<ItemStackBehaviour>();
                itemStackB.Slot.SetItem(stack.Content, stack.Quantity);
                stack.EmptyStack();
                if (OnDropItemAudio && OnDropItemEvent)
                    OnDropItemEvent.Raise(OnDropItemAudio, null);
            }
            Debug.Log("Drop ActualStack " + stack);
        }
    }
}
