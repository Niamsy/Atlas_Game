using System;
using System.Collections.Generic;
using AtlasAudio;
using AtlasEvents;
using Game.Map.DayNight;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Questing
{
    public class QuestingHud : Menu.MenuWidget
    {
        [Header("Live Quest Consumers")]
        [Tooltip("Those HUD listed here must implement ALiveQuestConsumer. they will be fed currently selected live quest informations.")]
        [SerializeField] private List<ALiveQuestConsumer> liveQuestConsumer = null;
        
        [Header("Audio")]
        [SerializeField] private Audio openingAudio = null;
        [SerializeField] private Audio closingAudio = null;
        [SerializeField] private AudioEvent audioEvent = null;
        
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
                TimeManager.StopPause(this);
                if (openingAudio != null && audioEvent != null)
                    audioEvent.Raise(closingAudio, null);
            }
            else
            {
                if (closingAudio != null && audioEvent != null)
                    audioEvent.Raise(openingAudio, null);
            }
            base.Show(display, force);
        }

        public void PauseTime()
        {
            TimeManager.AskForPause(this);
        }

        private void OnDestroy()
        {
            if (TimeManager.IsGamePaused)
            {
                TimeManager.StopPause(this);
            }
        }
    }
}
