using Game.Inventory;
using Game.Item;
using UnityEngine;

[CreateAssetMenu(fileName = "Produced Resource", menuName = "Item/Resource")]
public class ProducedResource : ItemAbstract
{
    public override bool CanUse(Transform transform)
    {
        return false;
    }

    public override void Use(ItemStack selfStack)
    {
        
    }
}
