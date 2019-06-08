using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Game.Crafting
{
    [CreateAssetMenu(menuName = "Crafting/Recipe Book", order = 3)]
    public class RecipeBook : ScriptableObject
    {
        #region Class Chapter
        [Serializable]
        public class Chapter
        {
            [Range(0, 1000)]
            [SerializeField]
            private int _RequiredLevel;

            [SerializeField]
            private Recipe[] _Recipes;

            public int RequiredLevel { get => _RequiredLevel; private set { } }
            public Recipe[] Recipes { get => _Recipes; private set { } }
        }
        #endregion

        [SerializeField]
        private List<Chapter> _Chapters;

        public List<Chapter> Chapters { get => _Chapters; private set { } }

        public List<Recipe> GetAvailableRecipes(int level)
        {
            return GetRecipesFromPred(currentLevel => currentLevel <= level);
        }

        public List<Recipe> GetUnavailableRecipes(int level)
        {
            return GetRecipesFromPred(currentLevel => currentLevel >= level);
        }

        private List<Recipe> GetRecipesFromPred(Predicate<int> cmp)
        { 
            List<Recipe> recipes = new List<Recipe>();
            int i = 0;

            while (i <= _Chapters.Count)
            {
                if (cmp(_Chapters[i].RequiredLevel))
                {
                    recipes.AddRange(_Chapters[i].Recipes);
                }
                ++i;
            }

            return recipes;
        }

        #region Sort the chapters
        private void OnEnable()
        {
            if (_Chapters.Count > 0)
            {
                _Chapters.Sort((chapter1, chapter2) => chapter1.RequiredLevel.CompareTo(chapter2.RequiredLevel));
            }
        }
        #endregion
    }
}
