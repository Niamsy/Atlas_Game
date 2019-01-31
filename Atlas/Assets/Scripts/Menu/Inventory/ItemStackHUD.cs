using System.Collections.Generic;
using Game.Inventory;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Menu.Inventory
{
    [RequireComponent(typeof(Button))]
    public class ItemStackHUD : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
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

        public void OnMouseOver()
        {
            if (Input.GetKeyDown(KeyCode.F))
                Drop();                
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
        
        public void OnBeginDrag(PointerEventData eventData)
        {
            _originalPosition = transform.position;
            _sprite.transform.SetParent(_rootCanvas.transform);
            _sprite.transform.SetAsLastSibling();
            var position = _rectTransform.position;
            position.z = -1;
            _rectTransform.position = position;
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
                     
            PointerEventData pointerData = new PointerEventData (EventSystem.current)
            {
                pointerId = -1,
            };
         
            pointerData.position = Input.mousePosition;
 
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerData, results);
         
            if (results.Count > 0)
            {
                var stack = results[0].gameObject.GetComponent<ItemStackHUD>();
                if (stack != null)
                    ActualStack.SwapStack(stack.ActualStack);
            }
            else if (results.Count == 0)
                Drop();
        }

        private void Drop()
        {
            Debug.Log("Drop ActualStack " + ActualStack);
        }
        #endregion
    }
}
