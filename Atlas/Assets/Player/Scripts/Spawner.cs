using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Vector3))]

public class Spawner : MonoBehaviour
{
    public Vector3 pos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Spawn()
    {
        Debug.Log("Spawn");
        gameObject.transform.position = pos;
    }
}
