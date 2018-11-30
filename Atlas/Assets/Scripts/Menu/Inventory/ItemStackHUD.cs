using Game.Inventory;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Menu.Inventory
{
    [RequireComponent(typeof(Button))]
    public class ItemStackHUD : MonoBehaviour
    {
        #region Variables

        [SerializeField] private Image _sprite;
        [SerializeField] private Text _quantity;

        protected ItemStack ActualStack;
        private Button _button;
        
        private bool ShouldBeDisplayed
        {
            get { return ((ActualStack != null) && (!ActualStack.IsEmpty)); }
        }
        #endregion
                
        private void Awake()
        {
            _button = GetComponent<Button>();
        }
        
        public void SetItemStack(ItemStack newStack)
        {
            if (ActualStack != null)
                ActualStack.OnItemStackUpdated -= SetItemStack;
            ActualStack = newStack;
            if (ActualStack != null)
                ActualStack.OnItemStackUpdated += SetItemStack;

            _quantity.enabled = ShouldBeDisplayed;   
            _sprite.enabled = ShouldBeDisplayed;
            _button.interactable = ShouldBeDisplayed;
            
            if (ShouldBeDisplayed)
            {
                _quantity.text = ActualStack.Quantity.ToString();
                _sprite.sprite = ActualStack.Content.Sprite;
            }
        }
    }
}
