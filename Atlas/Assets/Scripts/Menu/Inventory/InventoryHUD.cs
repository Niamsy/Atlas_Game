using System.Collections.Generic;
using AtlasAudio;
using AtlasEvents;
using Game.Map.DayNight;
using Game.Player;
using Game.SavingSystem;
using Menu.Inventory.ItemDescription;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace Menu.Inventory
{
    public class InventoryHUD : MenuWidget
    {
        private HandSlots _handSlot;
        private List<InputAction> _shortcuts;

        [Header("Audio")] public Audio OnToggleGUIAudio = null;
        public AudioEvent OnToggleGUIEvent = null;
        
        [SerializeField] private ItemDescriptionHUD _description = null;
        
        protected override void InitialiseWidget()
        {
            _handSlot = FindObjectOfType<HandSlots>();
            
            _shortcuts = new List<InputAction>();
            _shortcuts.Add(SaveManager.Instance.InputControls.Player.SelectField1);
            _shortcuts.Add(SaveManager.Instance.InputControls.Player.SelectField2);
            _shortcuts.Add(SaveManager.Instance.InputControls.Player.SelectField3);
            _shortcuts.Add(SaveManager.Instance.InputControls.Player.SelectField4);
            _shortcuts.Add(SaveManager.Instance.InputControls.Player.SelectField5);
            _shortcuts.Add(SaveManager.Instance.InputControls.Player.SelectField6);
            _shortcuts.Add(SaveManager.Instance.InputControls.Player.SelectField7);
            _shortcuts.Add(SaveManager.Instance.InputControls.Player.SelectField8);
            
            foreach (InputAction action in _shortcuts)
            {
                action.performed += ShortcutPressed;
            }
        }

        private void ShortcutPressed(InputAction.CallbackContext obj)
        {
            int index = _shortcuts.FindIndex(act => act == obj.action);
                   
            PointerEventData pointerData = new PointerEventData (EventSystem.current)
            {
                pointerId = -1,
            };
         
            pointerData.position = Input.mousePosition;
 
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerData, results);

            if (results.Count > 0)
            {
                var stack = results[0].gameObject.GetComponent<ItemStackHUD>();
                if (stack != null)
                    _handSlot[index].SwapStack(stack.ActualStack);
            }
        }

        private void OnDestroy()
        {
            foreach (InputAction action in _shortcuts)
            {
                action.performed -= ShortcutPressed;
            }
        }

        private void OnEnable()
        {
            SaveManager.Instance.InputControls.Player.Inventory.performed += OpenCloseInventory;
            SaveManager.Instance.InputControls.Player.Inventory.Enable();
        }

        private void OnDisable()
        {
            SaveManager.Instance.InputControls.Player.Inventory.performed -= OpenCloseInventory;
            SaveManager.Instance.InputControls.Player.Inventory.Disable();
        }

        private void OpenCloseInventory(InputAction.CallbackContext obj)
        {
            Show(!Displayed);
            if (OnToggleGUIAudio && OnToggleGUIEvent)
            {
                OnToggleGUIEvent.Raise(OnToggleGUIAudio, null);
            }
        }

        public override void Show(bool display, bool force = false)
        {
            if (display)
                TimeManager.AskForPause(this);
            else
                TimeManager.StopPause(this);
            base.Show(display, force);
            _description.Reset();
        }
    }
}
