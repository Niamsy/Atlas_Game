using UnityEngine;
using UnityEngine.UI;

namespace Game.HUD.Objectives
{
    [RequireComponent(typeof(Image))]
    public class ObjectiveIcon : MonoBehaviour
    {
        [SerializeField] private Sprite unCompleteSprite = null;
        [SerializeField] private Sprite completeSprite = null;
        [SerializeField] private bool isComplete = false;

        private Image _image = null;

        public bool IsComplete => isComplete;

        public void Awake()
        {
            _image = GetComponent<Image>();
            SetComplete(isComplete);
        }

        public void SetComplete(bool completed)
        {
            isComplete = completed;
            
            if (_image == null) return;
            
            _image.sprite = IsComplete ? completeSprite : unCompleteSprite;
        }
    }
}
