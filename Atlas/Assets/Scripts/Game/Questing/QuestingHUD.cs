

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
        [SerializeField] private Button rewardButton = null;
        [SerializeField] private DescriptionHUD descriptionHud = null;
        [SerializeField] private AnnouncementHUD announcementHud = null;
        [SerializeField] private QuestCompletedRewardHUD rewardHud = null;

        private LiveQuest _quest;
        private QuestingSaver _saver = null;

        public void NewQuest(LiveQuest quest)
        {
            _quest = quest;
            rewardHud.enabled = false;
            descriptionHud.enabled = true;
            
            announcementHud.NewQuest(quest);
            descriptionHud.SetData(quest);

            if (!Displayed)
                Show(true);
        }

        public void QuestComplete(LiveQuest quest)
        {
            _quest = quest;
            descriptionHud.enabled = false;
            rewardHud.enabled = true;
            
            announcementHud.QuestComplete(quest);
            rewardHud.SetData(quest);

            if (!Displayed)
                Show(true);
        }

        protected override void InitialiseWidget()
        {
        }

        private void OnEnable()
        {
            _saver = GetComponent<QuestingSaver>();
            okButton.onClick.AddListener(() => {
                Show(false);
            });
            rewardButton.onClick.AddListener(() =>
            {
                _saver.GiveRewards(_quest);
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
