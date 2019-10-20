

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

        private Quest _quest = null;
        private QuestingSaver _saver = null;

        public void NewQuest(Quest quest)
        {
            Debug.Log("Received a new quest!");
            _quest = quest;
            descriptionHud.SetData(quest);
            announcementHud.SetData(quest);
            if (!Displayed)
                Show(true);
        }

        protected override void InitialiseWidget()
        {
        }

        protected override void Awake()
        {
            base.Awake();
            _saver = GetComponent<QuestingSaver>();
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
                _quest = null;
            }
            else
            {
                if (_quest == null && _saver.LiveQuests.Count > 0)
                {
                    NewQuest(_saver.LiveQuests[0].Quest);
                }
            }
            base.Show(display, force);
        }

        public void PauseTime()
        {
            TimeManager.Instance.PauseGame(true);
        }
    }
}
