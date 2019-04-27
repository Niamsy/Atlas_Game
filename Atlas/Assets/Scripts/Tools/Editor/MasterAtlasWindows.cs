using System.Collections.Generic;
using Menu;
using SceneManagement;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Tools.Editor
{
    public class MasterAtlasWindows : EditorWindow
    {
        private static SceneAsset _masterScene;
        private Scene[] _loadedScenes;
        private Scene _activeScene;
        
        private void OnEnable()
        {
            titleContent.text = "ATLAS Master";
            _masterScene = AssetDatabase.LoadAssetAtPath<SceneAsset>("Assets/Scenes/Managers/Master Scene.unity");

            Update();
        }

        private void Update()
        {
            if (Application.isPlaying)
                return;
            
            if (SceneLoader.ActualLoadedScenes == null)
                SceneLoader.ActualLoadedScenes = new List<string>();
            _loadedScenes = new Scene[SceneManager.sceneCount];
            for (int x = 0; x < SceneManager.sceneCount; x++)
            {
                _loadedScenes[x] = SceneManager.GetSceneAt(x);
                var scene = _loadedScenes[x].name;
                if (_loadedScenes[x].buildIndex != 0)
                    SceneLoader.ActualLoadedScenes.Add(scene);
            }
            _activeScene = SceneManager.GetActiveScene();
            SceneLoader.ActiveLoadedScenes = _activeScene.buildIndex;
            EditorSceneManager.playModeStartScene = _masterScene;
        }

        private void OnGUI()
        {
            GUILayout.Label("ATLAS Master windows");
            GUILayout.FlexibleSpace();

            EditorGUI.indentLevel++;
            GUILayout.Label("Gardez cette fenêtre ouverte dans un coin, SVP");
            
            GUILayout.FlexibleSpace();
            _masterScene = (SceneAsset) EditorGUILayout.ObjectField(new GUIContent("Master Scene"),  _masterScene, typeof(SceneAsset), false);
            var labelError = new GUIStyle(GUI.skin.label);
            labelError.normal.textColor = Color.red;
            if (_masterScene == null)
                GUILayout.Label("Si Master Scene est pas remplit les fonctions de request et de chargement de scene vont probablement pas marcher.", labelError);

            GUILayout.FlexibleSpace();
            GUILayout.Label("Les scenes qui seronts chargés");
            EditorGUI.indentLevel++;
            foreach (var loadedScene in _loadedScenes)
                WriteSceneName(loadedScene, labelError);
            EditorGUI.indentLevel--;

            GUILayout.FlexibleSpace();
            GUILayout.Label("Toutes réclamations à mon propos sont à faire a Tim.");
            EditorGUI.indentLevel--;
        }

        private void WriteSceneName(Scene scene, GUIStyle styleError)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("'" + scene.name + "' - " + scene.buildIndex);
            if (scene.buildIndex == _activeScene.buildIndex)
                GUILayout.Label("(Active)");
            if (scene.buildIndex == -1 || scene.buildIndex >= SceneManager.sceneCountInBuildSettings)
                GUILayout.Label("Is not added to the build. Correct that else issue gotta occurs.", styleError);
            GUILayout.EndHorizontal();
        }
        
        [MenuItem("ATLAS/Master Scene")]
        public static void Open()
        {
            GetWindow<MasterAtlasWindows>();
        }
    }
}