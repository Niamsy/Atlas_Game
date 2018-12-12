using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Audio {
    public class AudioManager : Singleton<AudioManager> {

        private AudioStore _AudioStore;
        private AudioSource _AudioSource;

        // Use this for initialization
        void Start() {
            _AudioStore = new AudioStore();
            _AudioSource = GetComponent<AudioSource>();
            if (_AudioSource == null)
            {
                _AudioSource = gameObject.AddComponent<AudioSource>();
            }
        }

        // Update is called once per frame
        void Update() {
            
        }

        public void Play(AudioStore.AUDIO audio)
        {
            _AudioStore.Store[audio].Play(_AudioSource);
        }

        public void Play(AudioStore.AUDIO audio, AudioSource audioSource)
        {
            if (audioSource == null)
            {
                Debug.LogWarning("Audio source is null");
                return;
            }
            _AudioStore.Store[audio].Play(audioSource);
        }
    }
}
