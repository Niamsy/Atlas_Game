using UnityEngine;
using AtlasEvents;

namespace AtlasAudio {
    public class AudioManager : Singleton<AudioManager>
    {
        private AudioSource _AudioSource;

        // Use this for initialization
        void Start() {
            _AudioSource = GetComponent<AudioSource>();
            if (_AudioSource == null)
            {
                _AudioSource = gameObject.AddComponent<AudioSource>();
            }
        }

        // Update is called once per frame
        void Update() {
            
        }

        public void Play(Audio audio, AudioSource audioSource)
        {
            audio.Play(audioSource == null ? _AudioSource : audioSource);
        }
    }
}
