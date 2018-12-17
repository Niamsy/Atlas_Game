using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AtlasAudio;
using System;
using AtlasEvents;

namespace Player
{
    [RequireComponent(typeof(Animator))]
    public class AnimatorSoundEmitter : MonoBehaviour
    {
        [Serializable]
        public struct Sound
        {
            [Tooltip("Parameter passed by the animation event to recognize")]
            public string tag;
            public AudioSource source;
            public Audio audio;
        }

        [Tooltip("List of sounds to play")]
        public Sound[] steps;

        [Tooltip("If a sound has no audiosource, it'll be played globally by an AudioPlayer")]
        public AudioEvent Event;

        public void OnSoundEvent(string tag)
        {
            if (steps.Length > 0)
            {
                Sound sound = Array.Find(steps, (Sound s) => s.tag == tag);

                if (sound.tag != "")
                {
                    PlaySound(sound);
                }
            }
        }

        private void PlaySound(Sound sound)
        {
            if (sound.source == null)
            {
                Event.Raise(sound.audio, null);
            }
            else
            {
                sound.audio.Play(sound.source);
            }
        }
    }
}