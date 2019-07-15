using Menu;
using UnityEngine;
using UnityEngine.UI;

namespace SceneManagement
{
    public class LoadingScreen : MonoBehaviour
    {
        private SceneLoader _sceneLoader = null;

        private void Awake()
        {
            _sceneLoader = SceneLoader.Instance;
        }
    }
}
