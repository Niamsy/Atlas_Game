using Game.Player;
using Game.SavingSystem;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Menu.Inventory
{
    public class HandSlotHUD : MonoBehaviour
    {
        private HandSlots _handSlot = null;

        [SerializeField] private ItemStackActionHUD[] _handStackUI = null;
        [SerializeField] private Text _useText = null;
        
        #if UNITY_EDITOR
        private void Reset()
        {
            if (Application.isEditor)
                _handStackUI = GetComponentsInChildren<ItemStackActionHUD>();
        }
        #endif
        
        private void Awake()
        {
            _handSlot = FindObjectOfType<HandSlots>();
            #if ATLAS_DEBUG
            if (_handStackUI.Length != HandSlots.NbOfItemSlots)
                Debug.LogError("Not enought item in list 'HandStackUI' for " + name + "." + "Actually '" + _handStackUI.Length + "' against expected '" + HandSlots.NbOfItemSlots + "'");
            #endif
            _handSlot.OnSelectSlotChanged += OnSelectSlotChanged;
        }

        private void OnSelectSlotChanged(int idx)
        {
            for (int x = 0; x < HandSlots.NbOfItemSlots; x++)
            {
                _handStackUI[x].Select(x == idx);
            }
        }
        
        private void Start()
        {
            InputControls.PlayerActions playerControls = SaveManager.Instance.InputControls.Player;
            InputAction[] actions = {playerControls.SelectField1, playerControls.SelectField2, playerControls.SelectField3, playerControls.SelectField4, playerControls.SelectField5, playerControls.SelectField6, playerControls.SelectField7, playerControls.SelectField8};
            
            for (int x = 0; x < HandSlots.NbOfItemSlots; x++)
            {
                _handStackUI[x].SetItemStack(_handSlot[x]);
                _handStackUI[x].SetAction(actions[x]);
            }
        }

        private void Update()
        {
            if (_handSlot.SelectedItem && _handSlot.SelectedItem.CanUse(_handSlot.transform))
                _useText.text = _handSlot.SelectedItem.UsageText;
            else
                _useText.text = "";
        }
    }
}
