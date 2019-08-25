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
    public class RecipeHUD : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        #region Variables

        [SerializeField] private Image _image = null;
        [SerializeField] private RecipeDescriptionHUD _description = null;
        [SerializeField] private BlueprintDescriptionHUD _blueprint = null;
        [SerializeField] private Animator _blueprintAnimator;

        private int _hashShowed = Animator.StringToHash("Showed");

        protected Button Button = null;
        protected Recipe Recipe = null;

        private LockableSlot _lockableSlot = null;

        private bool GetLockState => ((Recipe != null) && (Recipe.isUnlocked));
        #endregion

        private void OnEnable()
        {
            _lockableSlot = GetComponent<LockableSlot>();
        }

        protected virtual void Awake()
        {
            Button = GetComponent<Button>();
        }

        public void SetRecipe(Recipe recipe, bool shouldBeUnlocked)
        {
            if (Recipe != null)
                Recipe.OnRecipeUpdate -= UpdateContent;
            Recipe = recipe;
            if (Recipe != null)
                Recipe.OnRecipeUpdate += UpdateContent;
            if (!recipe) return;
            recipe.Unlock(shouldBeUnlocked);
        }

        public void UpdateContent(Recipe recipe)
        {
            _image.enabled = recipe.Sprite != null;

            if (_image)
            {
                _image.sprite = recipe.Sprite;
                _image.color = !GetLockState ? Color.gray : Color.white;
            }

            if (!_lockableSlot) _lockableSlot = GetComponent<LockableSlot>();

            if (_lockableSlot)
            {
                _lockableSlot.isUnlocked = GetLockState;
            }
        }

        public virtual void OnPointerEnter(PointerEventData eventData)
        {
            DisplayDescription();
        }

        public virtual void OnPointerExit(PointerEventData eventData)
        {
            HideDescription();
        }


        #region OnSelect/Deselect

        public void OnSelected()
        {
            Debug.Log("Recipe : " + Recipe + ", Blueprint : " + _blueprint + ", Animator : " + _blueprintAnimator);
            if (_blueprint)
            {
                _blueprint.SetRecipe(Recipe);
                if (_blueprintAnimator)
                {
                    _blueprintAnimator.SetBool(_hashShowed, Recipe != null);
                }                
            }
        }

        public void OnDeselected()
        {
            if (_description != null && Recipe != null &&
                _blueprint.Recipe.Id == Recipe.Id)
                _blueprint.Reset();
            if (!_blueprintAnimator) return;
            _blueprintAnimator.SetBool(_hashShowed, false);
        }

        private void DisplayDescription()
        {
            if (_description != null && Recipe != null)
                _description.SetRecipe(transform, Recipe);
        }

        private void HideDescription()
        {
            if (_description != null && Recipe != null &&
                _description.Recipe.Id == Recipe.Id)
                _description.Reset();
        }

        private void DisplayRecipe()
        {

        }

        private void HideRecipe()
        {

        }

        public void OnPointerClick(PointerEventData eventData)
        {
            OnSelected();
        }
        #endregion
    }
}