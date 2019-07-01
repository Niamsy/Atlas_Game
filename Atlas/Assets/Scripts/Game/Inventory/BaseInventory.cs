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
        protected void InitMapWithSize(int size)
        {
            Slots = new List<ItemStack>();
            Slots.Capacity = size;
            for (int x = 0; x < size; x++)
                Slots.Add(new ItemStack());
        }
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

            Debug.LogWarning("Rest stack");
            return (newItem);
        }

        public void Drop(ItemStack stack)
        {
            Drop(stack, transform.forward);
        }
        
        public void Drop(ItemStack stack, Vector3 dir)
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
                var position = transform.position + Vector3.up + dir.normalized;
                GameObject droppedObject = Instantiate(stack.Content.PrefabDroppedGO, position, Quaternion.identity);
                var itemStackB = droppedObject.GetComponent<ItemStackBehaviour>();
                var rb = droppedObject.GetComponent<Rigidbody>();
                rb.AddForce(dir.normalized * 0.1f);
                itemStackB.Slot.SetItem(stack.Content, stack.Quantity);
                stack.EmptyStack();
                if (OnDropItemAudio && OnDropItemEvent)
                    OnDropItemEvent.Raise(OnDropItemAudio, null);
            }
        }

        public void DropAll()
        {
            int totalObj = 0;
            foreach (var stack in Slots)
                if (!stack.IsEmpty)
                    totalObj += 1;

            if (totalObj <= 0)
                return;
            
            float anglePerObj = 360f / totalObj;
            for (int x = 0; x < Slots.Count; x++)
                Drop(Slots[x], Quaternion.Euler(0, x * anglePerObj, 0) * transform.forward);
        }

        public int CountItems(Item.ItemAbstract item)
        {
            int total = 0;

            foreach (ItemStack itemStack in Slots)
            {
                if (itemStack.Content.Id == item.Id)
                {
                    total += itemStack.Quantity;
                }
            }

            return total;
        }

        // Be sure to check the presence of required items with CountItems before
        // Destroy the first items encountered in the inventory until the required quantity 
        // is reached or all items are destroyed
        public void DestroyFirsts(Item.ItemAbstract itemToDestroy, int quantity)
        {
            foreach (ItemStack itemStack in Slots)
            {
                if (itemStack.Content.Id == itemToDestroy.Id)
                {
                    if (itemStack.Quantity <= quantity)
                    {
                        quantity -= itemStack.Quantity;
                        itemStack.EmptyStack();
                    }
                    else
                    {
                        itemStack.ModifyQuantity(itemStack.Quantity - quantity);
                        quantity = 0;
                    }
                }

                if (quantity <= 0) break;
            }
        }
    }
}
