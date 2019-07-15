using UnityEngine;

namespace AtlasAudio
{
    public abstract class Audio : ScriptableObject
    {
        public abstract void Play(AudioSource source);
        public void Stop(AudioSource source)
        {
            if (source.isPlaying)
                source.Stop();
        }
    }
}