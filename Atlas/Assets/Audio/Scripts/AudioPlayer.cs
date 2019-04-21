using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Audio;

namespace AtlasAudio {
    public enum AudioGroup
    {
        Effect = 1,
        Music = 2
    }
    
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

        #region Volumes
        private readonly Dictionary<AudioGroup, float> _groupsVolume = new Dictionary<AudioGroup, float> { { AudioGroup.Effect, 0}, { AudioGroup.Music, 0} };
        private readonly string _volumeSuffix = "Volume";
        
        public float MasterVolume { get; private set; }

        public AudioMixer Mixer;
        #endregion
            
        // Use this for initialization
        void Start()
        {
            LoadMasterVolume();
            LoadGroupsVolume();
            
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

        #region Volume control
        #region MasterVolume
        private void LoadMasterVolume()
        {
            MasterVolume = PlayerPrefs.GetFloat("Main Volume");
            AudioListener.volume = MasterVolume;
        }

        private void SaveMasterVolume()
        {
            PlayerPrefs.SetFloat("Main Volume", MasterVolume);
            AudioListener.volume = MasterVolume;
        }
        
        public void SetMasterVolume(float value)
        {
            MasterVolume = value;
            SaveMasterVolume();
        }
        #endregion
        
        #region Group Volume

        private static void SetMixerVolume0To1(AudioMixer mixer, string key, float value)
        {
            mixer.SetFloat(key, (value * 80) - 80);
        }
        
        private void LoadGroupVolume(AudioGroup audioGroup)
        {
            string key = audioGroup + _volumeSuffix;
            _groupsVolume[audioGroup] = PlayerPrefs.GetFloat(key);
            SetMixerVolume0To1(Mixer, key, _groupsVolume[audioGroup]);
        }
        
        public void LoadGroupsVolume()
        {
            LoadGroupVolume(AudioGroup.Effect);
            LoadGroupVolume(AudioGroup.Music);
        }

        public void SetGroupVolume(AudioGroup audioGroup, float value)
        {
            _groupsVolume[audioGroup] = value;
            string key = audioGroup + _volumeSuffix;
            PlayerPrefs.SetFloat(key, value);
            SetMixerVolume0To1(Mixer, key, value);
        }
        
        public float GetGroupVolume(AudioGroup audioGroup)
        {
            return (_groupsVolume[audioGroup]);
        }
        #endregion
        #endregion

        public void Play(Audio audio)
        {
            if (audio)
            {
                audio.Play(_AudioSources[currentAudioSource.Value]);
                currentAudioSource++;
            }
        }

        public void Play(Audio audio, AudioSource audioSource)
        {
            if (audioSource)
            {
                if (audioSource.isPlaying)
                    audioSource.Stop();
            }
            if (audio)
            {
                audio.Play(audioSource ?? _AudioSources[currentAudioSource.Value]);
                currentAudioSource++;
            }
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
