

using System;
using System.Linq;
using Game.SavingSystem;
using Tools.Tools;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Game.Questing
{
    [RequireComponent(typeof(QuestingSaver))]
    public class QuestingHUD : Menu.MenuWidget
    {
        [SerializeField] private Button okButton = null;
        [SerializeField] private DescriptionHUD descriptionHud = null;
        [SerializeField] private AnnouncementHUD announcementHud = null;

        private LiveQuest _quest;

        public void NewQuest(LiveQuest quest)
        {
            _quest = quest;
            descriptionHud.SetData(quest);
            announcementHud.NewQuest(quest);
            if (!Displayed)
                Show(true);
        }

        public void QuestComplete(LiveQuest quest)
        {
            _quest = quest;
            announcementHud.QuestComplete(quest);
        }

        protected override void InitialiseWidget()
        {
        }

        private void OnEnable()
        {
            okButton.onClick.AddListener(() => {
                Show(false);
            });
        }

        public void OpenCloseQuesting(InputAction.CallbackContext obj)
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
