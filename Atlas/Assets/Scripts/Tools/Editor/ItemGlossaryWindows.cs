using System;
using Game.Item;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Tools.Editor
{
    public class ItemGlossaryWindows : EditorWindow
    {
        private class ItemDisplay
        {
            public ItemAbstract Item;
            public bool IsInSort;
            public bool DisplayDetails;
            public float Timer;

            public ItemDisplay(ItemAbstract item)
            {
                Item = item;
                IsInSort = true;
                DisplayDetails = false;
                Timer = 1;
            }
        }
        
        private ItemDisplay[] _allItems;
        
        private readonly string _masterScenePath = "Assets/Scenes/Managers/Master Scene.unity";
        private string _searchField = "";

        private Vector2 _scrollPosition;

        private static Vector2 _minSize = new Vector2(512, 120);

        private void OnEnable()
        {
            minSize = _minSize;
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
            var allItems = ItemFactory.GetAllItems();
            
            Array.Sort(allItems, delegate(ItemAbstract item1, ItemAbstract item2) {
                return item1.Id.CompareTo(item2.Id);
            });
            
            _allItems = new ItemDisplay[allItems.Length];
            for (int x = 0; x < allItems.Length; x++)
                _allItems[x] = new ItemDisplay(allItems[x]);
                     
            UpdateSearch("");
        }

        private void OnGUI()
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Item Glossary");
            GUILayout.FlexibleSpace();
            GUILayout.BeginHorizontal("Box");
            GUILayout.Label("Search field");
            UpdateSearch(EditorGUILayout.TextField(_searchField));
            
            GUILayout.EndHorizontal();
            EditorGUILayout.EndHorizontal();
            
            EditorGUILayout.BeginHorizontal();
            DisplaySceneDetails();
            EditorGUILayout.EndHorizontal();
            EditorGUI.indentLevel--;
        }

        #region Scene management

        private void UpdateSearch(string newFilter)
        {
            if (newFilter == _searchField)
                return;
            
            _searchField = newFilter;
            for (int x = 0; x < _allItems.Length; x++)
            {
                ItemAbstract item = _allItems[x].Item;
                _allItems[x].IsInSort = _searchField == "" || (item != null && (item.Name.Contains(_searchField) ||
                                        item.Id.ToString().Contains(_searchField) ||
                                        item.GetType().ToString().Contains(_searchField)));
            }
        }
        
        private void DisplaySceneDetails()
        {
            _scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition, "Box");
            EditorGUI.indentLevel++;

            for (int x = 0; x < _allItems.Length; x++)
            {
                DisplayItem(_allItems[x]);
            }

            if (GUILayout.Button("Reload the glossary"))
            {
                EditorSceneManager.OpenScene(_masterScenePath, OpenSceneMode.Additive);
                UpdateValues();
            }
            EditorGUI.indentLevel--;
            EditorGUILayout.EndScrollView();
        }

        private void DisplayItem(ItemDisplay struc)
        {
            if (struc.Item == null || !struc.IsInSort)
                return;
            
            ItemAbstract item = struc.Item;
            
            EditorGUILayout.BeginHorizontal("Box");
            EditorGUILayout.LabelField(item.Id.ToString("0000") + "    " + item.Name);
            
            if (item.Category)
                EditorGUILayout.LabelField(item.Category.Name);
            else
                EditorGUILayout.LabelField("No Category set", AtlasEditor.LabelColored(Color.red));
            EditorGUILayout.LabelField("(" + item.GetType() + ")");

            //if (struc.DisplayDetails)
            {
                string missingPart = "";
                if (item.PrefabHoldedGO == null)
                    missingPart += "Holded ";
                if (item.PrefabDroppedGO == null)
                    missingPart += "Dropped ";
                if (item.Description == null)
                    missingPart += "Description ";
                
                EditorGUILayout.LabelField(missingPart, AtlasEditor.LabelColored(Color.red));
            }
            EditorGUILayout.EndFoldoutHeaderGroup();
            EditorGUILayout.EndHorizontal();
        }
        #endregion
        
        [MenuItem("ATLAS/Glossary")]
        public static void Open()
        {
            ItemGlossaryWindows windows = GetWindow<ItemGlossaryWindows>("Glossary", true);
            windows.minSize = _minSize;
        }
    }
}