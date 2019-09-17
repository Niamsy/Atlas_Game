using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Crafting
{
    [RequireComponent(typeof(Image))]
    [RequireComponent(typeof(RectTransform))]
    public class LockableSlot : MonoBehaviour
    {
        public bool isUnlocked
        {
            get { return _isUnlocked; }
            set
            {
                if (_lockImage) _lockImage.enabled = !value;
                _isUnlocked = value;
            }
        }

        [SerializeField] private bool _isUnlocked = false;
        [SerializeField] private Image _lockImage = null;
        private RectTransform _transform;

        // Start is called before the first frame update
        private void OnEnable()
        {
            _transform = GetComponent<RectTransform>();

            if (!isUnlocked)
            {
                if (_lockImage) _lockImage.enabled = false;
            }
        }
    }
}