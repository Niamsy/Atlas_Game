using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AtlasAudio
{
    public interface IMusicEventListener
    {
        void OnEventRaised(AtlasAudio.Music audio);
    }
}