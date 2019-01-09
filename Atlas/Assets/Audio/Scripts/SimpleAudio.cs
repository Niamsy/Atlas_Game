using UnityEngine;
using Random = UnityEngine.Random;

namespace AtlasAudio
{
    [CreateAssetMenu(menuName="Audio/Simple")]
    public class SimpleAudio : Audio
    {
        public AudioClip[] Clips;

        public RangedFloat Volume;

        [MinMaxRange(0, 2)]
        public RangedFloat Pitch;

        public UnityEngine.Audio.AudioMixerGroup audioMixerGroup;

        public override void Play(AudioSource source)
        {
            // TODO Stop Audio if already Playing or not

            if (Clips.Length == 0) return;

            source.clip = Clips[Random.Range(0, Clips.Length)];
            source.volume = Random.Range(Volume.minimum, Volume.maximum);
            source.pitch = Random.Range(Pitch.minimum, Pitch.maximum);
            source.outputAudioMixerGroup = audioMixerGroup;
            if (source.isPlaying)
                source.Stop();
            source.Play();

        }
    }
}