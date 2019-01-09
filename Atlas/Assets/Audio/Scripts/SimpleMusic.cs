using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace AtlasAudio
{
    [CreateAssetMenu(menuName = "Audio/Music")]
    public class SimpleMusic : Music
    {
        public AudioClip[] Musics;
        [MinMaxRange(0, 1)]
        public float Volume = 1f;
        public bool Loop = false;
        [MinMaxRange(0, 10)]
        public float FadeInSeconds = 3f;
        [MinMaxRange(0, 10)]
        public float FadeOutSeconds = 3f;

        public UnityEngine.Audio.AudioMixerGroup audioMixerGroup;

        private AudioClip currentClip;
        private AudioSource source;
        private float fadeInterpolater;
        private float onFadeStartVolume;
        private float targetVolume;

        private bool Paused = false;
        private bool Stopping = false;

        public override void Pause()
        {
            if (!Paused && IsPlaying)
            {
                source.Pause();
                Paused = true;
            }
        }

        public override void Play(AudioSource audioSource)
        {
            if (Musics.Length == 0) return;
                
            currentClip = Musics[Random.Range(0, Musics.Length)];
            source = audioSource;

            source.clip = currentClip;
            source.loop = Loop;
            source.volume = 0f;
            source.outputAudioMixerGroup = audioMixerGroup;
            source.Play();
            onFadeStartVolume = 0f;
            targetVolume = Volume;
            IsPlaying = true;
            Stopping = false;
            fadeInterpolater = 0f;
            tempFadeSeconds = -1f;
        }

        public override void UnPause()
        {
            if (Paused)
            {
                source.UnPause();
                Paused = false;
            }
        }

        public override void Stop(Music music)
        {
            Stopping = true;
            fadeInterpolater = 0f;
            targetVolume = 0f;
            onFadeStartVolume = Volume;
        }

        private float tempFadeSeconds = -1f;

        public override void Update()
        {

            if (source == null) return;

            if (source.volume != targetVolume)
            {
                float fadeValue;
                fadeInterpolater += Time.unscaledDeltaTime;
                fadeValue = tempFadeSeconds != -1f ? tempFadeSeconds : Volume > targetVolume ? FadeOutSeconds * 1000 : FadeInSeconds * 1000;
                source.volume = Mathf.Lerp(source.volume, targetVolume, fadeInterpolater / fadeValue);
            }
            else if (tempFadeSeconds != -1f)
            {
                tempFadeSeconds = -1f;
            }

            IsPlaying = source.isPlaying;

            if (Volume < 0.005f && Stopping)
            {
                source.Stop();
                Stopping = false;
                IsPlaying = false;
                Paused = false;
                source = null;
            }
        }
    }
}