using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCinematic : MonoBehaviour
{
    public Animator animator;

    void Start()
    {
        animator.SetFloat("InputMagnitude", 0.5f);
        StartCoroutine(StopPlayer());
    }

    public float speedRatio = 0.01f;
    public IEnumerator StopPlayer()
    {
        var speed = 0.5f;
        while (speed > 0)
        {
            animator.SetFloat("InputMagnitude", speed);
            speed -= speedRatio;
            yield return 0;
        }
    }
}
