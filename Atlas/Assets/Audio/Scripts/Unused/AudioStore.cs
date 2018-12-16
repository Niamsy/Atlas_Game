using System.Collections.Generic;
using UnityEditor;

namespace AtlasAudio {
	public class AudioStore
	{
		public enum AUDIO
		{
			AUDIO_GUI_TOGGLEGUI
		}

		string[] paths = new string[]
		{
			"Assets/Audio/GUI/ToggleGUI.asset"
		};

		public Dictionary<AUDIO, Audio> Store = new Dictionary<AUDIO, Audio>();

		public AudioStore()
		{
			Store.Add(AUDIO.AUDIO_GUI_TOGGLEGUI,  AssetDatabase.LoadAssetAtPath<Audio>(paths[0]));
		}
	}
}
