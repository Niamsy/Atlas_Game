using UnityEngine;
using UnityEngine.Events;

namespace Leveling
{
    public class LevelingEventListener : MonoBehaviour, ILevelingEventListener
    {
        [System.Serializable]
        public class LevelingUnityEvent : UnityEvent<int, int> { }

        [Tooltip("Event to register with.")]
        public LevelingEvent Event;

        [Tooltip("Response to invoke when Event is raised.")]
        public LevelingUnityEvent Response;

        private void OnEnable()
        {
            Event.RegisterListener(this);
        }

        private void OnDisable()
        {
            Event.UnregisterListener(this);
        }

        public void OnEventRaised(int CurrentXP, int GainXP)
        {
            Response.Invoke(CurrentXP, GainXP);
        }
    }
}
