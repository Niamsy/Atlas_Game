using Game.Inventory;
using Game.Player;
using UnityEngine;
using UnityEngine.UI;

namespace Menu.Inventory
{
    public class HandSlotHUD : MonoBehaviour
    {
        private HandSlots _handSlot;

        [SerializeField] private ItemStackHUD _handStackUI;
        [SerializeField] private Text _useText;
        
        private void Awake()
        {
            _handSlot = FindObjectOfType<HandSlots>();
        }
        
        private void Start()
        {
            _handStackUI.SetItemStack(_handSlot.EquippedItemStack);
        }

        private void Update()
        {
            if (_handSlot.EquippedItem && _handSlot.ObjectIsUsable)
                _useText.text = _handSlot.EquippedItem.UsageText;
            else
                _useText.text = "";
        }
    }
}
