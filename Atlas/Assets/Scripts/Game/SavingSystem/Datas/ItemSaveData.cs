using System;
using Game.Inventory;
using Game.Item;
using Game.SavingSystem.Datas;

namespace Game.SavingSystem
{
    [Serializable]
    public class ItemBaseData
    {
        public int ID;
        public int Quantity;
        
        public ItemBaseData(ItemStack item)
        {
            ID = (item.Quantity == 0) ? (0) : (item.Content.Id);
            Quantity = item.Quantity;
        }

        public void SetObject(ItemStack item)
        {
            ID = (item.Quantity == 0) ? (0) : (item.Content.Id);
            Quantity = item.Quantity;
        }
    }

    [Serializable]
    public class ItemDroppedsData : ItemBaseData
    {
        public TransformSaveData    WorldPos;

        public ItemDroppedsData(ItemDropped item) : base(item.BaseStack.Slot)
        {
            WorldPos = new TransformSaveData(item.transform);
        }
    }
}