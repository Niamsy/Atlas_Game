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
            if (audioSource)
            {
                if (audioSource.isPlaying)
                    audioSource.Stop();
            }
            else
            {
                if (_AudioSource.isPlaying)
                    _AudioSource.Stop();
            }
            audio.Play(audioSource == null ? _AudioSource : audioSource);
        }
    }
}
