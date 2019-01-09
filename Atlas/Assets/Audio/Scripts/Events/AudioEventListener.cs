using UnityEngine;
using UnityEngine.Events;

namespace AtlasEvents
{
    [System.Serializable]
    public class AudioEventListener : MonoBehaviour, AtlasAudio.IAudioEventListener 
    {
        [System.Serializable]
        public class AudioUnityEvent : UnityEvent<AtlasAudio.Audio, AudioSource> { }

        [Tooltip("Event to register with.")]
        public AudioEvent Event;

        [Tooltip("Response to invoke when Event is raised.")]
        public AudioUnityEvent Response;

        private void OnEnable()
        {
            Event.RegisterListener(this);
        }

        private void OnDisable()
        {
            Event.UnregisterListener(this);
        }

        public void OnEventRaised(AtlasAudio.Audio audio, AudioSource source)
        {
            Response.Invoke(audio, source);
        }
    }
}