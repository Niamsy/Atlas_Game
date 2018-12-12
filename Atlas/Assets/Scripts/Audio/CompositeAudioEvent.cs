using System;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(menuName="Audio Events/Composite")]
public class CompositeAudioEvent : AudioEvent
{
	[Serializable]
	public struct CompositeEntry
	{
		public AudioEvent Event;
		public float Weight;
	}

	public CompositeEntry[] Entries;

	public override void Play(AudioSource source)
	{
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