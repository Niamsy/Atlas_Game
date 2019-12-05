using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    float TimerTransition = 18.0f;
    float ElapsedTime = 0.0f;


    // Update is called once per frame
    void Update()
    {
        ElapsedTime += Time.deltaTime;
        if (ElapsedTime > TimerTransition)
        {
            SceneManager.LoadScene(1);
        }
    }
}
