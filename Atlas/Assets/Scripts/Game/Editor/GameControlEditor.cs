using Game.Map;
using Game.SavingSystem;
using SceneManagement;
using UnityEditor;
using UnityEngine;

namespace Game.Editor
{
    [CustomEditor(typeof(GameControl))]
    [CanEditMultipleObjects]
    public class GameControlEditor : UnityEditor.Editor
    {
        public int SceneIndex = 2;

        public void Start()
        {
            SceneIndex = MapManager.Instance.gameObject.scene.buildIndex;
        }
        
        public override void OnInspectorGUI()
        {
            GameControl myTarget = (GameControl) target;
            DrawDefaultInspector();
            if (Application.isPlaying)
            {
                GUILayout.Label("Player data");
                GUILayout.BeginHorizontal();
                if (GUILayout.Button("Load"))
                    myTarget.LoadPlayerData();
                if (GUILayout.Button("Save"))
                    myTarget.SavePlayerData();
                GUILayout.EndHorizontal();

                GUILayout.Label("Map data: Scene Index " + SceneLoader.ActiveLoadedScenes);
                GUILayout.BeginHorizontal();
                if (GUILayout.Button("Load"))
                    myTarget.LoadMapData(SceneLoader.ActiveLoadedScenes);
                if (GUILayout.Button("Save"))
                    myTarget.SaveMapData(SceneLoader.ActiveLoadedScenes);
                GUILayout.EndHorizontal();
            }
        }
    }
}
