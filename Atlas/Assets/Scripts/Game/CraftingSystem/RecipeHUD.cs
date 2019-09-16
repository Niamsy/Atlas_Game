using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Game.Crafting;
using UnityEngine.Events;

namespace Menu.Crafting
{
    [RequireComponent(typeof(Button))]
    [RequireComponent(typeof(LockableSlot))]
    public class RecipeHUD : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        #region Variables

        [SerializeField] private Image image = null;
        [SerializeField] private RecipeDescriptionHUD description = null;
        [SerializeField] private BlueprintDescriptionHUD blueprint = null;
        [SerializeField] private Animator blueprintAnimator = null;

        private int _hashShowed = Animator.StringToHash("Showed");
        private UnityAction<Recipe> _onSelectedCb;
        
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

        public void SetRecipe(Recipe recipe, bool shouldBeUnlocked, UnityAction<Recipe> onSelected)
        {
            if (Recipe != null)
                Recipe.OnRecipeUpdate -= UpdateContent;
            Recipe = recipe;
            if (Recipe != null)
                Recipe.OnRecipeUpdate += UpdateContent;
            _onSelectedCb = null;
            if (recipe == null) return;
            _onSelectedCb = onSelected;
            recipe.Unlock(shouldBeUnlocked);
        }

        public void UpdateContent(Recipe recipe)
        {
            image.enabled = recipe.Sprite != null;

            if (image)
            {
                image.sprite = recipe.Sprite;
                image.color = !GetLockState ? Color.gray : Color.white;
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
            _onSelectedCb?.Invoke(Recipe);
            if (blueprint)
            {
                blueprint.SetRecipe(Recipe);
                if (blueprintAnimator)
                {
                    blueprintAnimator.SetBool(_hashShowed, Recipe != null);
                }                
            }
        }

        public void OnDeselected()
        {
            if (description != null && Recipe != null &&
                blueprint.Recipe.Id == Recipe.Id)
                blueprint.Reset();
            if (!blueprintAnimator) return;
            blueprintAnimator.SetBool(_hashShowed, false);
        }

        private void DisplayDescription()
        {
            if (description != null && Recipe != null)
                description.SetRecipe(transform, Recipe);
        }

        private void HideDescription()
        {
            if (description != null && Recipe != null &&
                description.Recipe.Id == Recipe.Id)
                description.Reset();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            OnSelected();
        }
        #endregion
    }
}