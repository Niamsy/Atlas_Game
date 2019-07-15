using System.Collections.Generic;
using UnityEngine;
using AtlasAudio;

namespace AtlasEvents
{
    [CreateAssetMenu(menuName = "Events/Music")]
    public class MusicEvent : ScriptableObject
    {
        /// <summary>
        /// The list of listeners that this event will notify if it is raised.
        /// </summary>
        private readonly List<IMusicEventListener> eventListeners =
            new List<IMusicEventListener>();

        public void Raise(AtlasAudio.Music audio)
        {
            for (int i = eventListeners.Count - 1; i >= 0; i--)
                eventListeners[i].OnEventRaised(audio);
        }

        public void RegisterListener(IMusicEventListener listener)
        {
            if (!eventListeners.Contains(listener))
                eventListeners.Add(listener);
        }

        public void UnregisterListener(IMusicEventListener listener)
        {
            if (eventListeners.Contains(listener))
                eventListeners.Remove(listener);
        }
    }
}