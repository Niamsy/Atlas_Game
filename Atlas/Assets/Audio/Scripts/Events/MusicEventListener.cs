using UnityEngine;
using UnityEngine.Events;

namespace AtlasEvents
{
    [System.Serializable]
    public class MusicEventListener : MonoBehaviour, AtlasAudio.IMusicEventListener
    {
        [System.Serializable]
        public class MusicUnityEvent : UnityEvent<AtlasAudio.Music> { }

        [Tooltip("Event to register with.")]
        public MusicEvent Event;

        [Tooltip("Response to invoke when Event is raised.")]
        public MusicUnityEvent Response;

        private void OnEnable()
        {
            Event.RegisterListener(this);
        }

        private void OnDisable()
        {
            Event.UnregisterListener(this);
        }

        public void OnEventRaised(AtlasAudio.Music audio)
        {
            Response.Invoke(audio);
        }
    }
}