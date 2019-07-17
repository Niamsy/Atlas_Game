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

        [SerializeField] private Image _sprite = null;

        protected Button Button = null;
        protected Recipe Recipe = null;

        private Canvas _rootCanvas = null;
        private bool _mouseOver = false;
        private LockableSlot _lockableSlot = null;
        private bool ShouldBeLocked => ((Recipe != null) && (!Recipe.isUnlocked));
        #endregion

        protected virtual void Awake()
        {
            _rootCanvas = GetComponentInParent<Canvas>();
            _lockableSlot = GetComponent<LockableSlot>();
            Button = GetComponent<Button>();
        }

        public void SetRecipe(Recipe recipe)
        {
            if (Recipe != null)
                Recipe.OnRecipeUpdate -= UpdateContent;
            Recipe = recipe;
            if (Recipe != null)
                Recipe.OnRecipeUpdate += UpdateContent;

            UpdateContent(recipe);
        }

        public void UpdateContent(Recipe recipe)
        {
            _sprite.enabled = ShouldBeLocked;
            if (_lockableSlot)
            {
                _lockableSlot.isLocked = ShouldBeLocked;
            }
            _sprite.sprite = recipe.Sprite;
        }

        public virtual void OnPointerEnter(PointerEventData eventData)
        {
            _mouseOver = true;
        }

        public virtual void OnPointerExit(PointerEventData eventData)
        {
            _mouseOver = false;
        }
    }
}