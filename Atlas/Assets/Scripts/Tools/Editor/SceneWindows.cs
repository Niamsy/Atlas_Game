using Menu;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Tools.Editor
{
    public class MasterAtlasWindows : EditorWindow
    {
        private static SceneAsset _masterScene;

        private void OnEnable()
        {
            titleContent.text = "ATLAS Master";
            _masterScene = AssetDatabase.LoadAssetAtPath<SceneAsset>("Assets/Scenes/Managers/Master Scene.unity");

            Update();
        }

        private void Update()
        {
            SceneLoader.ActualLoadedScene = SceneManager.GetActiveScene().name;
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
            GUILayout.Label("Toutes réclamations à mon propos sont à faire a Tim.");
            EditorGUI.indentLevel--;
        }
    
        [MenuItem("ATLAS/Master Scene")]
        public static void Open()
        {
            GetWindow<MasterAtlasWindows>();
        }
    }
}