using System;
using System.Collections.Generic;
using Game.Inventory;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Menu.Inventory
{
    [RequireComponent(typeof(Button))]
    public class ItemStackHUD : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
    {
        #region Variables

        [SerializeField] private Image     _sprite = null;
        [SerializeField] private Text      _quantity = null;

        protected Button        Button = null;
        public ItemStack     ActualStack = null;

        private RectTransform    _rectTransform = null;
        private Canvas          _rootCanvas = null;
        private bool _mouseOver = false;
        private bool ShouldBeDisplayed => ((ActualStack != null) && (!ActualStack.IsEmpty));
        private GlowDeactivator _glowDeactivator = null;

        private Action<ItemStack> OnDrop;
        #endregion
                
        protected virtual void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _rootCanvas = GetComponentInParent<Canvas>();
            Button = GetComponent<Button>();
            _glowDeactivator = GetComponent<GlowDeactivator>();
        }

        private void Update()
        {
            if (_mouseOver && Input.GetKeyDown(KeyCode.F))
                Drop();
        }
        
        public void SetItemStack(ItemStack newStack, Action<ItemStack> onDrop = null)
        {
            if (ActualStack != null)
                ActualStack.OnItemStackUpdated -= UpdateContent;
            ActualStack = newStack;
            if (ActualStack != null)
                ActualStack.OnItemStackUpdated += UpdateContent;
            
            OnDrop = onDrop;
            
            UpdateContent(newStack);
        }

        public void UpdateContent(ItemStack newStack)
        {
            _quantity.enabled = ShouldBeDisplayed;   
            _sprite.enabled = ShouldBeDisplayed;
            
            if (ShouldBeDisplayed)
            {
                _quantity.text = ActualStack.Quantity.ToString();
                _sprite.sprite = ActualStack.Content.Sprite;
                if (_glowDeactivator != null)
                {
                    _glowDeactivator.SetGlowActive();
                }
            }
        }
        
        public void Drop()
        {
            if (OnDrop != null)
                OnDrop(ActualStack);
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
        #endregion

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
            