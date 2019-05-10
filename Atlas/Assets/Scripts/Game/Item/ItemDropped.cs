using Game.Map;

namespace Game.Item
{
    public class ItemDropped : ItemPickable
    {
        protected override void Awake()
        {
            base.Awake();
            MapManager.DroppedItemManager.AddItemDropped(this);
        }

        private void OnDestroy()
        {
            if (MapManager.DroppedItemManager != null)
                MapManager.DroppedItemManager.RemoveItemDropped(this);
        }
    }
}
