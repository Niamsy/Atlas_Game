using Game.Player;
using UnityEngine;
using UnityEngine.UI;

namespace Menu.Inventory
{
    public class HandSlotHUD : MonoBehaviour
    {
        private HandSlots _handSlot = null;

        [SerializeField] private ItemStackHUD _handStackUI = null;
        [SerializeField] private Text _useText = null;
        
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
            if (_handSlot.EquippedItem && _handSlot.IsObjectUsable)
                _useText.text = _handSlot.EquippedItem.UsageText;
            else
                _useText.text = "";
        }
    }
}
