using System.Collections.Generic;
using UnityEngine;

namespace Leveling
{
    [CreateAssetMenu(menuName = "Events/Leveling Event")]
    public class LevelingEvent : ScriptableObject
    {
        /// <summary>
        /// The list of listeners that this event will notify if it is raised.
        /// </summary>
        private readonly List<ILevelingEventListener> eventListeners =
            new List<ILevelingEventListener>();

        public void Raise(int CurrentXP, int XPGain)
        {
            for (int i = eventListeners.Count - 1; i >= 0; i--)
                eventListeners[i].OnEventRaised(CurrentXP, XPGain);
        }

        public void RegisterListener(ILevelingEventListener listener)
        {
            if (!eventListeners.Contains(listener))
                eventListeners.Add(listener);
        }

        public void UnregisterListener(ILevelingEventListener listener)
        {
            if (eventListeners.Contains(listener))
                eventListeners.Remove(listener);
        }
    }
}