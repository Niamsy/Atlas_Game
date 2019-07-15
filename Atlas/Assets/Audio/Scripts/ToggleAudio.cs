using UnityEngine;

namespace AtlasAudio
{
    [CreateAssetMenu(menuName = "Audio/Toggle")]
    public class ToggleAudio : Audio
    {
        public AudioClip On;
        public AudioClip Off;

        public RangedFloat Volume;
        [MinMaxRange(0, 2)]
        public RangedFloat Pitch;

        public bool State = false;

        public UnityEngine.Audio.AudioMixerGroup audioMixerGroup;

        public override void Play(AudioSource source)
        {
            source.clip = State ? Off : On;
            State = !State;
            source.volume = Random.Range(Volume.minimum, Volume.maximum);
            source.pitch = Random.Range(Pitch.minimum, Pitch.maximum);
            source.outputAudioMixerGroup = audioMixerGroup;
            if (source.isPlaying)
                source.Stop();
            source.Play();
        }
    }
}