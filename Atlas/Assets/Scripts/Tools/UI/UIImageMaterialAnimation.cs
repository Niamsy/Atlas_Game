using UnityEngine;
using UnityEngine.UI;

namespace Tools.UI
{
    [RequireComponent(typeof(Image)), ExecuteInEditMode]
    public class UIImageMaterialAnimation : MonoBehaviour
    {
        private Image _image = null;

        public string PropertyName = "";
        private int _hashProperty = 0;

        public float Value = 0;
        
        private void Awake()
        {
            _image = GetComponent<Image>();
            _hashProperty = Shader.PropertyToID(PropertyName);
            _image.material.enableInstancing = true;
        }

        private void Update()
        {
            _image.material.SetFloat(_hashProperty, Value);
        }
    }
}
