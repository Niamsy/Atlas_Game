﻿using UnityEngine;
using Game.Map;
using Game.Inventory;


namespace Game.Item
{
    public class ItemDropped : ItemPickable
    {
        [SerializeField] public ItemAbstract item;

        protected override void Awake()
        {
            base.Awake();
            var bhvior = gameObject.GetComponent<ItemStackBehaviour>();
            if (bhvior != null)
            {
                item = bhvior.Slot.Content;
            }
            LevelManager.DroppedItemManager.AddItemDropped(this);
        }

        private void OnDestroy()
        {
            if (LevelManager.DroppedItemManager != null)
                LevelManager.DroppedItemManager.RemoveItemDropped(this);
        }
    }
}
