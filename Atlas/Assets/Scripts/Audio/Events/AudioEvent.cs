using System.Collections.Generic;
using UnityEngine;
using AtlasAudio;

namespace AtlasEvents
{
    [CreateAssetMenu(menuName = "Events/Audio")]
    public class AudioEvent : ScriptableObject
    {
        /// <summary>
        /// The list of listeners that this event will notify if it is raised.
        /// </summary>
        private readonly List<IAudioEventListener> eventListeners =
            new List<IAudioEventListener>();

        public void Raise(AtlasAudio.Audio audio, AudioSource source)
        {
            for (int i = eventListeners.Count - 1; i >= 0; i--)
                eventListeners[i].OnEventRaised(audio, source);
        }

        public void RegisterListener(IAudioEventListener listener)
        {
            if (!eventListeners.Contains(listener))
                eventListeners.Add(listener);
        }

        public void UnregisterListener(IAudioEventListener listener)
        {
            if (eventListeners.Contains(listener))
                eventListeners.Remove(listener);
        }
    }
}