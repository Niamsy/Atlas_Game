using UnityEngine;
using AtlasEvents;
using System.Collections.Generic;

namespace AtlasAudio {
    public class AudioPlayer : Singleton<AudioPlayer>
    {
        [MinMaxRange(2, 10)]
        public int AudioPoolSize = 5;
        [MinMaxRange(2, 10)]
        public int MusicPoolSize = 4;

        private List<AudioSource> _AudioSources = new List<AudioSource>();
        private List<AudioSource> _MusicSources = new List<AudioSource>();
        private Music[] musics = new Music[10];
        private ClampedInteger currentAudioSource;
        private ClampedInteger currentMusicSource;
        private ClampedInteger currentMusic;

        // Use this for initialization
        void Start() {
            int i = 0;
            while (i < AudioPoolSize)
            {
                _AudioSources.Add(gameObject.AddComponent<AudioSource>());
                _MusicSources.Add(gameObject.AddComponent<AudioSource>());
                ++i;
            }

            currentAudioSource = new ClampedInteger(AudioPoolSize);
            currentMusicSource = new ClampedInteger(MusicPoolSize);
            currentMusic = new ClampedInteger(MusicPoolSize);
        }

        // Update is called once per frame
        void Update() {
            foreach (Music music in musics)
            {
                if (music && music.IsPlaying)
                {
                    music.Update();
                }
            }     
        }

        public void Play(Audio audio, AudioSource audioSource)
        {
            if (audioSource)
            {
                if (audioSource.isPlaying)
                    audioSource.Stop();
            }
            audio.Play(audioSource == null ? _AudioSources[currentAudioSource.Value] : audioSource);
            currentAudioSource++;
        }

        public void Stop(Audio audio, AudioSource audioSource)
        {
            if (audioSource)
            {
                audioSource.Stop();
            }
        }

        public void PlayMusic(Music music)
        {
            if (_MusicSources.Count == 0) return;

            if (musics[currentMusic.Value] != null && musics[currentMusic.Value].IsPlaying)
            {
                musics[currentMusic.Value].Stop(music);
            }
                        
            music.Play(_MusicSources[currentMusicSource.Value]);
            currentMusicSource++;
            currentMusic++;

            musics[currentMusic.Value] = music;
        }

        public void StopMusic(Music music)
        {
            if (musics[currentMusic.Value].IsPlaying)
            {
                musics[currentMusic.Value].Stop(music);
            }
            currentMusic++; 
        }


        class ClampedInteger
        {
            int value = 0;
            public int max;

            public int Value
            {
                get { return value; }
                private set { }
            }

            public ClampedInteger(int max)
            {
                this.max = max;
            }

            public static ClampedInteger operator++(ClampedInteger a)
            {
                ++a.value;
                a.value = a.value % a.max;
                return a;
            }
        }
    }
}
