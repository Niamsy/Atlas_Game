using AtlasAudio;
using AtlasEvents;
using Game.Map.DayNight;
using Game.SavingSystem;
using Menu.Inventory.ItemDescription;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Menu.Inventory
{
    public class InventoryHUD : MenuWidget
    {
        protected override void InitialiseWidget()
        {
        }

        [Header("Audio")] public Audio OnToggleGUIAudio = null;
        public AudioEvent OnToggleGUIEvent = null;
        
        [SerializeField] private ItemDescriptionHUD _description = null;

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
