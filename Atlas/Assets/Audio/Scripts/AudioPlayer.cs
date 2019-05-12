using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Audio;
using System;

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

        [SerializeField]
        private bool _SHOW_SOUND_DEBUG = false;

        private List<AudioSource> _AudioSources = new List<AudioSource>();
        private Dictionary<string, AudioSource> _MusicSources = new Dictionary<string, AudioSource>();
        private List<string> _MusicSourcesInUse = new List<string>();
        private Dictionary<string, KeyValuePair<string, Music>> musics = new Dictionary<string, KeyValuePair<string, Music>>();
        private ClampedInteger currentAudioSource;

        #region Volumes
        private readonly Dictionary<AudioGroup, float> _groupsVolume = new Dictionary<AudioGroup, float> { { AudioGroup.Effect, 0}, { AudioGroup.Music, 0} };
        private readonly string _volumeSuffix = "Volume";
        
        public float MasterVolume { get; private set; }

        public AudioMixer Mixer;
        #endregion

        #region Initialization
        private string GenerateId()
        {
            return Guid.NewGuid().ToString();
        }

        void Awake()
        {
            LoadMasterVolume();
            LoadGroupsVolume();
            
            int i = 0;
            while (i < AudioPoolSize)
            {
                _AudioSources.Add(gameObject.AddComponent<AudioSource>());
                _MusicSources.Add(GenerateId(), gameObject.AddComponent<AudioSource>());
                ++i;
            }

            currentAudioSource = new ClampedInteger(AudioPoolSize);
        }
        
        // Update is called once per frame
        void Update() {
            List<string> toRemove = new List<string>();

            foreach (KeyValuePair<string, KeyValuePair<string, Music>> music in musics)
            { 
                if (music.Value.Value.IsPlaying)
                {
                    music.Value.Value.Update();
                }
                else
                {
                    _MusicSourcesInUse.Remove(music.Value.Key);
                    toRemove.Add(music.Key);
                }

            }

            foreach (string removed in toRemove)
            {
                if (_SHOW_SOUND_DEBUG) Debug.Log("Stopped " + removed);
                musics.Remove(removed);
            }
        }
        #endregion

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

        #region AudioPlayer methods
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

        private string GetFirstFreeMusicAudioSource()
        {
            foreach (var musicSource in _MusicSources)
            {
                if (!_MusicSourcesInUse.Contains(musicSource.Key))
                {
                    return musicSource.Key;
                }
            }
            return null;
        }

        public void PlayMusic(Music music)
        {
            if (_SHOW_SOUND_DEBUG) Debug.Log("Trying to Play " + music.name);
            if (_MusicSources.Count == 0)
            {
                if (_SHOW_SOUND_DEBUG) Debug.Log("AudioPlayer not initialized Yet");
                return;
            }

            if (!musics.ContainsKey(music.name))
            {
                string nextAudioID = GetFirstFreeMusicAudioSource();

                if (nextAudioID != null)
                {
                    music.Play(_MusicSources[nextAudioID]);
                    _MusicSourcesInUse.Add(nextAudioID);
                    musics.Add(music.name, new KeyValuePair<string, Music>(nextAudioID, music));
                    if (_SHOW_SOUND_DEBUG) Debug.Log("Played " + music.name);
                }
                else
                {
                    if (_SHOW_SOUND_DEBUG) Debug.LogWarning("Not enough Music source, current size : " + _MusicSources.Count);
                }

            } 
            else
            {
                if (_SHOW_SOUND_DEBUG) Debug.Log("Music " + music.name + " already playing");
            }
        }

        public void StopMusic(Music music)
        {
            if (_SHOW_SOUND_DEBUG) Debug.Log("Trying to Stop " + music.name);
            if (musics.ContainsKey(music.name))
            {
                musics[music.name].Value.Stop(music);
            }
        }
        #endregion

        #region ClampedInteger
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
        #endregion
    }
}
