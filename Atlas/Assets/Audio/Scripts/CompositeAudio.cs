using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace AtlasAudio
{
    [CreateAssetMenu(menuName = "Audio/Composite")]
    public class CompositeAudio : Audio
    { 
        [Serializable]
        public struct CompositeEntry
        {
            public Audio Event;
            public float Weight;
        }

        public CompositeEntry[] Entries;

        public override void Play(AudioSource source)
        {
            // TODO Stop Audio if already Playing or not
            float totalWeight = 0;
            foreach (CompositeEntry e in Entries)
                totalWeight += e.Weight;

            float pick = Random.Range(0, totalWeight);
            foreach (CompositeEntry e in Entries)
            {
                if (pick > e.Weight)
                {
                    pick -= e.Weight;
                    continue;
                }

                e.Event.Play(source);
                return;
            }
        }
    }
}