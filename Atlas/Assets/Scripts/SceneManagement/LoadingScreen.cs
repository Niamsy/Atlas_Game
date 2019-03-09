using Menu;
using UnityEngine;
using UnityEngine.UI;

namespace SceneManagement
{
    public class LoadingScreen : MonoBehaviour
    {
        private SceneLoader _sceneLoader;
        [SerializeField] private Image _fillProgress;

        private void Awake()
        {
            _sceneLoader = SceneLoader.Instance;
        }

        private void Update()
        {
            _fillProgress.fillAmount = _sceneLoader.Progress;
        }
    }
}
