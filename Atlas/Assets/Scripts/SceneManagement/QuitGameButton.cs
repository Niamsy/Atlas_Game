using UnityEngine;

namespace SceneManagement
{
    public class QuitGameButton : MonoBehaviour
    {
        public void QuitTheGame()
        {
            SceneLoader.Instance.QuitTheGame();
        }
    }
}
