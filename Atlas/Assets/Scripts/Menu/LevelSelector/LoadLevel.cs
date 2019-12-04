using UnityEngine;
using UnityEngine.SceneManagement;

namespace Menu.LevelSelector
{
    public class LoadLevel : MonoBehaviour
    {
        public void LoadSceneIndex(string name)
        {
            Debug.Log("sceneBuildIndex to load: " + name);
            SceneManager.LoadScene(name, LoadSceneMode.Single);
        }
    }
}
