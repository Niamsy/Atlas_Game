using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Crafting
{
    [RequireComponent(typeof(RectTransform))]
    public class LockableSlot : MonoBehaviour
    {
        public Image LockImagePrefab;
        public bool isLocked
        {
            get { return _isLocked; }
            set
            {
                _isLocked = value;
                if (!_isLocked)
                {
                    _lockImage.enabled = false;
                }
                else
                {
                    _lockImage.enabled = true;
                }
            }
        }

        [SerializeField] private bool _isLocked = false;
        private RectTransform _transform;
        private Image _lockImage;
        // Start is called before the first frame update
        void Start()
        {
            _transform = GetComponent<RectTransform>();

            if (LockImagePrefab != null)
            {
                _lockImage = Instantiate(LockImagePrefab);
                _lockImage.rectTransform.SetParent(_transform);
                _lockImage.rectTransform.SetPositionAndRotation(new Vector3(), new Quaternion());
                _lockImage.rectTransform.anchorMax = new Vector2(1, 1);
                _lockImage.rectTransform.anchorMin = new Vector2(0, 0);
                _lockImage.rectTransform.localPosition = new Vector3();
                _lockImage.rectTransform.localScale = new Vector3(1, 1, 1);
                if (!isLocked)
                {
                    _lockImage.enabled = false;
                }
            }
        }
    }
}