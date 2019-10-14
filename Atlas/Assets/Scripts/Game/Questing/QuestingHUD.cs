

using System;
using Game.SavingSystem;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Questing
{
    public class QuestingHUD : Menu.MenuWidget
    {
        protected override void InitialiseWidget()
        {
        }

        private void OnEnable()
        {
            SaveManager.Instance.InputControls.Player.Quest.performed += OpenCloseQuesting;
            SaveManager.Instance.InputControls.Player.Quest.Enable();
        }

        private void OnDisable()
        {
            SaveManager.Instance.InputControls.Player.Quest.performed -= OpenCloseQuesting;
            SaveManager.Instance.InputControls.Player.Quest.Disable();
        }

        private void OpenCloseQuesting(InputAction.CallbackContext obj)
        {
            Show(!Displayed);
        }


        public override void Show(bool display, bool force = false)
        {
            if (display == false)
            {
                TimeManager.Instance.PauseGame(false);
            }
            base.Show(display, force);
        }

        public void PauseTime()
        {
            TimeManager.Instance.PauseGame(true);
        }
    }
}
