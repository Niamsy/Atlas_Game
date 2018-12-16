using System.Collections.Generic;
using UnityEditor;

namespace Audio {
	public class AudioStore
	{
		public enum AUDIO
		{
			ASSETS_SCRIPTS_AUDIO_GARBAGE_BOOMHEADSHOT,
			ASSETS_SCRIPTS_AUDIO_GARBAGE_GRODNOBB,
			ASSETS_SCRIPTS_AUDIO_GARBAGE_SHELLEXPLOSION,
			ASSETS_SCRIPTS_AUDIO_GARBAGE_TEST
		}

		string[] paths = new[]
		{
			"Assets/Scripts/Audio/Garbage/BOomHeadSHot.asset",
			"Assets/Scripts/Audio/Garbage/GrodNObb.asset",
			"Assets/Scripts/Audio/Garbage/ShellExplosion.asset",
			"Assets/Scripts/Audio/Garbage/Test.asset"
		};

		public Dictionary<AUDIO, AudioEvent> Store = new Dictionary<AUDIO, AudioEvent>();

		public AudioStore()
		{
			Store.Add(AUDIO.ASSETS_SCRIPTS_AUDIO_GARBAGE_BOOMHEADSHOT,  AssetDatabase.LoadAssetAtPath<AudioEvent>(paths[0]));
			Store.Add(AUDIO.ASSETS_SCRIPTS_AUDIO_GARBAGE_GRODNOBB,  AssetDatabase.LoadAssetAtPath<AudioEvent>(paths[1]));
			Store.Add(AUDIO.ASSETS_SCRIPTS_AUDIO_GARBAGE_SHELLEXPLOSION,  AssetDatabase.LoadAssetAtPath<AudioEvent>(paths[2]));
			Store.Add(AUDIO.ASSETS_SCRIPTS_AUDIO_GARBAGE_TEST,  AssetDatabase.LoadAssetAtPath<AudioEvent>(paths[3]));
		}
	}
}
