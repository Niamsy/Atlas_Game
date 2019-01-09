using UnityEngine;

namespace AtlasAudio
{
    public interface IAudioEventListener
    {
        void OnEventRaised(AtlasAudio.Audio audio, AudioSource source);
    }
}