using Game.Inventory;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Menu.Inventory
{
    [RequireComponent(typeof(Button))]
    public class ItemStackHUD : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IDropHandler
    {
        #region Variables

        [SerializeField] private Image     _sprite;
        [SerializeField] private Text      _quantity;

        protected Button        Button;
        protected ItemStack     ActualStack;

        private RectTransform    _rectTransform;
        private Canvas          _rootCanvas;
        
        private bool ShouldBeDisplayed
        {
            get { return ((ActualStack != null) && (!ActualStack.IsEmpty)); }
        }
        #endregion
                
        protected virtual void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _rootCanvas = GetComponentInParent<Canvas>();
            Button = GetComponent<Button>();
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
            
            if (ShouldBeDisplayed)
            {
                _quantity.text = ActualStack.Quantity.ToString();
                _sprite.sprite = ActualStack.Content.Sprite;
            }
        }

        #region Drag&Drop
        private Vector3 _originalPosition;
        private static ItemStackHUD _actualDrag;
        
        public void OnBeginDrag(PointerEventData eventData)
        {
            _originalPosition = transform.position;
            _sprite.transform.SetParent(_rootCanvas.transform);
            _sprite.transform.SetAsLastSibling();
            var position = _rectTransform.position;
            position.z = -1;
            _rectTransform.position = position;
            
            _actualDrag = this;
        }

        public void OnDrag(PointerEventData eventData)
        {
            _sprite.transform.position = Input.mousePosition;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _sprite.transform.position = _originalPosition;
            _sprite.transform.SetParent(transform);
            var position = _rectTransform.position;
            position.z = 0;
            _rectTransform.position = position;
            
            _actualDrag = null;
        }
        
        public void OnDrop(PointerEventData eventData)
        {
            if (RectTransformUtility.RectangleContainsScreenPoint(_rectTransform, Input.mousePosition))
                _actualDrag.ActualStack.SwapStack(ActualStack);
        }
        #endregion
    }
}
