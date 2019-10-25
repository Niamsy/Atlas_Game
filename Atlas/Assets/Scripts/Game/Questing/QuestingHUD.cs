using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Questing
{
    public class QuestingHud : Menu.MenuWidget
    {
        [SerializeField] private List<ALiveQuestConsumer> liveQuestConsumer = null;

        public delegate void OnOkClicked(LiveQuest quest);

        private OnOkClicked _onOkClicked = null;
        
        private LiveQuest _quest;

        public void SetDelegate(OnOkClicked _delegate)
        {
            _onOkClicked = _delegate;
        }

        public void SetData(LiveQuest quest)
        {
            _quest = quest;
            
            liveQuestConsumer.ForEach(consumer => consumer.ConsumeLiveQuest(_quest));
        }

        protected override void InitialiseWidget()
        {
        }

        private void OnEnable()
        {
            liveQuestConsumer.ForEach(consumer => consumer.SetOnOkClickDelegate(() =>
            {
                _onOkClicked?.Invoke(_quest);
                Show(false);
            }));
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
