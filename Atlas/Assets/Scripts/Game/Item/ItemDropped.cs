using UnityEngine;
using Game.Map;


namespace Game.Item
{
    public class ItemDropped : ItemPickable
    {
        [SerializeField] public ItemAbstract item;

        protected override void Awake()
        {
            base.Awake();
            LevelManager.DroppedItemManager.AddItemDropped(this);
        }

        private void OnDestroy()
        {

            if (LevelManager.DroppedItemManager != null)
                LevelManager.DroppedItemManager.RemoveItemDropped(this);
        }
    }
}
