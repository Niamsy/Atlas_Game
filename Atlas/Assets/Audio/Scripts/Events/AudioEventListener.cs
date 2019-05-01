using UnityEngine;
using UnityEngine.Events;

namespace AtlasEvents
{
    [System.Serializable]
    public class AudioEventListener : MonoBehaviour, AtlasAudio.IAudioEventListener 
    {
        [System.Serializable]
        public class AudioUnityEvent : UnityEvent<AtlasAudio.Audio, AudioSource> { }

        [Tooltip("Events to register to.")]
        public AudioEvent[] Events;

        [Tooltip("Response to invoke when Event is raised.")]
        public AudioUnityEvent Response;

        private void OnEnable()
        {
            foreach (AudioEvent _event in Events) {
                _event.RegisterListener(this);
            }
        }

        private void OnDisable()
        {
            foreach (AudioEvent _event in Events)
            {
                _event.UnregisterListener(this);
            }
        }

        public void OnEventRaised(AtlasAudio.Audio audio, AudioSource source)
        {
            Response.Invoke(audio, source);
        }
    }
}