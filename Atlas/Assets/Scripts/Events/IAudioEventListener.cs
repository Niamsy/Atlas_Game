using UnityEngine;

public interface IAudioEventListener
{
    void OnEventRaised(AtlasAudio.Audio audio, AudioSource source);
}
