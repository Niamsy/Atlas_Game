using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public string SceneToLoad;
    public LoadSceneMode SceneMode;

    public void Load()
    {
        SceneManager.LoadScene(SceneToLoad, SceneMode);
    }

}
