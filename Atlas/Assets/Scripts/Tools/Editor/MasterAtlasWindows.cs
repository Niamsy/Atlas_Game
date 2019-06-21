using System.Collections.Generic;
using Game.SavingSystem;
using Game.SavingSystem.Datas;
using SceneManagement;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;
using UnityEngine.SceneManagement;

namespace Tools.Editor
{
    public static class AtlasEditor
    {
        #region Labels
        public static GUIStyle LabelColored(Color color)
        {
            var labelError = new GUIStyle(GUI.skin.label);
            labelError.normal.textColor = color;
            return (labelError);
        }
        public static GUIStyle TextFieldColored(Color color)
        {
            var labelError = new GUIStyle(GUI.skin.textField);
            labelError.normal.textColor = color;
            return (labelError);
        }
        #endregion
    }
    public class MasterAtlasWindows : EditorWindow
    {
        private static SceneAsset _masterScene;
        private Scene[] _loadedScenes;
        private Scene _activeScene;
        private SaveManager _saveManager;
        
        private readonly string _masterScenePath = "Assets/Scenes/Managers/Master Scene.unity";
       
        
        private void OnEnable()
        {
            titleContent.text = "ATLAS Master";
            _masterScene = AssetDatabase.LoadAssetAtPath<SceneAsset>(_masterScenePath);

            OnProjectChange();
        }

        private void OnValidate()
        {
            UpdateValues();
        }

        private void OnProjectChange()
        {
            UpdateValues();
        }

        private void UpdateValues()
        {
            if (Application.isPlaying)
                return;

            SceneLoader.ActualLoadedScenes = new List<string>();
            _loadedScenes = new Scene[SceneManager.sceneCount];
            for (int x = 0; x < _loadedScenes.Length; x++)
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
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("ATLAS Master windows");
            GUILayout.FlexibleSpace();
            GUILayout.Label("Gardez cette fenêtre ouverte dans un coin, SVP");
            EditorGUILayout.EndHorizontal();
            
            EditorGUILayout.BeginHorizontal();
            DisplaySceneDetails();
            EditorGUILayout.EndHorizontal();
            EditorGUI.indentLevel--;
            GUILayout.Label("Toutes réclamations à mon propos sont à faire a Tim.");
        }

        #region Scene management
        private void DisplaySceneDetails()
        {
            EditorGUILayout.BeginVertical("Box");
            _masterScene = (SceneAsset) EditorGUILayout.ObjectField(new GUIContent("Master Scene"),  _masterScene, typeof(SceneAsset), false);
            if (_masterScene == null)
                GUILayout.Label("Si Master Scene est pas remplit les fonctions de request et de chargement de scene vont probablement pas marcher.", AtlasEditor.LabelColored(Color.red));

            GUILayout.Label("Les scenes qui seronts chargés");
            EditorGUI.indentLevel++;
            foreach (var loadedScene in _loadedScenes)
                WriteSceneName(loadedScene, AtlasEditor.LabelColored(Color.red));
            if (GUILayout.Button("Add the masterScene to the scenes"))
                EditorSceneManager.OpenScene(_masterScenePath, OpenSceneMode.Additive);
            EditorGUI.indentLevel--;
            EditorGUILayout.EndVertical();
        }

        private void WriteSceneName(Scene scene, GUIStyle styleError)
        {
            GUILayout.BeginHorizontal("Box");
            GUILayout.Label("'" + scene.name + "' - " + scene.buildIndex);
            if (scene.buildIndex == _activeScene.buildIndex)
                GUILayout.Label("(Active)");
            if (scene.buildIndex == -1 || scene.buildIndex >= SceneManager.sceneCountInBuildSettings)
                GUILayout.Label("Is not added to the build. Correct that else issue gotta occurs.", styleError);
            GUILayout.EndHorizontal();
        }
        #endregion
        
        [MenuItem("ATLAS/Master Scene")]
        public static void Open()
        {
            GetWindow<MasterAtlasWindows>();
        }
    }
}