using System;
using System.Collections.Generic;
using Game.Inventory;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Game.Crafting;

namespace Menu.Crafting
{
    [RequireComponent(typeof(Button))]
    [RequireComponent(typeof(LockableSlot))]
    public class RecipeHUD : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        #region Variables

        [SerializeField] private Image _image = null;

        protected Button Button = null;
        protected Recipe Recipe = null;

        private Canvas _rootCanvas = null;
        private bool _mouseOver = false;
        private LockableSlot _lockableSlot = null;
        private bool GetLockState => ((Recipe != null) && (Recipe.isUnlocked));
        #endregion

        protected virtual void Awake()
        {
            _rootCanvas = GetComponentInParent<Canvas>();
            _lockableSlot = GetComponent<LockableSlot>();
            Button = GetComponent<Button>();
        }

        public void SetRecipe(Recipe recipe, bool shouldBeUnlocked)
        {
            if (Recipe != null)
                Recipe.OnRecipeUpdate -= UpdateContent;
            Recipe = recipe;
            if (Recipe != null)
                Recipe.OnRecipeUpdate += UpdateContent;
            recipe?.Unlock(shouldBeUnlocked);
        }

        public void UpdateContent(Recipe recipe)
        {
            if (_image)
            {
                _image.sprite = recipe.Sprite;
                _image.enabled = !GetLockState;
                Debug.Log("Changing color of the image");
                //_image.color = GetLockState ? Color.gray : Color.white;
            }

            if (_lockableSlot)
            {
                _lockableSlot.isUnlocked = GetLockState;
            }
        }

        public virtual void OnPointerEnter(PointerEventData eventData)
        {
            DisplayDescription();
            _mouseOver = true;
        }

        public virtual void OnPointerExit(PointerEventData eventData)
        {
            HideDescription();
            _mouseOver = false;
        }

        [SerializeField] private RecipeDescriptionHUD _description = null;

        #region OnSelect/Deselect
        public void OnSelected(BaseEventData eventData)
        {
            // show right panel
        }

        public void OnDeselected(BaseEventData eventData)
        {
            // hide right panel
        }

        private void DisplayDescription()
        {
            if (_description != null && Recipe != null)
                _description.SetRecipe(Recipe);
        }

        private void HideDescription()
        {
            if (_description != null && Recipe != null &&
                _description.Recipe.Id == Recipe.Id)
                _description.SetRecipe(null);
        }

        private void DisplayRecipe()
        {

        }

        private void HideRecipe()
        {

        }
        #endregion
    }
}