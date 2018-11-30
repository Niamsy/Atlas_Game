using Game.Inventory;
using UnityEngine;

namespace Game.Player
{
    [RequireComponent(typeof(InventoryBehaviour))]
    public class HandSlots : MonoBehaviour
    {
        private InventoryBehaviour _inventory;
        
        public ItemStack LeftHandItem;
        public ItemStack RightHandItem;

        private void Awake()
        {
            _inventory = GetComponent<InventoryBehaviour>();
        }
        
        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
                Equip(true, _inventory[0]);
            if (Input.GetKeyDown(KeyCode.E))
                Equip(false, _inventory[1]);
        }
        
        public void Equip(bool left, ItemStack newItem)
        {
            if (left)
                LeftHandItem.SwapStack(newItem);
            else
                RightHandItem.SwapStack(newItem);
            
            // ToDo: Display model in game in the hand
        }
    }
}
