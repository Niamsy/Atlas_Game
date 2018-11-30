using Game.Player;
using UnityEngine;

namespace Menu.Inventory
{
    public class HandSlotHUD : MonoBehaviour
    {
        [SerializeField] private HandSlots _handSlot;
        [SerializeField] private ItemStackHUD _leftHandHUD;
        [SerializeField] private ItemStackHUD _rightHandHUD;

        private void Start()
        {
            _leftHandHUD.SetItemStack(_handSlot.LeftHandItem);
            _rightHandHUD.SetItemStack(_handSlot.RightHandItem);
        }
    }
}
