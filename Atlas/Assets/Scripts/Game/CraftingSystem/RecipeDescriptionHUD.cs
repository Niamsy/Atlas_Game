using UnityEngine;
using UnityEngine.UI;
using Game.Crafting;

namespace Menu.Crafting
{
    public class RecipeDescriptionHUD : MonoBehaviour
    {
        [SerializeField] private Image _recipeImage;
        [SerializeField] private Text _recipeName;
        [SerializeField] private Text _recipeDescription;
        // Update is called once per frame

        private Recipe _currentRecipe = null;

        public Recipe Recipe {
            get { return _currentRecipe; }
            private set { }
        }


        void Update()
        {

        }

        public void SetRecipe(Recipe recipe = null)
        {

        }
    }
}