using UnityEngine;

namespace AtlasAudio
{
    public abstract class Music : ScriptableObject
    {
        // Start is called before the first frame update
        public abstract void Play(AudioSource source);

        // Update is called once per frame
        public abstract void Update();

        public abstract void Stop(Music music);

        public abstract void Pause();

        public abstract void UnPause();

        public bool IsPlaying
        {
            get;
            protected set;
        }
    }
}