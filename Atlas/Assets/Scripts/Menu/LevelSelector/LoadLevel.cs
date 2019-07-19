using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void LoadSceneIndex(string name)
    {
        Debug.Log("sceneBuildIndex to load: " + name);
        SceneManager.LoadScene(name, LoadSceneMode.Single);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
