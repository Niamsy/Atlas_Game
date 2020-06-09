using Game.Map;
using Game.SavingSystem;
using Game.SavingSystem.Datas;
using SceneManagement;
using Tools.Editor;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Game.Editor
{
    [CustomEditor(typeof(SaveManager))]
    public class SaveManagerEditor : UnityEditor.Editor
    {
        public int SceneIndex = 2;

        private void Start()
        {
            SceneIndex = LevelManager.Instance.gameObject.scene.buildIndex;
        }

        public override void OnInspectorGUI()
        {
            SaveManager myTarget = (SaveManager) target;

            DisplayProfilDetails(myTarget);
            
            if (Application.isPlaying)
            {
                GUILayout.Label("Map data: Scene Index " + SceneLoader.ActiveLoadedScenes);
                GUILayout.BeginHorizontal();
                
                if (GUILayout.Button("Reload Map data"))
                    myTarget.LoadMapData(SceneLoader.ActiveLoadedScenes);
                if (GUILayout.Button("Force save Map data"))
                    myTarget.SaveMapData(SceneLoader.ActiveLoadedScenes);
                
                GUILayout.EndHorizontal();

                if (myTarget.MapData != null)
                {
                    EditorGUILayout.BeginVertical("Box");
                    EditorGUILayout.SelectableLabel("Map data");
                    EditorGUILayout.EndVertical();
                }
            }
        }
        
        #region Profils;
        public static int AccountToLoad = 0;

        private void DisplayProfilDetails(SaveManager target)
        {
            EditorGUILayout.BeginVertical("Box");
            EditorGUILayout.BeginHorizontal();
            if (target.AccountData != null)
            {
                GUILayout.Label("Account loaded ID:" + target.AccountData.ID + ".");
                if (GUILayout.Button("Unload"))
                    target.RemoveAccountData();
            }
            else
                GUILayout.Label("No Account actually loaded.", AtlasEditor.LabelColored(Color.red));
            EditorGUILayout.EndHorizontal();
            AccountToLoad = EditorGUILayout.IntField("Account to load", AccountToLoad);
            if (GUILayout.Button("Reload account data"))
                ReloadProfils(target, AccountToLoad);
            EditorGUILayout.EndVertical();

            EditorGUILayout.BeginVertical("Box");
            GUILayout.Label("List of profils");
            if (target.AccountData != null)
            {
                if (target.AccountData.Profils == null)
                    target.AccountData.Profils = new ProfilData[AccountToLoad];
                for (int index = 0; index < target.AccountData.Profils.Length; index++)
                {
                    DisplayProfile(target, target.AccountData.Profils[index], index);
                    GUILayout.FlexibleSpace();
                }
            }
            else
                GUILayout.Label("Load a account first.", AtlasEditor.LabelColored(Color.red));

            GUILayout.FlexibleSpace();

            if (target.SelectedProfil == null)
                GUILayout.Label("No load profils selected", AtlasEditor.LabelColored(Color.red));
            EditorGUILayout.EndVertical();
        }

        private void DisplayProfile(SaveManager target, ProfilData data, int index)
        {
            EditorGUILayout.BeginHorizontal("Box");
            GUILayout.Label(index + ".");
            if (!data.Used)
            {
                GUILayout.Label("Emtpy slot", AtlasEditor.LabelColored(Color.red));
                if (GUILayout.Button("Create"))
                {
                    SaveManager.InstantiateProfilToUse(target.AccountData, data, "Editor Profil " + index);
                    EditorUtility.SetDirty(target);
                }
            }
            else
            {
                string str;
                if (target.SelectedProfil != null && data.ID == target.SelectedProfil.ID)
                    str = GUILayout.TextField(data.Name, AtlasEditor.TextFieldColored(Color.green));
                else
                    str = GUILayout.TextField(data.Name);
                if (str != data.Name)
                {
                    data.Name = str;
                    target.SaveAccountData();
                }
                if (GUILayout.Button("Select"))
                {
                    target.SelectProfilToUseForSave(data);
                    EditorUtility.SetDirty(target);
                }
                if (GUILayout.Button("Delete"))
                {
                    SaveManager.DestroyProfile(target.AccountData, data);
                    EditorUtility.SetDirty(target);
                }
            }
            EditorGUILayout.EndHorizontal();
        }
        
        private void ReloadProfils(SaveManager target, int index)
        {
            target.LoadAccountDataByID(index);
            target.SelectProfilToUseForSave(null);
            EditorUtility.SetDirty(target);
        }
        
        #endregion
        
   
    }
}
