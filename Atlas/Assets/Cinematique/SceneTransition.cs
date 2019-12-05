using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SceneManagement;

public class SceneTransition : MonoBehaviour
{
    float TimerTransition = 18.0f;
    float ElapsedTime = 0.0f;
    bool done = false;


    // Update is called once per frame
    void Update()
    {
        ElapsedTime += Time.deltaTime;
        if (ElapsedTime > TimerTransition)
        {
            if (!done)
            {
                done = true;
                SceneLoader.Instance.LoadScene(1, 4);
            }
        }
    }
}
