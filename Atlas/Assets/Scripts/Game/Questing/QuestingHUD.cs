

using System;
using Game.SavingSystem;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Game.Questing
{
    public class QuestingHUD : Menu.MenuWidget
    {
        [SerializeField] private Button okButton = null;
        [SerializeField] private DescriptionHUD descriptionHud = null;
        [SerializeField] private AnnouncementHUD announcementHud = null;

        private Quest _quest = null;

        public void NewQuest(Quest quest)
        {
            Debug.Log("Received a new quest!");
            _quest = quest;
            descriptionHud.SetData(quest);
            announcementHud.SetData(quest);
            Show(true);
        }
        
        protected override void InitialiseWidget()
        {
        }

        private void OnEnable()
        {
            SaveManager.Instance.InputControls.Player.Quest.performed += OpenCloseQuesting;
            SaveManager.Instance.InputControls.Player.Quest.Enable();
            okButton.onClick.AddListener(() => {
                Show(false);
            });
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
