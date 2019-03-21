﻿using Game.Player;
using UnityEngine;

namespace Menu.Inventory
{
    public class HandSlotHUD : MonoBehaviour
    {
        private HandSlots _handSlot;
        [SerializeField] private ItemStackHUD _rightHandHUD;

        private void Awake()
        {
            _handSlot = FindObjectOfType<HandSlots>();
        }
        
        private void Start()
        {
            _rightHandHUD.SetItemStack(_handSlot.EquippedItemStack);
        }
    }
}
