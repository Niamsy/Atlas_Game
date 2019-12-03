using System;
using UnityEngine;
using UnityEngine.UI;

namespace Game.HUD.Objectives
{
    [RequireComponent(typeof(Image))]
    public class ObjectiveIcon : MonoBehaviour
    {
        [SerializeField] private Color uncompleteColor = Color.white;
        [SerializeField] private Color completeColor = Color.green;
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

            _image.color = IsComplete ? completeColor : uncompleteColor;
               
        }
    }
}
