using UnityEngine;

namespace Game.Questing
{
    public delegate void OnOkClickDelegate();

    public abstract class ALiveQuestConsumer : MonoBehaviour
    {
        public abstract void ConsumeLiveQuest(LiveQuest liveQuest);
        public abstract void SetOnOkClickDelegate(OnOkClickDelegate _delegate);
    }
}